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
    public class ClientsController : Controller
    {
        private ShopContext db = new ShopContext();

        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.fnameSortParm = sortOrder == "firstname" ? "firstname_desc" : "firstname";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var client = from s in db.Client
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                client = client.Where(s => s.Name.Contains(searchString)
                                       || s.Firstname.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name":
                    client = client.OrderByDescending(s => s.Name);
                    break;
                case "Firstname":
                    client = client.OrderBy(s => s.Firstname);
                    break;
                case "firstname_desc":
                    client = client.OrderByDescending(s => s.Firstname);
                    break;
                default:
                    client = client.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(client.ToPagedList(pageNumber,pageSize));
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Client.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Firstname,Name,Email,Adress")] Clients clients)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Client.Add(clients);
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
            }
            catch(DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(clients);
        }


        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Client.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // POST: Clients/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_cli,Firstname,Name,Email,Adress")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clients).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clients);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Clients clients = db.Client.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clients clients = db.Client.Find(id);
            db.Client.Remove(clients);
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

        //get login
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(String Email, String Name)
        {
            if (ModelState.IsValid)
            {
                var data = db.Client.Where(s => s.Email.Equals(Email) && s.Name.Equals(Name));
                System.Diagnostics.Debug.WriteLine(Name);
                if (data.Count() > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Connection On");
                    Session["id"] = data.FirstOrDefault().Id_cli;
                    Session["Firstanme"] = data.FirstOrDefault().Firstname;
                    Session["Name"] = data.FirstOrDefault().Name;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Connection Off");
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login", "Clients");
                }
            }
            return View();
        }

        //logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
