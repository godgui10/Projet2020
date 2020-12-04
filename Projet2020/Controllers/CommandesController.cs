using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Projet2020.DAL;
using Projet2020.Models;

namespace Projet2020.Controllers
{
    public class CommandesController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: Commandes
        public ActionResult Index()
        {
            
            return View(db.orders.ToList());
        }

        // GET: Commandes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commandes commandes = db.orders.Find(id);
            if (commandes == null)
            {
                return HttpNotFound();
            }
            return View(commandes);
        }

        // GET: Commandes/Create
        public ActionResult Create()
        {
            ViewBag.Id_cli = new SelectList(db.Client, "Id_cli", "Firstname");
            return View();
        }

        // POST: Commandes/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_commande,Id_cli")] Commandes commandes)
        {
            if (ModelState.IsValid)
            {
                db.orders.Add(commandes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_cli = new SelectList(db.Client, "Id_cli", "Firstname", commandes.Id_cli);
            return View(commandes);
        }

        // GET: Commandes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commandes commandes = db.orders.Find(id);
            if (commandes == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_cli = new SelectList(db.Client, "Id_cli", "Firstname", commandes.Id_cli);
            return View(commandes);
        }

        // POST: Commandes/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_commande,Id_cli")] Commandes commandes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commandes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_cli = new SelectList(db.Client, "Id_cli", "Firstname", commandes.Id_cli);
            return View(commandes);
        }

        // GET: Commandes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commandes commandes = db.orders.Find(id);
            if (commandes == null)
            {
                return HttpNotFound();
            }
            return View(commandes);
        }

        // POST: Commandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Commandes commandes = db.orders.Find(id);
            db.orders.Remove(commandes);
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
