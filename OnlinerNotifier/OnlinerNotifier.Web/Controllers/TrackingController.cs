using System.Web.Http;
using OnlinerNotifier.BLL.Models.TrackingModels;
using OnlinerNotifier.BLL.Services;
using OnlinerNotifier.Filters;

namespace OnlinerNotifier.Controllers
{
    [Authentication]
    public class TrackingController : ApiControllerBase
    {
        private ITrackingService trackingService;

        public TrackingController(ITrackingService trackingService)
        {
            this.trackingService = trackingService;
        }

        public IHttpActionResult Post([FromBody] TrackingModel trackingModel)
        {
            if (trackingService.ChangeStatus(trackingModel))
            {
                return Ok();
            };
            return NotFound();
        }
    }
}
