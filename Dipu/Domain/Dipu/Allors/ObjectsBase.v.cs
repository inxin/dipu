//------------------------------------------------------------------------------------------------- 
// <copyright file="ObjectsBase.cs" company="inxin bvba">
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
    using Allors.Domain;

    public abstract partial class ObjectsBase<T> where T : IObject
    {
        public void Prepare(Setup setup)
        {
            this.BasePrepare(setup);
            this.AppsPrepare(setup);
            this.DipuPrepare(setup);
        }

        public void Setup(Setup setup)
        {
            this.BaseSetup(setup);
            this.AppsSetup(setup);
            this.DipuSetup(setup);

            this.session.Derive(true);
        }

        public void Secure(Security security)
        {
            this.BaseSecure(security);
            this.AppsSecure(security);
            this.DipuSecure(security);
        }
    }
}
