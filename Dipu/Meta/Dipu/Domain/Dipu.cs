﻿//------------------------------------------------------------------------------------------------- 
// <copyright file="DipuDomain.cs" company="inxin bvba">
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

namespace Allors.Meta
{
    #region Allors
    [Id("D2E91A29-13B8-4261-BF99-67C494AE9CF4")]
    #endregion
    [Inherit(typeof(AppsDomain))]
    public partial class DipuDomain : Domain
    {
        public static DipuDomain Instance { get; internal set; }

        private DipuDomain(MetaPopulation metaPopulation) : base(metaPopulation)
        {
        }
    }
}