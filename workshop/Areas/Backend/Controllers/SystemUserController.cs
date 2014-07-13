using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using workshop.Models;

namespace workshop.Areas.Backend.Controllers
{
    public class SystemUserController : Controller
    {
        private WorkshopEntities db = new WorkshopEntities();

        // GET: Backend/SystemUser
        public ActionResult Index()
        {
            return View(db.SystemUsers.ToList());
        }

        // GET: Backend/SystemUser/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemUser systemUser = db.SystemUsers.Find(id);
            if (systemUser == null)
            {
                return HttpNotFound();
            }
            return View(systemUser);
        }

        // GET: Backend/SystemUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Backend/SystemUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID,Name,Account,Password,Email,CreateUser,CreateDate,UpdateUser,UpdateDate")] SystemUser systemUser)
        public ActionResult Create([Bind(Include= "Name,Account,Password,Email")] SystemUser systemUser)
        {
            var NameisExists = db.SystemUsers
                .Any(x => x.Name == systemUser.Name );

            if (NameisExists)
            {
                ModelState.AddModelError("Name", "SystemUsers Name Repeat");
                return View(systemUser);
            }

            var AccountisExists = db.SystemUsers
                .Any(x => x.Account == systemUser.Account);
            if (NameisExists)
            {
                ModelState.AddModelError("Account", "SystemUsers Account Repeat");
                return View(systemUser);
            }

            if (ModelState.IsValid)
            {
                systemUser.ID = Guid.NewGuid();
                systemUser.CreateDate = DateTime.Now;
                systemUser.UpdateDate = DateTime.Now;
                db.SystemUsers.Add(systemUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(systemUser);
        }

        // GET: Backend/SystemUser/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemUser systemUser = db.SystemUsers.Find(id);
            if (systemUser == null)
            {
                return HttpNotFound();
            }
            return View(systemUser);
        }

        // POST: Backend/SystemUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Account,Password,Email")] SystemUser systemUser)
        {
            if (ModelState.IsValid)
            {
                systemUser.UpdateDate = DateTime.Now;
                db.Entry(systemUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(systemUser);
        }

        // GET: Backend/SystemUser/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SystemUser systemUser = db.SystemUsers.Find(id);
            if (systemUser == null)
            {
                return HttpNotFound();
            }
            return View(systemUser);

        }

        // POST: Backend/SystemUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SystemUser systemUser = db.SystemUsers.Find(id);
            db.SystemUsers.Remove(systemUser);
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
