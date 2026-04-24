using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class PedidosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
