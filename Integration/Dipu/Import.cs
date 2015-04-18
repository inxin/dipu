//------------------------------------------------------------------------------------------------- 
// <copyright file="Import.cs" company="inxin bvba">
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
    using System.Reflection;

    public abstract class Import
    {
        private readonly ISession session;
        private readonly IImportLog log;
        private readonly CultureInfo cultureInfo;

        private Dictionary<Type, FieldInfo[]> fieldsByClass;

        protected Import(ISession session, CultureInfo cultureInfo, IImportLog log)
        {
            this.session = session;
            this.log = log;
            this.cultureInfo = cultureInfo;
        }

        protected IImportLog Log
        {
            get
            {
                return this.log;
            }
        }

        protected ISession Session
        {
            get
            {
                return this.session;
            }
        }

        protected void TrimAll(object record)
        {
            var type = record.GetType();

            if (this.fieldsByClass == null)
            {
                this.fieldsByClass = new Dictionary<Type, FieldInfo[]>();
            }

            FieldInfo[] fields;
            if (!this.fieldsByClass.TryGetValue(type, out fields))
            {
                fields = type.GetFields();
                this.fieldsByClass[type] = fields;
            }

            foreach (var field in fields)
            {
                var value = field.GetValue(record) as string;
                if (value != null)
                {
                    value = this.Trim(value);
                    field.SetValue(record, value);
                }
            }
        }

        protected string Trim(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        protected string ToLower(string value)
        {
            return value != null ? value.ToLowerInvariant() : null;
        }

        protected int? ParseInt(string stringValue)
        {
            if (!string.IsNullOrWhiteSpace(stringValue))
            {
                try
                {
                    return Convert.ToInt32(stringValue);
                }
                catch (Exception e)
                {
                    this.Log.AddError(e);
                }
            }

            return null;
        }

        protected decimal? ParseDecimal(string stringValue)
        {
            return this.ParseDecimal(stringValue, this.cultureInfo);
        }

        protected decimal? ParseDecimal(string stringValue, CultureInfo decimalCultureInfo)
        {
            if (stringValue != null)
            {
                try
                {
                    return Convert.ToDecimal(stringValue, decimalCultureInfo);
                }
                catch (Exception e)
                {
                    this.Log.AddError(e);
                }
            }

            return null;
        }

        protected DateTime? ParseDate(string stringValue)
        {
            return this.ParseDate(stringValue, this.cultureInfo);
        }

        protected DateTime? ParseDate(string stringValue, CultureInfo dateCultureInfo)
        {
            if (stringValue != null)
            {
                try
                {
                    var date = Convert.ToDateTime(stringValue, dateCultureInfo);
                    if (date.Kind == DateTimeKind.Unspecified)
                    {
                        date = new DateTime(
                            date.Year,
                            date.Month,
                            date.Day,
                            date.Hour,
                            date.Minute,
                            date.Second,
                            date.Millisecond,
                            DateTimeKind.Utc);
                    }

                    return date;
                }
                catch (Exception e)
                {
                    this.Log.AddError(e);
                }
            }

            return null;
        }

        protected bool? ParseBoolean(string stringValue)
        {
            if (stringValue != null)
            {
                try
                {
                    switch (stringValue.ToLowerInvariant())
                    {
                        case "0":
                        case "n":
                        case "no":
                            return false;
                        case "1":
                        case "y":
                        case "yes":
                            return true;
                        default:
                            return Convert.ToBoolean(stringValue);
                    }
                }
                catch (Exception e)
                {
                    this.Log.AddError(e);
                }
            }

            return null;
        }
    }
}
