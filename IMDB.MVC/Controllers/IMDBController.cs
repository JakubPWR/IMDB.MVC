using IMDB.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.MVC.Controllers
{
    public class IMDBController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
