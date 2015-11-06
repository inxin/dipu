//------------------------------------------------------------------------------------------------- 
// <copyright file="Global.asax.cs" company="inxin bvba">
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
    using System.Web.Optimization;
    using System.Web.Routing;

    using Allors.Web.Mvc;

    public class MvcApplication : System.Web.HttpApplication
    {
        public void Application_BeginRequest()
        {
            AllorsContext.Instance = new AllorsContext();
        }

        public void Application_Error()
        {
            AllorsContext.Instance.Dispose();
        }

        public void Application_EndRequest()
        {
            AllorsContext.Instance.Dispose();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MetaConfig.Register();
            ViewEngineConfig.Register();
            MenuConfig.Register();
        }
    }
}
