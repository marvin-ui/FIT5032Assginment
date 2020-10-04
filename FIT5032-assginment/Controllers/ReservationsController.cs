using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_assginment.Models;
using FIT5032_assginment.ViewModels;
using Microsoft.AspNet.Identity;

namespace FIT5032_assginment.Controllers
{
    public class ReservationsController : Controller
    {
        private Entities db = new Entities();

        // GET: Reservations
        public ActionResult Index()
        {
            var reservation = db.Reservation.Include(r => r.Coach);
            return View(reservation.ToList());
        }

        public ActionResult SelectSwimmingPool()
        {
            return View(db.SwimmingPool.ToList());
        }

        public ActionResult SelectCoach()
        {
            return View(db.Coach.ToList());
        }

        // GET: Reservations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create(Nullable<int> id)
        {
            var reservation = new ReservationsViewModel();
            var couchId = id;
            if (id != null)
            {
                ViewBag.CoachId = couchId;
                ViewBag.CoachName = db.Coach.Find(couchId).CoachName;
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //ViewBag.CoachId = new SelectList(db.Coach, "Id", "CoachName");
            return View(reservation);
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,StartTime,EndTime,UserId,CoachId,ReservationDesc")]
        public ActionResult Create(ReservationsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reservation = new Reservation
                {
                    Id = model.Id,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    UserId = User.Identity.GetUserId(),
                    CoachId = model.CoachId,
                    ReservationDesc = model.ReservationDesc
                };
                db.Reservation.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.CoachId = new SelectList(db.Coach, "Id", "CoachName", reservation.CoachId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StartTime,EndTime,UserId,CoachId,ReservationDesc")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CoachId = new SelectList(db.Coach, "Id", "CoachName", reservation.CoachId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservation.Find(id);
            db.Reservation.Remove(reservation);
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
