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
    public class CommentsController : Controller
    {
        private ShopContext db = new ShopContext();

        // GET: Comments
        public ActionResult Index(int id)
        {
            var res = from Comments in db.Com
                      select Comments;
            List<Comments> coco = new List<Comments>();
            foreach(Comments c in res)
            {
                if(c.Id_prod == id)
                {
                    coco.Add(c);
                }
            }
            return View(coco.ToList());
        }

        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Com.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // GET: Comments/Create
        public ActionResult Create()
        {
            ViewBag.Id_prod = new SelectList(db.Product, "Id_prod", "Name_produits");
            return View();
        }

        // POST: Comments/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_com,Id_prod,Comment")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                db.Com.Add(comments);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.Id_prod = new SelectList(db.Product, "Id_prod", "Name_produits", comments.Id_prod);
            return View(comments);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Com.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_prod = new SelectList(db.Product, "Id_prod", "Name_produits", comments.Id_prod);
            return View(comments);
        }

        // POST: Comments/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_com,Id_prod,Comment")] Comments comments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_prod = new SelectList(db.Product, "Id_prod", "Name_produits", comments.Id_prod);
            return View(comments);
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comments comments = db.Com.Find(id);
            if (comments == null)
            {
                return HttpNotFound();
            }
            return View(comments);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comments comments = db.Com.Find(id);
            db.Com.Remove(comments);
            db.SaveChanges();
            return RedirectToAction("Index","Produits");
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
