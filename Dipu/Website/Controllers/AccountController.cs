using Allors.Web.Identity;

namespace Website.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    public class AccountController : BaseAccountController
    {
    }
}