using System;
using System.Web.Http;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.Filters;

namespace OnlinerNotifier.Controllers
{
    [Authentication]
    public class NotificationController : ApiControllerBase
    {
        private IUserService userService;

        public NotificationController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]DateTime time)
        {
            if (userService.SetNotificationTime(Principal.Id, time))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
