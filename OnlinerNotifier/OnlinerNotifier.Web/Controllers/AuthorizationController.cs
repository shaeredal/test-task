using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAuth2;
using OAuth2.Client;
using OAuth2.Client.Impl;
using OAuth2.Models;
using OnlinerNotifier.BLL.Redis;
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
            return Json( new { Url = GetClient(providerName).GetLoginLinkUri() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Auth(string providerName)
        {
            UserInfo userInfo;
            try
            {
                var client = GetClient(providerName);
                userInfo = client.GetUserInfo(Request.QueryString);
                //var token = ((OAuth2Client)client).GetToken(Request.QueryString);
            }
            catch (Exception e)
            {
                return Redirect("/#/auth");
            }
            var userId = userService.AddOrUpdate(userInfo);
            var key = GetKey(providerName, Request.QueryString);

            SetAuthParameters(userId.ToString(), key);

            return Redirect("/#/home");
        }

        private void SetAuthParameters(string id, string key)
        {
            SetStorage(id, key);
            SetCookies(id, key);
        }

        private void SetStorage(string id, string key)
        {
            RedisConnector.Connection.GetDatabase().StringSet($"user:{id}:key", key);
        }

        private void SetCookies(string id, string key)
        {
            HttpCookie idCookie = new HttpCookie("User", id);
            HttpCookie keyCookie = new HttpCookie("Key", key);
            Response.Cookies.Add(idCookie);
            Response.Cookies.Add(keyCookie);
        }

        private IClient GetClient(string providerName)
        {
            return authorizationRoot.Clients.First(c => c.Name == providerName);
        }

        private string GetKey(string providerName, NameValueCollection queryString)
        {
            //TODO: try to get token
            if (providerName == "Vkontakte" || providerName == "Facebook")
            {
                return queryString["code"];
            }
            else if (providerName == "Twitter")
            {
                return queryString["oauth_token"];
            }
            else
            {
                throw new Exception("Unsupported provider name.");
            }
        }
    }
}
