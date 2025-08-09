using AlumniPlatform.Models;
using AlumniPlatform.Models.user;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
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

            ViewBag.CampusID = new SelectList(db.Universitycampuses, "Id", "Name", alumni.UniversitycampusID);

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
            ViewBag.CampusID = new SelectList(db.Universitycampuses, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Alumni alumni)
        {
            if (ModelState.IsValid)
            {
                // 设置默认值
                alumni.Id = Guid.NewGuid().ToString();
                alumni.UserName = alumni.Email; // 使用邮箱作为用户名
                alumni.EmailConfirmed = true; // 简化流程

                db.Alumni.Add(alumni);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(alumni);
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
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Debug.WriteLine($"实体 \"{eve.Entry.Entity.GetType().Name}\" 在状态 \"{eve.Entry.State}\" 时验证失败：");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Debug.WriteLine($"- 属性: \"{ve.PropertyName}\", 错误信息: \"{ve.ErrorMessage}\"");
                        }
                    }
                    throw; // 保留原始异常
                }

                return RedirectToAction("Index");
            }
            return View(@alumni);
        }
    }
}