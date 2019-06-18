using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParcProject.Models;

namespace ParcProject.Controllers
{
    public class DetailsController : Controller
    {
        private WebAppContext db = new WebAppContext();

        // GET: Details
        public async Task<ActionResult> Index()
        {
            return View(await db.Details.ToListAsync());
        }

        // GET: Details/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var article_detail = db.getArticleIdsWithState.Where(g => g.carID == id).ToList();
            List<Articles> articles = new List<Articles>();
            foreach (var ad in article_detail)
            {
                var article = db.Article.Where(a => a.articleId == ad.articleID).FirstOrDefault();
                articles.Add(article);
            }

            Details details = await db.Details.FindAsync(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            ViewBag.articles = article_detail;
            return View(details);
        }

        // GET: Details/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "carId,CC,kwFrom,kwTo,HpFrom,HpTo,AnneeDe,AnneeA,Carrosserie,valves,cylinder,CodeMoteur,TypeCarburant,CarburantProcess,Date_Dev,Date_Dev2")] Details details)
        {
            if (ModelState.IsValid)
            {
                db.Details.Add(details);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(details);
        }

        // GET: Details/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Details details = await db.Details.FindAsync(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);
        }

        // POST: Details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "carId,CC,kwFrom,kwTo,HpFrom,HpTo,AnneeDe,AnneeA,Carrosserie,valves,cylinder,CodeMoteur,TypeCarburant,CarburantProcess,Date_Dev,Date_Dev2")] Details details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(details).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(details);
        }

        // GET: Details/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Details details = await db.Details.FindAsync(id);
            if (details == null)
            {
                return HttpNotFound();
            }
            return View(details);
        }

        // POST: Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Details details = await db.Details.FindAsync(id);
            db.Details.Remove(details);
            await db.SaveChangesAsync();
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

        [HttpGet]
        public ActionResult AssignArticle(string id)
        {
            List<Articles> AllArticles = db.Article.ToList();
            Details detail = db.Details.Where(d => d.carId == id).FirstOrDefault();
            var article_detail = db.getArticleIdsWithState.Where(g => g.carID == id).ToList();
            var articles = new List<Articles>();

            foreach (var ad in article_detail)
            {
                var artilce = db.Article.Where(a => a.articleId == ad.articleID).FirstOrDefault();
                articles.Add(artilce);
            }
            var restArticles = AllArticles.Except<Articles>(articles);
            ViewBag.restArticles = restArticles;
            ViewBag.detail = detail;
            return View();
        }

        [HttpPost]
        public ActionResult AssignArticle(int articleId, string detailId)
        {
            getArticleIdsWithState ga= new getArticleIdsWithState()
            {
                articleID = articleId,
                carID = detailId,
                SortNo = 1
            };
            db.getArticleIdsWithState.Add(ga);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = detailId });
        }

        [HttpPost]
        public JsonResult UpdateDetails(getArticleIdsWithState ga)
        {
            getArticleIdsWithState gold = db.getArticleIdsWithState.Where(g => g.carID == ga.carID && g.articleID == ga.articleID).FirstOrDefault();
            db.Entry(gold).CurrentValues.SetValues(ga);
            db.SaveChanges();
            return Json(gold);
        }

      



    }


}
