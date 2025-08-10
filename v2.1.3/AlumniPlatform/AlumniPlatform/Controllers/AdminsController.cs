using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlumniPlatform.Models;
using AlumniPlatform.Models.user;

namespace AlumniPlatform.Controllers
{
    public class AdminsController : Controller
    {
        private AlumniNetworkingPlatformContext db = new AlumniNetworkingPlatformContext();

        // GET: Admins
        public ActionResult Index()
        {
            var admins = db.Admins.Include(a => a.Universitycampus).ToList();
            return View(admins);
        }

        // GET: Admins/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(@admin);
        }

        // GET: Admins/Create
        public ActionResult Create()
        {
            // 将 ViewBag.UniversitycampusID 改为 ViewBag.CampusID
            ViewBag.CampusID = new SelectList(db.Universitycampuses, "ID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,UserName,Department,UniversitycampusID,CanManageUsers,CanManageContent,CanManageEvents")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Admins.Add(admin); // 使用 db.Admins 而不是 db.Users
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CampusID = new SelectList(db.Universitycampuses, "ID", "Name", admin.UniversitycampusID);
            return View(@admin);
        }


        // GET: Admins/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            ViewBag.CampusID = new SelectList(db.Universitycampuses, "ID", "Name", admin.UniversitycampusID);
            return View(@admin);
        }

        // POST: Admins/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性；有关
        // 更多详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Email,UserName,Department,UniversitycampusID,CanManageUsers,CanManageContent,CanManageEvents")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UniversitycampusID = new SelectList(db.Universitycampuses, "ID", "Name", admin.UniversitycampusID);
            return View(admin);
        }

        // GET: Admins/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admins.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Admin admin = db.Admins.Find(id);
            db.Users.Remove(admin);
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
