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
using PagedList;

namespace Projet2020.Controllers
{
    public class ProduitsController : Controller
    {
        private ShopContext db = new ShopContext();

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewBag.fnameSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var produits = from s in db.Product
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                produits = produits.Where(s => s.Name_produits.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Name":
                    produits = produits.OrderByDescending(s => s.Name_produits);
                    break;
                case "Price":
                    produits = produits.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    produits = produits.OrderByDescending(s => s.Price);
                    break;
                default:
                    produits = produits.OrderBy(s => s.Name_produits);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(produits.ToPagedList(pageNumber, pageSize));
        }

        // GET: Produits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produits produits = db.Product.Find(id);
            if (produits == null)
            {
                return HttpNotFound();
            }
            return View(produits);
        }

        // GET: Produits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produits/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_prod,Name_produits,Price,Stk")] Produits produits)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(produits);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(produits);
        }

        // GET: Produits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produits produits = db.Product.Find(id);
            if (produits == null)
            {
                return HttpNotFound();
            }
            return View(produits);
        }

        // POST: Produits/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_prod,Name_produits,Price,Stk")] Produits produits)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produits).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produits);
        }

        // GET: Produits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produits produits = db.Product.Find(id);
            if (produits == null)
            {
                return HttpNotFound();
            }
            return View(produits);
        }

        // POST: Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produits produits = db.Product.Find(id);
            db.Product.Remove(produits);
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
        //commandes de prod
        public ActionResult Add(int id)
        {

            var test = from Commandes in db.orders
                       select Commandes;
            List<Commandes> otest = new List<Commandes>();
            foreach (Commandes ot in test)
            {
                if (ot.Id_cli == int.Parse(Session["id"].ToString()) && ot.check == 0)
                {
                    otest.Add(ot);
                }
            }

                if (otest.Count() == 0)
            {
                Session["coco"] = 1;
                Commandes c = new Commandes { Id_cli = int.Parse(Session["id"].ToString()), check = 0 };
                db.orders.Add(c);
                db.SaveChanges();
                var res = from Commandes in db.orders
                          select Commandes;
                Commandes co = null;
                foreach (Commandes cc in res)
                {
                    if (cc.Id_cli == int.Parse(Session["id"].ToString()) && cc.check == 0)
                    {
                        co = cc;
                    }
                }
                Panier p = new Panier { Id_commande = co.Id_commande, Id_prod = id };
                db.Paniers.Add(p);
                db.SaveChanges();
            }
            else
            {
                var res = from Commandes in db.orders
                          select Commandes;
                Commandes co = null;
                foreach (Commandes cc in res)
                {
                    if (cc.Id_cli == int.Parse(Session["id"].ToString()) && cc.check == 0)
                    {
                        co = cc;
                    }
                }
                Panier p = new Panier { Id_commande = co.Id_commande, Id_prod = id };
                db.Paniers.Add(p);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult commentaire(int id)
        {
            return RedirectToAction("Index", "Comments", new { id = id });
        }
    }
}
