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
            var res = from Commandes in db.orders
                      select Commandes;
            List<Commandes> comm = new List<Commandes>();
            foreach (Commandes co in res)
            {
                if (co.Id_cli == int.Parse(Session["id"].ToString()) && co.check == 1)
                {
                    comm.Add(co);
                }
            }
            return View(comm.ToList());
        }

        // GET: Commandes/Details/5
        public ActionResult Details(int? id)
        {
            var result = from Orders in db.orders
                         select Orders;

            Commandes Order = null;
            List<Panier> CO = new List<Panier>();
            List<Produits> prod = new List<Produits>();

            foreach (Commandes o in result)
            {
                if (o.Id_cli == int.Parse(Session["id"].ToString()) && o.check == 1)
                {
                    Order = o;
                    //System.Diagnostics.Debug.WriteLine(Order.Id_commande);
                }
            }
            var result2 = from Panier in db.Paniers
                          select Panier;
            if (Order != null)
            {
                foreach (Panier co in result2)
                {

                    if (co.Id_commande == Order.Id_commande)
                    {
                        CO.Add(co);
                        //System.Diagnostics.Debug.WriteLine(co.Id_commande);
                    }
                }


                var result3 = from Produits in db.Product
                              select Produits;
                for (int i = 0; i < CO.Count; i++)
                {
                    foreach (Produits p in result3)
                    {
                        prod.Add(p);
                    }
                }

                for (int j = 0; j < CO.Count; j++)
                {
                    for (int i = 0; i < prod.Count; i++)
                    {
                        if (CO[j].Id_prod == prod[i].Id_prod)
                        {
                            CO[j].p = prod[i];
                            //System.Diagnostics.Debug.WriteLine(CO[j].p.Name_produits);
                        }
                    }
                }

                for (int i = 0; i < CO.Count; i++)
                {
                    if (CO[i].Id_commande == Order.Id_commande)
                    {
                        Order.Prod.Add(CO[i].p);
                    }
                }
            }
            return View(Order);
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
                return RedirectToAction("index");
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
        //Panier
        public ActionResult Panier()
        {
            var result = from Orders in db.orders
                         select Orders;

            Commandes Order = null;
            List<Panier> CO = new List<Panier>();
            List<Produits> prod = new List<Produits>();

            foreach (Commandes o in result)
            {
                if (o.Id_cli == int.Parse(Session["id"].ToString()) && o.check == 0)
                {
                    Order = o;
                    System.Diagnostics.Debug.WriteLine(Order.Id_commande);
                }
            }
            var result2 = from Panier in db.Paniers
                          select Panier;
            if (Order != null)
            {
                foreach (Panier co in result2)
                {

                    if (co.Id_commande == Order.Id_commande)
                    {
                        CO.Add(co);
                        System.Diagnostics.Debug.WriteLine(co.Id_commande);
                    }
                }


                var result3 = from Produits in db.Product
                              select Produits;
                for (int i = 0; i < CO.Count; i++)
                {
                    foreach (Produits p in result3)
                    {
                        prod.Add(p);
                    }
                }

                for (int j = 0; j < CO.Count; j++)
                {
                    for (int i = 0; i < prod.Count; i++)
                    {
                        if (CO[j].Id_prod == prod[i].Id_prod)
                        {
                            CO[j].p = prod[i];
                            System.Diagnostics.Debug.WriteLine(CO[j].p.Name_produits);
                        }
                    }
                }

                for (int i = 0; i < CO.Count; i++)
                {
                    if (CO[i].Id_commande == Order.Id_commande)
                    {
                        Order.Prod.Add(CO[i].p);
                    }
                }
            }
            return View(Order);
        }
        //validate
        public ActionResult validate(int id)
        {
            Commandes comm = db.orders.Find(id);
            comm.check = 1;
            db.Entry(comm).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Commandes");
        }
    }
}
