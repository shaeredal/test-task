using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OAuth2;
using OAuth2.Client;
using OnlinerNotifier.Models;

namespace OnlinerNotifier.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly AuthorizationRoot _authorizationRoot;

        public AuthorizationController(AuthorizationRoot authorizationRoot)
        {
            _authorizationRoot = authorizationRoot;
        }

        public IEnumerable<LoginInfoModel> Index()
        {
            var model = _authorizationRoot.Clients.Select(client => new LoginInfoModel
            {
                ProviderName = client.Name
            });
            return model;
        }

        public ActionResult GetAuthUrl(string providerName)
        {
            return Json( new { Url = GetClient(providerName).GetLoginLinkUri()}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Auth(string providerName)
        {
            return View(GetClient(providerName).GetUserInfo(Request.QueryString));
        }

        private IClient GetClient(string providerName)
        {
            return _authorizationRoot.Clients.First(c => c.Name == providerName);
        }
    }
}
