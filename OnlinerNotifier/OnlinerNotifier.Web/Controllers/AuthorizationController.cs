using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAuth2;
using OAuth2.Client;
using OAuth2.Models;
using OnlinerNotifier.BLL.Services;

namespace OnlinerNotifier.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IUserService userService;

        private readonly AuthorizationRoot authorizationRoot;

        public AuthorizationController(AuthorizationRoot authorizationRoot, IUserService userService)
        {
            this.authorizationRoot = authorizationRoot;
            this.userService = userService;
        }

        public ActionResult GetAuthUrl(string providerName)
        {
            return Json( new { Url = GetClient(providerName).GetLoginLinkUri()}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Auth(string providerName)
        {
            UserInfo userInfo;
            try
            {
                userInfo = GetClient(providerName).GetUserInfo(Request.QueryString);
            }
            catch (Exception e)
            {
                return Redirect("/#/auth");
            }
            var userId = userService.AddOrUpdate(userInfo);

            SetCookies(userId.ToString());

            return Redirect("/#/home");
        }

        private void SetCookies(string id)
        {
            HttpCookie cookie = new HttpCookie("User", id);
            Response.Cookies.Add(cookie);
        }

        private IClient GetClient(string providerName)
        {
            return authorizationRoot.Clients.First(c => c.Name == providerName);
        }
    }
}
