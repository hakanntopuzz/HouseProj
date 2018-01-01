using System.Configuration;
using System.Web.Mvc;
using HouseProj.Service.Contracts;

namespace HouseProj.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IElasticService _elasticService;

        public HomeController(IElasticService elasticService)
        {
            _elasticService = elasticService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Search(string queries)
        {
            string indexName = ConfigurationManager.AppSettings["ElasticSearchIndexName"];

            var model = _elasticService.SearchProduct(indexName,queries);

            return Json(model,JsonRequestBehavior.AllowGet);
        }
    }
}