using AlumniPlatform.Models;
using AlumniPlatform.Models.user;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace AlumniPlatform.Controllers
{
    public class AlumniController : Controller
    {
        private AlumniNetworkingPlatformContext db = new AlumniNetworkingPlatformContext();


        //// GET: Alumni
        //public ActionResult Index()
        //{
        //    //// 获取所有校友并按毕业年份降序排列
        //    //var alumniList = db.Alumni
        //    //    .OrderByDescending(a => a.GraduationYear)
        //    //    .ToList();

        //    //return View(alumniList);
        //}

        // GET: Alumni/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Alumni alumni = db.Alumni.Find(id);

            if (alumni == null)
            {
                return HttpNotFound();
            }

            return View(@alumni);
        }

        public ActionResult Index(string searchString, string industryFilter, int? graduationYear)
        {
            // 获取所有校友
            var alumni = from a in db.Alumni select a;

            // 应用搜索条件
            if (!String.IsNullOrEmpty(searchString))
            {
                alumni = alumni.Where(a =>
                    a.Name.Contains(searchString) ||
                    a.CurrentCompany.Contains(searchString) ||
                    a.Skills.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(industryFilter))
            {
                alumni = alumni.Where(a => a.Industry == industryFilter);
            }

            if (graduationYear.HasValue)
            {
                alumni = alumni.Where(a => a.GraduationYear == graduationYear.Value);
            }

            // 获取行业列表用于下拉框
            ViewBag.IndustryList = new SelectList(
                db.Alumni.Select(a => a.Industry).Distinct().ToList()
            );

            // 获取毕业年份列表用于下拉框
            ViewBag.GraduationYears = new SelectList(
                db.Alumni.Select(a => a.GraduationYear).Distinct().OrderByDescending(y => y).ToList()
            );

            return View(alumni.OrderByDescending(a => a.GraduationYear).ToList());
        }

        // GET: Alumni/Edit/5

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Alumni alumni = db.Alumni.Find(id);

            if (alumni == null)
            {
                return HttpNotFound();
            }

            return View(@alumni);
        }

        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumni alumni = db.Alumni.Find(id);
            if (alumni == null)
            {
                return HttpNotFound();
            }
            return View(@alumni);
        }

        //create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alumni/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,GraduationYear,Degree,CurrentCompany,CurrentPosition,Industry,Skills,IsMentor,CareerHistory")] Alumni alumni)
        {
            if (ModelState.IsValid)
            {
                // 标记实体为已修改
                db.Entry(alumni).State = EntityState.Modified;

                // 保存更改
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(@alumni);
        }
    }
}