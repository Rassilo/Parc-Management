using ParcProject.Models;
using System.Linq;
using System.Web.Mvc;

namespace ParcProject.Controllers
{
    public class LogController : Controller
    {
        
        // GET: Log
        public ActionResult Index()
        {
            WebAppContext _db = new WebAppContext();

            return View(_db.Log.ToList());
        }

        //public void ajouter()


  
    }
}
