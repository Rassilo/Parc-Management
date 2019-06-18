using ParcProject.Models;

using System.Collections.Generic;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Dynamic;
using System.Data.SqlClient;
using System.Web;
using System.IO;

namespace ParcProject.Controllers
{
    public class ArticleController : Controller
    {
        WebAppContext _db = new WebAppContext();
        // GET: Article
        public ActionResult Index()
        {
            List<Articles> lst = _db.Article.ToList();
            return View(lst);
        }

        public ActionResult listeArticleId()
        {
            List<SelectListItem> mySkills = new List<SelectListItem>() {
            new SelectListItem {
                 Text = "ASP.NET MVC", Value = "1"
                },
            new SelectListItem {
                Text = "ASP.NET WEB API", Value = "2"
                },
            new SelectListItem {
                Text = "ENTITY FRAMEWORK", Value = "3"
                }
            };
            ViewData["MySkills"] = mySkills;

            List<Articles> lst = _db.Article.ToList();

            return View(lst);

        }
        [HttpPost]
        public ActionResult listeArticleId(Articles art)
        {


            return new JsonResult { Data = new { status = Request.Form["SelectID"] } };
        }

        [HttpGet]
        public ActionResult AssignOrigin(int id)
        {
            Articles article = _db.Article.Where(a => a.articleId == id).FirstOrDefault();

            var article_orgin = _db.ArticleOrigins.Where(g => g.articleID == id).ToList();
            List<OriginesOEN> allOrigins = _db.OriginesOEN.ToList();
            var origins = new List<OriginesOEN>();

            foreach (var ao in article_orgin)
            {
                var origin = _db.OriginesOEN.Where(d => d.oeNumber == ao.oeNumber).FirstOrDefault();
                origins.Add(origin);
            }
            var restOrigins = allOrigins.Except<OriginesOEN>(origins);

            ViewBag.restOrigins = restOrigins;
            ViewBag.article = article;
            return View();
        }

        [HttpPost]
        public ActionResult AssignOrigin(int articleId, string originId)
        {
            ArticleOrigin ao = new ArticleOrigin()
            {
                articleID = articleId,
                oeNumber = originId,
                SortNo = 1
            };
            _db.ArticleOrigins.Add(ao);
            _db.SaveChanges();

            return RedirectToAction("ResRecherche", new { id = articleId });
        }

        [HttpGet]
        public ActionResult AssignDetails(int id)
        {
            Articles article = _db.Article.Where(a => a.articleId == id).FirstOrDefault();
            var article_detail = _db.getArticleIdsWithState.Where(g => g.articleID == id).ToList();
            List<Details> allDetails = _db.Details.ToList();
            var details = new List<Details>();
            foreach (var ad in article_detail)
            {
                var detail = _db.Details.Where(d => d.carId == ad.carID).FirstOrDefault();
                details.Add(detail);
            }
            var restDetails = allDetails.Except<Details>(details);

            ViewBag.restDetails = restDetails;
            ViewBag.article = article;
            return View();
        }

        [HttpPost]
        public ActionResult AssignDetails(int articleId, string carId)
        {
            getArticleIdsWithState gs = new getArticleIdsWithState()
            {
                articleID = articleId,
                carID = carId,
                SortNo = 1
            };
            _db.getArticleIdsWithState.Add(gs);
            _db.SaveChanges();

            return RedirectToAction("ResRecherche", new { id = articleId });
        }

        // GET: Article/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Article/Create
        public ActionResult Create()
        {
            ViewData["BrandList"] = _db.Brand.ToList();
            return View();
        }
        // POST: home/Create
        [HttpPost]
        public ActionResult Create(Articles articlesToCreate)
        {



            if (!ModelState.IsValid)
            {
                return View();
            }

            articlesToCreate.brandNo = int.Parse(Request.Form["SelectID"]);
            _db.Article.Add(articlesToCreate);
            _db.SaveChanges();

            //return new JsonResult { Data = new { status = articlesToCreate.articleId } };

            return RedirectToAction("CreateProp", new { id = articlesToCreate.articleId });

        }


        // GET: Article/Edit/5
        public ActionResult Edit(int id)
        {
            var articlesToEdit = (from m in _db.Article
                                  where m.articleId == id
                                  select m).First();
            return View(articlesToEdit);
        }


