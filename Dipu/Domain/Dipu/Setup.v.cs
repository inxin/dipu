//------------------------------------------------------------------------------------------------- 
// <copyright file="Setup.v.cs" company="inxin bvba">
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
    public partial class Setup
    {
        private void OnPrePrepare()
        {
            this.BaseOnPrePrepare();
            this.AppsOnPrePrepare();
            this.DipuOnPrePrepare();
        }

        private void OnPostPrepare()
        {
            this.BaseOnPostPrepare();
            this.AppsOnPostPrepare();
            this.DipuOnPostPrepare();
        }

        private void OnPreSetup()
        {
            this.BaseOnPreSetup();
            this.AppsOnPreSetup();
            this.DipuOnPreSetup();
        }

        private void OnPostSetup()
        {
            this.BaseOnPostSetup();
            this.AppsOnPostSetup();
            this.DipuOnPostSetup();
        }
    }
}