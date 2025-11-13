using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SistemaProduccionMVC.Controllers
{
    public class ProduccionController : Controller
    {
        // GET: ProduccionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProduccionController/Details/5
        public ActionResult Detalle( )
        {
            return View();
        }

    }
}
