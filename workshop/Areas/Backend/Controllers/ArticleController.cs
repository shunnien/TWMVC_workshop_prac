using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using workshop.Models;
using PagedList;

namespace workshop.Areas.Backend.Controllers
{
    public class ArticleController : Controller
    {
        private WorkshopEntities db = new WorkshopEntities();

        // GET: Backend/Article
        public ActionResult Index(int page = 1, int count = 20, string keyword = null, string column = null, string sort = "Ascending")
        {
            var articles = db.Articles.Include(a => a.Category);

            if (!string.IsNullOrEmpty(keyword))
            {
                articles = articles.Where(a => a.Subject.Contains(keyword)
                                               ||
                                               a.Summary.Contains(keyword)
                                               ||
                                               a.ContentText.Contains(keyword)
                                               ||
                                               a.Category.Name.Contains(keyword));
            }

            articles = articles.OrderBy(a => a.ID);

            if (!string.IsNullOrEmpty(column))
            {
                switch (column)
                {
                    case "Subject":
                        if (sort == "Ascending")
                            articles = articles.OrderBy(a => a.Subject);
                        else
                            articles = articles.OrderByDescending(a => a.Subject);
                        break;

                    case "Category.Name":
                        if (sort == "Ascending")
                            articles = articles.OrderBy(a => a.Category.Name);
                        else
                            articles = articles.OrderByDescending(a => a.Category.Name);
                        break;

                    case "PublishDate":
                        if (sort == "Ascending")
                            articles = articles.OrderBy(a => a.PublishDate);
                        else
                            articles = articles.OrderByDescending(a => a.PublishDate);
                        break;

                    case "CreateDate":
                        if (sort == "Ascending")
                            articles = articles.OrderBy(a => a.CreateDate);
                        else
                            articles = articles.OrderByDescending(a => a.CreateDate);
                        break;

                    case "UpdateDate":
                        if (sort == "Ascending")
                            articles = articles.OrderBy(a => a.UpdateDate);
                        else
                            articles = articles.OrderByDescending(a => a.UpdateDate);
                        break;
                }
            }

            return View(articles.ToPagedList(page, count));
        }


        // GET: Backend/Article/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Backend/Article/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Backend/Article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]      //跳過資料驗證檢查
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,CategoryID,Subject,Summary,ContentText,IsPublish,PublishDate,ViewCount,CreateUser,CreateDate,UpdateUser,UpdateDate")] Article article)
        public ActionResult Create([Bind(Exclude = "ID,CreateUser,CreateDate,UpdateUser,UpdateDate")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.ID = Guid.NewGuid();
                article.CreateDate = DateTime.Now;
                article.UpdateDate = DateTime.Now;
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", article.CategoryID);
            return View(article);
        }

        // GET: Backend/Article/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", article.CategoryID);
            return View(article);
        }

        // POST: Backend/Article/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]      //跳過資料驗證檢查
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                var instance = db.Articles.FirstOrDefault(x => x.ID == article.ID);

                instance.CategoryID = article.CategoryID;
                instance.Subject = article.Subject;
                instance.Summary = article.Summary;
                instance.ContentText = article.ContentText;
                instance.PublishDate = article.PublishDate;
                instance.IsPublish = article.IsPublish;
                instance.UpdateDate = DateTime.Now;

                db.Entry(instance).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", article.CategoryID);
            return View(article);
            //var isExists = db.Articles
            //    .Any(x => x.ID != article.ID);

            //if (isExists)
            //{
            //    ModelState.AddModelError("ID", "Article ID is not exist");
            //    return View(article);
            //}

            //if (ModelState.IsValid)
            //{
            //    article.UpdateDate = DateTime.Now;
            //    db.Entry(article).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", article.CategoryID);
            //return View(article);
        }

        // GET: Backend/Article/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Backend/Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
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
    }
}
