using System.Web.Mvc;

namespace FirstChoiceApp.Controllers
{
    public class ExpenseController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}