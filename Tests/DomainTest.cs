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
        private IDatabaseSession databaseSession;

        public IDatabaseSession DatabaseSession
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

        public IWorkspaceSession CreateWorkspaceSession()
        {
            var workspace = Config.Default.CreateWorkspace();
            return workspace.CreateSession();
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

            SecurityCache.Invalidate();
        }

        protected IObject[] GetObjects(ISession session, Composite objectType)
        {
            if (session is IDatabaseSession)
            {
                return session.Extent(objectType);
            }

            var workspaceSess = (IWorkspaceSession)session;
            return workspaceSess.LocalExtent(objectType);
        }
    }
}