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
    public class SwimmingPoolsController : Controller
    {
        private Entities db = new Entities();

        // GET: SwimmingPools
        public ActionResult Index()
        {
            return View(db.SwimmingPool.ToList());
        }

        // GET: SwimmingPools/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SwimmingPool swimmingPool = db.SwimmingPool.Find(id);
            if (swimmingPool == null)
            {
                return HttpNotFound();
            }
            return View(swimmingPool);
        }

        // GET: SwimmingPools/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SwimmingPools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address")] SwimmingPool swimmingPool)
        {
            if (ModelState.IsValid)
            {
                db.SwimmingPool.Add(swimmingPool);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(swimmingPool);
        }

        // GET: SwimmingPools/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SwimmingPool swimmingPool = db.SwimmingPool.Find(id);
            if (swimmingPool == null)
            {
                return HttpNotFound();
            }
            return View(swimmingPool);
        }

        // POST: SwimmingPools/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Address")] SwimmingPool swimmingPool)
        {
            if (ModelState.IsValid)
            {
                db.Entry(swimmingPool).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(swimmingPool);
        }

        // GET: SwimmingPools/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SwimmingPool swimmingPool = db.SwimmingPool.Find(id);
            if (swimmingPool == null)
            {
                return HttpNotFound();
            }
            return View(swimmingPool);
        }

        // POST: SwimmingPools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SwimmingPool swimmingPool = db.SwimmingPool.Find(id);
            db.SwimmingPool.Remove(swimmingPool);
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
