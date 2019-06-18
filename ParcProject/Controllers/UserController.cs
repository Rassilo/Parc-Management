using ParcProject.Models;

using System.Linq;

using System.Web.Mvc;

namespace ParcProject.Controllers
{
    public class UserController : Controller
    {
              
        // GET: User
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User u )
        {
            WebAppContext _db = new WebAppContext();
            string message = "";
            var rst = (from m in _db.User
                                  where m.login== u.login && m.password==u.password
                                  select m).FirstOrDefault();

            
            if (rst != null)
            {
                Session["login"] = rst.login.ToString();
                Session["role"] = rst.role.ToString();
                Session["password"] = "";
                return RedirectToAction("PostLogin");
            }
              else 
              {
                   message = "login failed";
              }
              ViewBag.Message = message;

      
      //      ModelState.AddModelError("", "login ou mot de passe non valide");

            return View();
        }
        
        public ActionResult PostLogin()
        {
            if (Session["login"]!=null)
            {
                return View();
            }
            return RedirectToAction("Login");
            
        }

    }
}