        // POST: Article/Edit/5
        [HttpPost]
        public ActionResult Edit(Articles articlesToEdit)
        {

            var originalArticles = (from m in _db.Article
                                    where m.articleId == articlesToEdit.articleId
                                    select m).First();

            if (!ModelState.IsValid)
                return View("Index");
            _db.Entry(originalArticles).CurrentValues.SetValues(articlesToEdit);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }


        // GET: Article/Delete/5
        public ActionResult Delete(int id)
        {

            var v = _db.Article.Where(a => a.articleId == id).FirstOrDefault();
            if (v != null)
                return View(v);
            else
                return HttpNotFound();
        }

        // POST: Article/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {

            bool status = false;
            var v = _db.Article.Where(a => a.articleId == id).FirstOrDefault();
            if (v != null)
            {
                _db.Article.Remove(v);
                _db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }


        //GET: Article/AddProprieties/5

        public ActionResult CreateProp(int id)
        {
            var model = new ArticleAttributes
            {
                articleId = id
            };

            return View(model);

            // JsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            /*JsonResult Dat = new JsonResult { Data = new { status = id } };
            return  Json( Dat , JsonRequestBehavior.AllowGet); */

        }

        //POST: Article/AddProprieties/5
        [HttpPost]
        public ActionResult CreateProp(ArticleAttributes ArticleAttributesToCreate, string submitButton)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }

            //ArticleAttributesToCreate.articleId = id;
            _db.ArticleAttributes.Add(ArticleAttributesToCreate);
            _db.SaveChanges();


