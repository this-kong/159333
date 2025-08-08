using AlumniPlatform.Models;
using AlumniPlatform.Models.user;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

public class StudentsController : Controller
{
    private AlumniNetworkingPlatformContext db = new AlumniNetworkingPlatformContext();

    // GET: Students
    public ActionResult Index()
    {
        // 只获取学生用户
        var students = db.Students.Include(s => s.Universitycampus).ToList();
        return View(@students);
    }

    // GET: Students/Details/5
    public ActionResult Details(string id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        // 直接从学生集合中查找
        Student student = db.Students.Find(id);
        if (student == null)
        {
            return HttpNotFound();
        }
        return View(@student);
    }

    // GET: Students/Create
    public ActionResult Create()
    {
        // 使用一致的ViewBag名称
        ViewBag.CampusID = new SelectList(db.Universitycampuses, "ID", "Name");
        return View();
    }

    // POST: Students/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,Name,Email,UserName,StudentID,Major,GraduationYear,AcademicInterests,CareerGoals,UniversitycampusID")] Student student)
    {
        if (ModelState.IsValid)
        {
            // 添加到学生集合
            db.Students.Add(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        ViewBag.CampusID = new SelectList(db.Universitycampuses, "ID", "Name", student.UniversitycampusID);
        return View(student);
    }

    // GET: Students/Edit/5
    public ActionResult Edit(string id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        // 直接从学生集合中查找
        Student student = db.Students.Find(id);
        if (student == null)
        {
            return HttpNotFound();
        }
        ViewBag.CampusID = new SelectList(db.Universitycampuses, "ID", "Name", student.UniversitycampusID);
        return View(@student);
    }

    // POST: Students/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Name,Email,UserName,StudentID,Major,GraduationYear,AcademicInterests,CareerGoals,UniversitycampusID")] Student student)
    {
        if (ModelState.IsValid)
        {
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.CampusID = new SelectList(db.Universitycampuses, "ID", "Name", student.UniversitycampusID);
        return View(student);
    }

    // GET: Students/Delete/5
    public ActionResult Delete(string id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        // 直接从学生集合中查找
        Student student = db.Students.Find(id);
        if (student == null)
        {
            return HttpNotFound();
        }
        return View(@student);
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(string id)
    {
        Student student = db.Students.Find(id);
        // 从学生集合中删除
        db.Students.Remove(student);
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