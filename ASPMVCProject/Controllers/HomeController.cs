using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASPMVCProject.Model.DbModel;
using ASPMVCProject.Model.StudentManagement;

namespace ASPMVCProject.Controllers
{
    public class HomeController : Controller
    {
        private StudentEntities db = new StudentEntities();

        // GET: Home
        public ActionResult Index()
        {
            return View(db.StudentLists.ToList());
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentList studentList = db.StudentLists.Find(id);
            if (studentList == null)
            {
                return HttpNotFound();
            }
            return View(studentList);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Department")] AddStudent student)
        {
            StudentList studentList = new StudentList();
            studentList.Name = student.Name;
            studentList.Department = student.Department;
            if (ModelState.IsValid)
            {
                db.StudentLists.Add(studentList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentList studentList = db.StudentLists.Find(id);
            if (studentList == null)
            {
                return HttpNotFound();
            }
            return View(studentList);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Department")] StudentList studentList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentList);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentList studentList = db.StudentLists.Find(id);
            if (studentList == null)
            {
                return HttpNotFound();
            }
            return View(studentList);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentList studentList = db.StudentLists.Find(id);
            db.StudentLists.Remove(studentList);
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
