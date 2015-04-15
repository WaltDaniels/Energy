using System.Threading.Tasks;
using System.Web.Mvc;
using CentralHubService;

namespace CentralHub.Controllers
{
    public class GridBankController : Controller
    {
        private int _siteId = 1;

        public async Task<ActionResult> Collect()
        {
            var service = new CollectUsage();

            var usageDetails = await service.GetRemoteData(_siteId);
            service.SaveData(usageDetails);

            return View(usageDetails);
        }

        public ActionResult Display()
        {
            var service = new CollectUsage();

            ViewBag.SiteId = _siteId;
            var usageDetails = service.GetData(_siteId);
            return View(usageDetails);
        }
    }
}