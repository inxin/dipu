//------------------------------------------------------------------------------------------------- 
// <copyright file="Import`1.cs" company="inxin bvba">
// Copyright 2014-2015 inxin bvba.
// 
// Dual Licensed under
//   a) the Affero General Public Licence v3 (AGPL)
//   b) the Allors License
// 
// The AGPL License is included in the file LICENSE.
// The Allors License is an addendum to your contract.
// 
// Dipu is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// For more information visit http://www.dipu.com/legal
// </copyright>
//-------------------------------------------------------------------------------------------------

namespace Allors.Integrations
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Allors.Meta;

    public abstract class Import<TObject, TTable, TRecord> : Import
        where TObject : class, Allors.Domain.Object
        where TTable : Table
    {
        private readonly TTable table;
        private readonly RoleType keyRoleType;
        private readonly Func<TRecord, string> keyFunction;

        private readonly Dictionary<TObject, TRecord> recordByObject;
        private readonly Dictionary<string, TObject> objectsByExternalPrimaryKey;

        private IObjectType objectType;
        private TObject[] objects;

        protected Import(ISession session, CultureInfo cultureInfo, IImportLog log, TTable table, RoleType keyRoleType, Func<TRecord, string> keyFunction)
            : base(session, cultureInfo, log)
        {
            this.table = table;
            this.keyRoleType = keyRoleType;
            this.keyFunction = keyFunction;

            this.recordByObject = new Dictionary<TObject, TRecord>();

            var objectsWithExternalPrimaryKey = this.Session.Extent<TObject>();
            objectsWithExternalPrimaryKey.Filter.AddExists(keyRoleType);
            this.objectsByExternalPrimaryKey = objectsWithExternalPrimaryKey.ToDictionary(item => (string)item.Strategy.GetUnitRole(keyRoleType), item => item);
        }

        protected IObjectType ObjectType
        {
            get
            {
                if (this.objectType == null)
                {
                    this.objectType = this.Session.Database.ObjectFactory.GetObjectTypeForType(typeof(TObject));
                }

                return this.objectType;
            }
        }

        protected Dictionary<TObject, TRecord> RecordByObject
        {
            get
            {
                return this.recordByObject;
            }
        }

        protected Dictionary<string, TObject> ObjectsByExternalPrimaryKey
        {
            get
            {
                return this.objectsByExternalPrimaryKey;
            }
        }

        public TObject[] Objects
        {
            get
            {
                if (this.objects == null)
                {
                    this.objects = this.Session.Extent<TObject>().ToArray();
                }

                return objects;
            }
        }

        public void Execute()
        {
            var records = new List<TRecord>();
            foreach (TRecord record in this.table.GetRecords())
            {
                if (this.OnPrepare(record))
                {
                    records.Add(record);
                }
            }

            foreach (var record in records)
            {
                var externalPrimaryKey = this.keyFunction(record);
                if (!string.IsNullOrEmpty(externalPrimaryKey))
                {
                    TObject @object;
                    if (!this.ObjectsByExternalPrimaryKey.TryGetValue(externalPrimaryKey, out @object))
                    {
                        @object = this.Match(record);
                        if (@object == null)
                        {
                            @object = (TObject)Allors.ObjectBuilder.Build(this.Session, this.ObjectType);
                            @object.Strategy.SetUnitRole(this.keyRoleType, externalPrimaryKey);
                            this.ObjectsByExternalPrimaryKey[externalPrimaryKey] = @object;

                            this.OnBuild(@object, record);
                        }
                    }

                    this.RecordByObject[@object] = record;
                }
                else
                {
                    this.Log.AddError(new Exception(this.GetType().Name + ": illegal external primary key (" + this.keyFunction(record) + ")"));
                }
            }

            foreach (var dictionaryEntry in this.RecordByObject)
            {
                var location = dictionaryEntry.Key;
                var record = dictionaryEntry.Value;

                this.OnUpdate(location, record);

                this.Log.Tick();
            }
        }

        protected virtual TObject Match(TRecord record)
        {
            return null;
        }

        protected virtual bool OnPrepare(TRecord record)
        {
            this.TrimAll(record);
            return true;
        }

        protected virtual void OnBuild(TObject @object, TRecord record)
        {
        }

        protected abstract void OnUpdate(TObject @object, TRecord record);
    }
}