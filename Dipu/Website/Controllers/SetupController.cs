namespace Website.Controllers
{
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Web;

    using Allors;

    using Allors.Domain;

    using System.Web.Mvc;

    using Allors.Integrations;

    using Microsoft.AspNet.Identity;

    public class SetupController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Setup()
        {

            SetupDefault();

            return this.View("Index");
        }

        [HttpPost]
        public ActionResult Import()
        {
            SetupDefault();

            var database = Config.Default;
            var dataPath = ConfigurationManager.AppSettings["dataPath"];
            if (!Path.IsPathRooted(dataPath))
            {
                dataPath = HttpRuntime.AppDomainAppPath + dataPath;
            }
            var defaultImportLog = new ImportLog();
            var importLog = new ConsoleImportLog(defaultImportLog);

            // Integration.Import(database, dataPath, importLog);

            return this.View("Index");
        }

        private static void SetupDefault()
        {
            var database = Config.Default;
            database.Init();

            using (var session = database.CreateSession())
            {
                new Setup(session).Apply();

                var passwordHasher = new PasswordHasher();

                var koen = new Persons(session).Extent().First(x => "koen@inxin.com".Equals(x.UserEmail));
                koen.UserPasswordHash = passwordHasher.HashPassword("a");

                new UserGroups(session).Administrators.AddMember(koen);

                session.Derive();
                session.Commit();
            }
        }
    }
}