using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParcProject.Models;

namespace ParcProject.Controllers
{
    public class OriginController : Controller
    {
        private WebAppContext db = new WebAppContext();

        // GET: Origin
        public ActionResult Index()
        {
            return View(db.OriginesOEN.ToList());
        }

        // GET: Origin/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OriginesOEN originesOEN = db.OriginesOEN.Find(id);
            if (originesOEN == null)
            {
                return HttpNotFound();
            }
            return View(originesOEN);
        }

        // GET: Origin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Origin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "oeNumber,blockNumber,SearchoeNumber")] OriginesOEN originesOEN)
        {
            if (ModelState.IsValid)
            {
                db.OriginesOEN.Add(originesOEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(originesOEN);
        }

        // GET: Origin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OriginesOEN originesOEN = db.OriginesOEN.Find(id);
            if (originesOEN == null)
            {
                return HttpNotFound();
            }
            return View(originesOEN);
        }

        // POST: Origin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "oeNumber,blockNumber,SearchoeNumber")] OriginesOEN originesOEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(originesOEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(originesOEN);
        }

        // GET: Origin/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OriginesOEN originesOEN = db.OriginesOEN.Find(id);
            if (originesOEN == null)
            {
                return HttpNotFound();
            }
            return View(originesOEN);
        }

        // POST: Origin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            OriginesOEN originesOEN = db.OriginesOEN.Find(id);
            db.OriginesOEN.Remove(originesOEN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult RemoveOrigin(int articleId, string originId)
        {
            ArticleOrigin aos = db.ArticleOrigins.Where(ao => ao.articleID == articleId && ao.oeNumber == originId).FirstOrDefault();
            if (aos != null)
            {
                db.ArticleOrigins.Remove(aos);
                db.SaveChanges();
            }
            return RedirectToAction("ResRecherche", "Article", new { id = articleId });
        }

        [HttpPost]
        public ActionResult RemoveDetail(int articleId, string carId)
        {
            getArticleIdsWithState aos = db.getArticleIdsWithState.Where(ao => ao.articleID == articleId && ao.carID == carId).FirstOrDefault();
            if (aos != null)
            {
                db.getArticleIdsWithState.Remove(aos);
                db.SaveChanges();
            }
            return RedirectToAction("ResRecherche", "Article", new { id = articleId });
        }
            
        [HttpPost]
        public JsonResult UpdateOrigin(ArticleOrigin oa)
        {
            ArticleOrigin gold = db.ArticleOrigins.Where(ao => ao.articleID == oa.articleID && ao.oeNumber == oa.oeNumber).FirstOrDefault();
            db.Entry(gold).CurrentValues.SetValues(oa);
            db.SaveChanges();
            return Json(gold);
        }

        // GET: Origin
        public ActionResult Assign()
        {
            return View();
        }

        public JsonResult assignOrigin(string oeNumber, int articleId)
        {
            ArticleOrigin ao = db.ArticleOrigins
                .Where(g => g.articleID == articleId && g.oeNumber == oeNumber).FirstOrDefault();
            if (db.Article.Where(a => a.articleId == articleId).FirstOrDefault() == null)
            {
                return Json("article dont exist");
            }
            else if (db.OriginesOEN.Where(o => o.oeNumber == oeNumber).FirstOrDefault() == null)
            {
                return Json("origin dont exist");
            }
            else if (ao != null)
            {
                if (ao.SortNo == 1)
                    return Json("exist");
                else
                {
                    ArticleOrigin gn = ao;
                    gn.SortNo = 1;
                    db.Entry(ao).CurrentValues.SetValues(gn);
                    db.SaveChanges();
                    return Json("success");
                }

            }
            else
            {
                ArticleOrigin ga = new ArticleOrigin()
                {
                    articleID = articleId,
                    oeNumber = oeNumber,
                    SortNo = 1
                };
                db.ArticleOrigins.Add(ga);
                db.SaveChanges();
                return Json("success");
            }
        }


    }
}
