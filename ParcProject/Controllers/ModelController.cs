using ParcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParcProject.Controllers
{
    public class ModelController : Controller
    {
        WebAppContext _db = new WebAppContext();
        // GET: Model
        public ActionResult Index()
        {
            return View(_db.Model.ToList());
        }

        // GET: Model/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Model/Create
        public ActionResult Create()
        {
            ViewData["Manufacterlist"] = _db.Manufacter.ToList();

            return View();
        }

        // POST: Model/Create
        [HttpPost]
        public ActionResult Create(Model modelToCreate)
        {
          
            if (!ModelState.IsValid)
            {
                return View();
            }
            modelToCreate.manuId = Request.Form["SelectID"];
            _db.Model.Add(modelToCreate);
            _db.SaveChanges();
            return RedirectToAction("Create");
        }

        // GET: Model/Edit/5
        public ActionResult Edit(String id)
        {
            
            var modelToEdit = (from m in _db.Model
                                  where m.ModelId == id
                                  select m).First();
            return View(modelToEdit);
        }

        // POST: Model/Edit/5
        [HttpPost]
        public ActionResult Edit(Model modelToEdit )
        {
            
            var originalModel = (from m in _db.Model
                                    where m.ModelId == modelToEdit.ModelId
                                    select m).First();

            if (!ModelState.IsValid)
                return View("Index");
            _db.Entry(originalModel).CurrentValues.SetValues(modelToEdit);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Model/Delete/5
        public ActionResult Delete(String id)
        {
            
            var v = _db.Model.Where(a => a.ModelId == id).FirstOrDefault();
            if (v != null)
                return View(v);
            else
                return HttpNotFound();
        }

        // POST: Model/Delete/5
        [HttpPost]
        public ActionResult Delete(String id, FormCollection collection)
        {
            bool status = false;
            var v = _db.Model.Where(a => a.ModelId == id).FirstOrDefault();
            if (v != null)
            {
                _db.Model.Remove(v);
                _db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }


        public String ManufacterName(String id)
        {
            String name =(from a in _db.Manufacter
                          where a.manuID == id 
                          select a.manuName).FirstOrDefault<String>();
            return name;
        }

        public String ModelName(String id)
        {
            String name = (from a in _db.Model
                           where a.ModelId == id
                           select a.modelName).FirstOrDefault<String>();
            return name;
        }
    }
}