            if (submitButton.Equals("Add more Attributes"))
            {
                return RedirectToAction("CreateProp", new { id = int.Parse(Request.Form["articleid"]) });
            }
            else
            {
                return RedirectToAction("AddArticleImages", new { id = ArticleAttributesToCreate.articleId });
            }


        }

        //GET/Images
        public ActionResult AddArticleImages(int id)
        {
            ViewBag.id = id;
            ViewBag.images = _db.Images.Where(i => i.articleId == id).ToList();
            return View();
        }


        [HttpPost]
        public ActionResult UploadImage(ImageModel img)
        {
            HttpPostedFileBase file = img.FileImage;
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads"),
                                               Path.GetFileName(file.FileName));
                    var model = new Images
                    {
                        articleId = img.IdArticle,
                        docFileName = file.FileName,
                        docTypeName = file.ContentType,
                        docTypeId = file.ContentLength

                    };
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                    _db.Images.Add(model);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return RedirectToAction("AddArticleImages", new { id = img.IdArticle });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ImageToCreate"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddArticleImages(Images ImageToCreate)
        {


            if (!ModelState.IsValid)
            {
                return View();
            }
            _db.Images.Add(ImageToCreate);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        /* -------------------------------- partie modification de la base de donnees ---------------------------------- */



        /* Affectation des origine et carID par ArticleID */

        //recherche par ArticleID
        public ActionResult Rechercher()
        {

            return View();

        }
        //recherche par ArticleID
        [HttpPost]
        public ActionResult Rechercher(Articles article)
        {


            var v = _db.Article.Where(a => a.articleId == article.articleId).FirstOrDefault();
            if (v != null)
            {

                return RedirectToAction("ResRecherche", new { id = article.articleId });
            }
            else
            {
                ViewBag.Message = "ID article n'existe pas ";
                return View();
            }

        }

        public ActionResult ResRecherche(int id)
        {
            var article = _db.Article.Where(a => a.articleId == id).FirstOrDefault();
            var cousins = getCousins(article.articleId);
            // detail lists
            var article_detail = _db.getArticleIdsWithState.Where(g => g.articleID == id).ToList();

            // origin lists
            var article_orgin = _db.ArticleOrigins.Where(g => g.articleID == id).ToList();
            var origins = new List<OriginesOEN>();
            foreach (var ao in article_orgin)
            {
                var origin = _db.OriginesOEN.Where(d => d.oeNumber == ao.oeNumber).FirstOrDefault();
                origins.Add(origin);
            }
            var plusDetail = _db.ArticleAttributes.Where(a => a.articleId == id).FirstOrDefault();
            List<ArticleAttributes> Attrlst = _db.ArticleAttributes.Where(g => g.articleId == id).ToList();

            ViewBag.Attrlst = Attrlst;
            ViewBag.plusDetail = plusDetail;
            ViewBag.images = _db.Images.Where(i => i.articleId == id).ToList();
            ViewBag.article = article;
            ViewBag.attrs = article_detail;
            ViewBag.origins = article_orgin;
            ViewBag.cousins = cousins;

            return View();
        }


        //GET/Affectation des carID
        [ChildActionOnly]
        public ActionResult AffecterOENcarID()
        {

            return View();

        }

        //POST/Affectation des  carID
        [ChildActionOnly]
        [HttpPost]
        public ActionResult AffecterCarID(int id)
        {

            return View();

        }

        //POST/Afficher images 
        // [ChildActionOnly]
        public ActionResult AffImages(int id)
        {
            ViewData["id"] = id;
            return View();

        }

        //POST/Afficher images 
        [ChildActionOnly]
        [HttpPost]
        public ActionResult AffImages()
        {

            return View();

        }


        //POST/Affectation des origines 
        [ChildActionOnly]
        public ActionResult AffecterOEN()
        {

            return View();

        }

        //POST/Affectation des origines 
        [ChildActionOnly]
        [HttpPost]
        public ActionResult AffecterOEN(int id)
        {

            return View();

        }

        //GET/détailles Article
        public ActionResult Affdetailles(int id)
        {
            var article = _db.Article.Where(a => a.articleId == id).FirstOrDefault();
            var cousins = getCousins(article.articleId);
            // detail lists
            var article_detail = _db.getArticleIdsWithState.Where(g => g.articleID == id).ToList();

            // origin lists
            var article_orgin = _db.ArticleOrigins.Where(g => g.articleID == id).ToList();
            var origins = new List<OriginesOEN>();
            foreach (var ao in article_orgin)
            {
                var origin = _db.OriginesOEN.Where(d => d.oeNumber == ao.oeNumber).FirstOrDefault();
                origins.Add(origin);
            }
            List<ArticleAttributes> Attrlst = _db.ArticleAttributes.Where(g => g.articleId == id).ToList();



            ViewBag.Attrlst = Attrlst;
            ViewBag.images = _db.Images.Where(i => i.articleId == id).ToList();
            ViewBag.article = article;
            ViewBag.attrs = article_detail;
            ViewBag.origins = article_orgin;
            ViewBag.cousins = cousins;

            return View();
        }




        public List<Articles> getCousins(int id)
        {
            var sql = "select a.* from articles a where a.articleId !=" + id + " and a.articleId in (select o.articleID from articleorigins o where o.oeNumber in (select u.oeNumber from articleorigins u where u.articleID=" + id + ") )";
            List<Articles> articles = _db.Database.SqlQuery<Articles>(sql).ToList();
            return articles;
        }

        [HttpPost]
        public ActionResult Affdetailles(string carId, int articleId)
        {
            getArticleIdsWithState ga = new getArticleIdsWithState()
            {
                articleID = articleId,
                carID = carId,
                SortNo = 1
            };
            _db.getArticleIdsWithState.Add(ga);
            _db.SaveChanges();

            return RedirectToAction("Affdetailles", new { id = articleId });
        }

        //POST/détailles Article
        [ChildActionOnly]
        public ActionResult Plusdetailles(int id)
        {


            var v = _db.ArticleAttributes.Where(a => a.articleId == id).FirstOrDefault();
            return View(v);

        }

        //Modifier la table "ArticleAttributes"
        public ActionResult EditArticleAttributes(int id)
        {


            var articlesAttributesToEdit = (from m in _db.ArticleAttributes
                                            where m.articleId == id
                                            select m).First();
            return View(articlesAttributesToEdit);
        }

        //Modifier la table "ArticleAttributes"
        [HttpPost]
        public ActionResult EditArticleAttributes(ArticleAttributes articlesAttributesToEdit)
        {

            var originalArticlesAttributes = (from m in _db.ArticleAttributes
                                              where m.articleId == articlesAttributesToEdit.articleId
                                              select m).First();

            if (!ModelState.IsValid)
                return View("Index");
            _db.Entry(originalArticlesAttributes).CurrentValues.SetValues(articlesAttributesToEdit);
            _db.SaveChanges();

            return RedirectToAction("ResRecherche", new { id = articlesAttributesToEdit.articleId });



        }

        [HttpGet]
        public ActionResult Search()
        {
            ViewBag.Manufacter = _db.Manufacter.ToList();
            ViewBag.details = _db.Details.ToList();
            return View();
        }

        [HttpPost]
        public JsonResult SearchingVin(string vin)
        {
            var sqlVin = "select g.* from getarticleidswithstates g, vehicles v where g.carID = v.carId and v.VIN = '" + vin + "'";
            List<getArticleIdsWithState> articles = _db.Database.SqlQuery<getArticleIdsWithState>(sqlVin).ToList();
            return Json(articles);
        }

        [HttpPost]
        public JsonResult SearchingCar(string carid)
        {
            var sqlVin = "select g.* from getarticleidswithstates g where g.carID = '" + carid + "'";
            List<getArticleIdsWithState> articles = _db.Database.SqlQuery<getArticleIdsWithState>(sqlVin).ToList();
            return Json(articles);
        }

        [HttpPost]
        public JsonResult SearchingSerie(string serie)
        {
            var sqlVin = "select g.* from getarticleidswithstates g, vehicles v, parcars p where g.carID = v.carId and v.EntryVin = p.EntryVin and p.Serie='" + serie + "'";
            List<getArticleIdsWithState> articles = _db.Database.SqlQuery<getArticleIdsWithState>(sqlVin).ToList();
            return Json(articles);
        }

        [HttpPost]
        public JsonResult SearchingModel(string modelId)
        {

            var sqlVin = "select g.*  from getarticleidswithstates g, vehicles v where g.carID = v.carId and v.modelId = " + modelId;
            List<getArticleIdsWithState> articles = _db.Database.SqlQuery<getArticleIdsWithState>(sqlVin).ToList();
            return Json(articles);
        }

        [HttpPost]
        public JsonResult SearchingModelCarId(string modelId)
        {

            var sqlVin = "select d.*  from details d, vehicles v where d.carId  = v.carId and v.modelId =" + modelId;
            List<Details> articles = _db.Database.SqlQuery<Details>(sqlVin).ToList();
            return Json(articles);
        }

        public ActionResult AssignId(string id)
        {
            ViewBag.detail = _db.Details.Where(d => d.carId == id).FirstOrDefault();
            ViewBag.details = _db.Details.ToList();
            return View("~/Views/Article/Assign.cshtml");
        }

        public ActionResult Assign()
        {
            ViewBag.details = _db.Details.ToList();
            return View();
        }


        [HttpPost]
        public JsonResult SearchingArticle(string nba)
        {
            var sqlVin = "select a.*  from  articles a  where a.articleNo='" + nba + "'";
            List<Articles> articles = _db.Database.SqlQuery<Articles>(sqlVin).ToList();
            return Json(articles);
        }

        [HttpPost]
        public JsonResult getArtilce(int ida)
        {
            var article = _db.Article.Where(a => a.articleId == ida).FirstOrDefault();
            if (article != null)
            {
                return Json(article);
            }
            return Json("error");
        }

        public JsonResult assignDetail(string idCar, int articleId)
        {
            getArticleIdsWithState gs = _db.getArticleIdsWithState
                .Where(g => g.articleID == articleId && g.carID == idCar).FirstOrDefault();

            if (gs != null)
            {
                if (gs.SortNo == 1)
                    return Json("exist");
                else
                {
                    getArticleIdsWithState gn = gs;
                    gn.SortNo = 1;
                    _db.Entry(gs).CurrentValues.SetValues(gn);
                    _db.SaveChanges();
                    return Json(gn);
                }

            }
            else
            {
                getArticleIdsWithState ga = new getArticleIdsWithState()
                {
                    articleID = articleId,
                    carID = idCar,
                    SortNo = 1
                };
                _db.getArticleIdsWithState.Add(ga);
                _db.SaveChanges();
                return Json(ga);
            }
        }


    }
}
