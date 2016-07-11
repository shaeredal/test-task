﻿using System.Web.Http;
using OnlinerNotifier.BLL.Models.NotificationModels;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.BLL.Services.Interfaces;
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
        public IHttpActionResult Post([FromBody]NotificationParametersModel parameters)
        {
            if (userService.SetNotificationParameters(Principal.Id, parameters))
            {
                return Ok();
            }
            return Conflict();
        }

        public IHttpActionResult Delete()
        {
            if (userService.DisableNotifications(Principal.Id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
