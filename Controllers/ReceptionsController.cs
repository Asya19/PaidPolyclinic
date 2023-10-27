using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PaidPolyclinic.Models;

namespace PaidPolyclinic.Controllers
{
    public class ReceptionsController : Controller
    {
        private PaidPolyclinicDBEntities db = new PaidPolyclinicDBEntities();

        // GET: Receptions
        public ActionResult Index()
        {
            var receptions = db.Receptions.Include(r => r.Doctor).Include(r => r.Patient);
            return View(receptions.ToList());
        }

        // GET: Receptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reception reception = db.Receptions.Find(id);
            if (reception == null)
            {
                return HttpNotFound();
            }
            return View(reception);
        }

        // GET: Receptions/Create
        public ActionResult Create()
        {
            ViewBag.DoctorID = new SelectList(db.Doctors, "Id", "LastName");
            ViewBag.PatientID = new SelectList(db.Patients, "Id", "LastName");
            return View();
        }

        // POST: Receptions/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DoctorID,PatientID,ReceptionDate,Diagnosis,Price")] Reception reception)
        {
            if (ModelState.IsValid)
            {
                db.Receptions.Add(reception);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorID = new SelectList(db.Doctors, "Id", "LastName", reception.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "Id", "LastName", reception.PatientID);
            return View(reception);
        }

        // GET: Receptions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reception reception = db.Receptions.Find(id);
            if (reception == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "Id", "LastName", reception.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "Id", "LastName", reception.PatientID);
            return View(reception);
        }

        // POST: Receptions/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DoctorID,PatientID,ReceptionDate,Diagnosis,Price")] Reception reception)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reception).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "Id", "LastName", reception.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "Id", "LastName", reception.PatientID);
            return View(reception);
        }

        // GET: Receptions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reception reception = db.Receptions.Find(id);
            if (reception == null)
            {
                return HttpNotFound();
            }
            return View(reception);
        }

        // POST: Receptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reception reception = db.Receptions.Find(id);
            db.Receptions.Remove(reception);
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
