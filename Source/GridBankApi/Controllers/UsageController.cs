using System.Dynamic;
using System.Web.Http;
using GridBankApi.Models;
using GridBankService;
using Newtonsoft.Json.Linq;

namespace GridBankApi.Controllers
{
    public class UsageController : ApiController
    {
        public int Get()
        {
            return 27;
        }

        [Route("api/usage/drain")]
        [AcceptVerbs("POST")]
        [HttpPost]
        public void Drain(DrainModel body)
        {

            var service = new PowerUsage();
            var currentPower = service.Drain(body.SiteId, body.Amount);
        }

        [Route("api/usage/charge")]
        [AcceptVerbs("POST")]
        [HttpPost]
        public void Charge(ChargeModel body)
        {
            var service = new PowerUsage();
            var currentPoser = service.Charge(body.SiteId, body.Amount);
        }

    }
}
