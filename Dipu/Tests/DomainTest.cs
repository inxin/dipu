//------------------------------------------------------------------------------------------------- 
// <copyright file="DomainTest.cs" company="inxin bvba">
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

namespace Allors
{
    using System.IO;
    using System.Xml;

    using Allors.Domain;
    using Allors.Meta;

    using NUnit.Framework;

    public class DomainTest
    {
        private ISession databaseSession;

        public ISession DatabaseSession
        {
            get { return this.databaseSession; }
        }

        [SetUp]
        public virtual void SetUp()
        {
            this.Init(true);
        }

        [TearDown]
        public virtual void TearDown()
        {
            this.databaseSession.Rollback();
            this.databaseSession = null;
        }

        protected void Init(bool setup)
        {
            if (setup)
            {
                var stringReader = new StringReader(Fixture.DefaultXml);
                var reader = new XmlTextReader(stringReader);
                Config.Default.Load(reader);
            }
            else
            {
                Config.Default.Init();
            }

            this.databaseSession = Config.Default.CreateSession();

            new SecurityCache(this.databaseSession).Invalidate();
        }

        protected IObject[] GetObjects(ISession session, Composite objectType)
        {
            return session.Extent(objectType);
        }
    }
}