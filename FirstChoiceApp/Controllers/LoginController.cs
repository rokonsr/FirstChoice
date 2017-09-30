using FirstChoiceApp.Manager;
using FirstChoiceApp.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace FirstChoiceApp.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login login)
        {
            if (ModelState.IsValid)
            {
                LoginManager loginManager = new LoginManager();
                var user = loginManager.GetAuthentication(login);
                
                if (user.Username != null && user.Password != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, true);
                    Session["Username"] = user.Username;
                    Session["UserId"] = user.Id;
                    
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Error = "Invalid username or password!";
            }
            return View();
        }

        public ActionResult Logout()
        {
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
                Session.Abandon();
            }
            return Redirect("~/");
        }
    }
}