using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_assginment.Models;

namespace FIT5032_assginment.Controllers
{
    public class CoachesController : Controller
    {
        private Entities db = new Entities();

        // GET: Coaches
        public ActionResult Index(int? id)
        {


            var coach = (from m in db.Coach where (m.SwimmingPoolID == id) select m); 
            return View(coach.ToList());
        }

        // GET: Coaches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coach.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // GET: Coaches/Create
        public ActionResult Create()
        {
            ViewBag.SwimmingPoolID = new SelectList(db.SwimmingPool, "Id", "Name");
            return View();
        }

        // POST: Coaches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CoachName,CoachDescription,CoachPhone,SwimmingPoolID")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                db.Coach.Add(coach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SwimmingPoolID = new SelectList(db.SwimmingPool, "Id", "Name", coach.SwimmingPoolID);
            return View(coach);
        }

        // GET: Coaches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coach.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            ViewBag.SwimmingPoolID = new SelectList(db.SwimmingPool, "Id", "Name", coach.SwimmingPoolID);
            return View(coach);
        }

        // POST: Coaches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CoachName,CoachDescription,CoachPhone,SwimmingPoolID")] Coach coach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SwimmingPoolID = new SelectList(db.SwimmingPool, "Id", "Name", coach.SwimmingPoolID);
            return View(coach);
        }

        // GET: Coaches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coach coach = db.Coach.Find(id);
            if (coach == null)
            {
                return HttpNotFound();
            }
            return View(coach);
        }

        // POST: Coaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coach coach = db.Coach.Find(id);
            db.Coach.Remove(coach);
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
