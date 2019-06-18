using ParcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParcProject.Controllers
{
    public class ManufacterController : Controller
    {
        WebAppContext _db = new WebAppContext();
        // GET: Manufacter
        public ActionResult Index()
        {
            return View(_db.Manufacter.ToList());
        }

        // GET: Manufacter/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Manufacter/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manufacter/Create
        [HttpPost]
        public ActionResult Create(Manufacter manufacterToCreate)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _db.Manufacter.Add(manufacterToCreate);
            _db.SaveChanges();
            return View();
        }

        // GET: Manufacter/Edit/5
        public ActionResult Edit(String id)
        {
            var manufacterToEdit = (from m in _db.Manufacter
                               where m.manuID == id
                               select m).First();
            return View(manufacterToEdit);
        }

        // POST: Manufacter/Edit/5
        [HttpPost]
        public ActionResult Edit(Manufacter manufacterToEdit)
        {
            var originalManufacter = (from m in _db.Manufacter
                                 where m.manuID == manufacterToEdit.manuID
                                 select m).First();

            if (!ModelState.IsValid)
                return View("Index");
            _db.Entry(originalManufacter).CurrentValues.SetValues(manufacterToEdit);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Manufacter/Delete/5
        public ActionResult Delete(String id)
        {
            var v = _db.Manufacter.Where(a => a.manuID == id).FirstOrDefault();
            if (v != null)
                return View(v);
            else
                return HttpNotFound();
        }

        // POST: Manufacter/Delete/5
        [HttpPost]
        public ActionResult Delete(String id, FormCollection collection)
        {
            bool status = false;
            var v = _db.Manufacter.Where(a => a.manuID == id).FirstOrDefault();
            if (v != null)
            {
                _db.Manufacter.Remove(v);
                _db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }


        [HttpPost]
        public JsonResult getModel(string idm)
        {
            List<Model> models = _db.Model.Where(m => m.manuId == idm).ToList();
            return Json(models);
        }

    }
}
