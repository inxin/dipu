//------------------------------------------------------------------------------------------------- 
// <copyright file="IImportLog.cs" company="inxin bvba">
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

    public interface IImportLog
    {
        bool HasErrors { get; }

        bool HasWarnings { get; }

        void AddMarker(string name, int? total = null);

        void AddWarning(string message);

        void AddError(Exception exception);
        
        void Tick();
    }
}
