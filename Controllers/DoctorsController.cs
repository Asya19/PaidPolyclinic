using PaidPolyclinic.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows;

namespace PaidPolyclinic.Controllers
{
    public class DoctorsController : Controller
    {
        private PaidPolyclinicDBEntities db = new PaidPolyclinicDBEntities();

        // GET: Doctors // Отображение списка всех врачей
        public ActionResult Index()
        {
            var doctors = db.Doctors.Include(d => d.Category).Include(d => d.Specialization);
            return View(doctors.ToList());
        }

        // GET: Doctors/Details/5 // Отображение деталей карточки врача
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Doctors/Create // Отображение формы для создания карточки врача
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "CategoryName");
            ViewBag.SpecializationID = new SelectList(db.Specializations, "Id", "SpecializationName");
            return View();
        }

        // POST: Doctors/Create // Сохранение в БД информации о враче
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LastName,FirstName,MiddleName,SpecializationID,CategoryID,Image")] Doctor doctor, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    using (BinaryReader reader = new BinaryReader(upload.InputStream))
                    {
                        doctor.Image = reader.ReadBytes(upload.ContentLength);
                    }
                }
                db.Doctors.Add(doctor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "CategoryName", doctor.CategoryID);
            ViewBag.SpecializationID = new SelectList(db.Specializations, "Id", "SpecializationName", doctor.SpecializationID);
            return View(doctor);
        }

        // GET: Doctors/Edit/5 // Отображение формы редактирования карточки врача
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "CategoryName", doctor.CategoryID);
            ViewBag.SpecializationID = new SelectList(db.Specializations, "Id", "SpecializationName", doctor.SpecializationID);
            return View(doctor);
        }


        // POST: Doctors/Edit/5 // Сохранение изменений информации о враче в БД 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LastName,FirstName,MiddleName,SpecializationID,CategoryID,Image")] Doctor doctor, HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    db.Entry(doctor).State = EntityState.Modified;
                    if (upload != null && upload.ContentLength > 0)
                    {
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                            doctor.Image = reader.ReadBytes(upload.ContentLength);
                        }
                        db.SaveChanges();
                    }

                    else
                    {
                        db.Entry(doctor).Property(m => m.Image).IsModified = false;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
                return View(doctor);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

            finally { }
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "CategoryName", doctor.CategoryID);
            ViewBag.SpecializationID = new SelectList(db.Specializations, "Id", "SpecializationName", doctor.SpecializationID);
            return View(doctor);
        }

        // GET: Doctors/Delete/5 // Отображение предупридительного окна с информаией, которую необходимо удалить
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5 // Удаление карточки врача из БД
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
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
