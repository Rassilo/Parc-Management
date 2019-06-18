using ParcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParcProject.Controllers
{
    public class VehicleController : Controller
    {
        WebAppContext _db = new WebAppContext();

        // GET: Vehicle
        public ActionResult Index()
        {
            return View(_db.Vehicle.ToList());
        }

        // GET: Vehicle/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Vehicle/Create
        public ActionResult Create()
        {
            ViewData["Modellist"] = _db.Model.ToList();
            ViewData["Manufacterlist"] = _db.Manufacter.ToList();
            ViewData["DetailList"] = _db.Details.ToList();

            return View();
        }

        // POST: Vehicle/Create
        [HttpPost]
        public ActionResult Create(Vehicle vehicleToCreate)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            vehicleToCreate.manuId = Request.Form["manufacter"];
            vehicleToCreate.modelId = Request.Form["model"];
            vehicleToCreate.carId = Request.Form["detail"];
            _db.Vehicle.Add(vehicleToCreate);
            _db.SaveChanges();
            return RedirectToAction("Create");
        }

        // GET: Vehicle/Edit/5
        public ActionResult Edit(int id)
        {
            var vehicleToEdit = (from m in _db.Vehicle
                                 where m.EntryVin == id
                               select m).First();
            return View(vehicleToEdit);
        }

        // POST: Vehicle/Edit/5
        [HttpPost]
        public ActionResult Edit(Vehicle vehicleToEdit)
        {
            var originalVehicle = (from m in _db.Vehicle
                                   where m.EntryVin == vehicleToEdit.EntryVin
                                 select m).First();

            if (!ModelState.IsValid)
                return View("Index");

            _db.Entry(originalVehicle).CurrentValues.SetValues(vehicleToEdit);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Vehicle/Delete/5
        public ActionResult Delete(int id)
        {
            var v = _db.Vehicle.Where(a => a.EntryVin == id).FirstOrDefault();
            if (v != null)
                return View(v);
            else
                return HttpNotFound();
        }

        // POST: Vehicle/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            bool status = false;
            var v = _db.Vehicle.Where(a => a.EntryVin == id).FirstOrDefault();
            if (v != null)
            {
                _db.Vehicle.Remove(v);
                _db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }
    }
}
