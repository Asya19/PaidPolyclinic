using PaidPolyclinic.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PaidPolyclinic.Controllers
{
    public class ReceptionsController : Controller
    {
        private PaidPolyclinicDBEntities db = new PaidPolyclinicDBEntities();

        // GET: Receptions // Отображение списка всех обращений
        public ActionResult Index()
        {
            var receptions = db.Receptions.Include(r => r.Doctor).Include(r => r.Patient);
            return View(receptions.ToList());
        }

        // GET: Receptions/Details/5 // Отображение деталей обращения
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

        // GET: Receptions/Create // Отображение формы для создания обращения
        public ActionResult Create()
        {
            ViewBag.DoctorID = new SelectList(db.Doctors, "Id", "LastName");
            ViewBag.PatientID = new SelectList(db.Patients, "Id", "LastName");
            return View();
        }

        // POST: Receptions/Create // Сохранение в БД информации об обращении
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

        // GET: Receptions/Edit/5 // Отображение формы редактирования обращения
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

        // POST: Receptions/Edit/5 // Сохранение изменений информации об обращении в БД
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

        // GET: Receptions/Delete/5 // Отображение предупридительного окна с информаией, которую необходимо удалить
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

        // POST: Receptions/Delete/5  // Удаление обращения из БД
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
