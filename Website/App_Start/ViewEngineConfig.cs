//------------------------------------------------------------------------------------------------- 
// <copyright file="ViewEngineConfig.cs" company="inxin bvba">
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

namespace Website
{
    using System.Web.Mvc;

    public class ViewEngineConfig
    {
        public static void Register()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new AllorsRazorViewEngine());
        }

        private class AllorsRazorViewEngine : RazorViewEngine
        {
            public AllorsRazorViewEngine()
            {
                this.ViewLocationFormats = new[]
{
"~/Views/{1}/{0}.cshtml",
"~/Allors/Dipu/{1}/{0}.cshtml",
"~/Allors/Base/{1}/{0}.cshtml",
"~/Views/Shared/{0}.cshtml",
"~/Allors/Dipu/Shared/{0}.cshtml",
"~/Allors/Base/Shared/{0}.cshtml",
};

                this.MasterLocationFormats = this.ViewLocationFormats;

                this.PartialViewLocationFormats = new[]
{
"~/Views/{1}/{0}.cshtml",
"~/Allors/Dipu/{1}/{0}.cshtml",
"~/Allors/Base/{1}/{0}.cshtml",
"~/Views/Shared/{0}.cshtml",
"~/Allors/Dipu/Shared/{0}.cshtml",
"~/Allors/Base/Shared/{0}.cshtml",
};
            }
        }
    }
}
