﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ISPYZU.Models;

namespace ISPYZU.Areas.Login.Controllers
{
    public class StudentsController : Controller
    {
        private TestDbContext db = new TestDbContext();

        // GET: Login/Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Colleges).Include(s => s.Majors);
            return View(students.ToList());
        }

        // GET: Login/Students/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Login/Students/Create
        public ActionResult Create()
        {
            ViewBag.CollageId = new SelectList(db.Colleges, "CollegeId", "CollegeName");
            ViewBag.MajorId = new SelectList(db.Majors, "MajorId", "MajorName");
            return View();
        }

        // POST: Login/Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,Name,Gender,MajorId,CollageId,Session,Password,Email")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CollageId = new SelectList(db.Colleges, "CollegeId", "CollegeName", student.CollageId);
            ViewBag.MajorId = new SelectList(db.Majors, "MajorId", "MajorName", student.MajorId);
            return View(student);
        }

        // GET: Login/Students/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.CollageId = new SelectList(db.Colleges, "CollegeId", "CollegeName", student.CollageId);
            ViewBag.MajorId = new SelectList(db.Majors, "MajorId", "MajorName", student.MajorId);
            return View(student);
        }

        // POST: Login/Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,Name,Gender,MajorId,CollageId,Session,Password,Email")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CollageId = new SelectList(db.Colleges, "CollegeId", "CollegeName", student.CollageId);
            ViewBag.MajorId = new SelectList(db.Majors, "MajorId", "MajorName", student.MajorId);
            return View(student);
        }

        // GET: Login/Students/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Login/Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.Find(id);
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
}
