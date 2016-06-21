using System.Web.Http;
using OnlinerNotifier.Filters;

namespace OnlinerNotifier.Controllers
{
    public class ApiControllerBase : ApiController
    {
        public Principal Principal
        {
            get
            {
                return (Principal) RequestContext.Principal;
            }
        }
    }
}