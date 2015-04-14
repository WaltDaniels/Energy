using System.Threading.Tasks;
using System.Web.Mvc;
using CentralHubService;

namespace CentralHub.Controllers
{
    public class GridBankController : Controller
    {
        public async Task<ActionResult> Collect()
        {
            var service = new CollectUsage();
            
            var usageDetails = await service.GetData(1);
            service.SaveData(usageDetails);

            return View(usageDetails);
        }

        public ActionResult Display()
        {
            return View();
        }
    }
}