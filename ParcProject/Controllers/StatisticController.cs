using ParcProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParcProject.Controllers
{
    public class StatisticController : Controller
    {
        private WebAppContext db = new WebAppContext();
        // GET: Statistic
        public ActionResult Index()
        {
            return View();
        }

        class CustomData
        {
            public string label { get; set; }
            public int value { get; set; }
        }

        public JsonResult getData()
        {
            var sql = "select d.carId as label, count(d.carId) as value from details d INNER JOIN getarticleidswithstates g ON g.carID = d.carId GROUP BY d.carId";
            List<CustomData> list = db.Database.SqlQuery<CustomData>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        class TopSales
        {
            public string articleId { get; set; }
            public int Quantity { get; set; }
            public int net { get; set; }
            public string DateSale { get; set; }

        }
        public JsonResult getTopSales()
        {
            var sql = "select s.ArticleId as articleId, s.Quantity, s.DateSale , s.Value - s.Cost as net from sales s order by net DESC limit 5";
            List<TopSales> list = db.Database.SqlQuery<TopSales>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getTopQuantity()
        {
            var sql = "select s.ArticleId as articleId, s.Quantity, s.DateSale , s.Value - s.Cost as net from sales s order by Quantity DESC limit 4";
            List<TopSales> list = db.Database.SqlQuery<TopSales>(sql).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

    }
}