using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SearchNameCodeFirst.DAL;
using SearchNameCodeFirst.Models;
using System.Diagnostics;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace SearchNameCodeFirst.Controllers
{
    public class PeopleController : Controller
    {
        private SearchContext db = new SearchContext();

        // GET: People
        public ActionResult Index()
        {
            return View(db.People.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            People people = db.People.Find(id);
            if (people == null)
            {
                return HttpNotFound();
            }
            return View(people);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Age,Interest,Address")] People people, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string FileName = file.FileName;
                    string TailPath = "Image/" + FileName;
                    string ServerPath = Server.MapPath(" ");
                    int lastSlash = ServerPath.LastIndexOf("People");
                    ServerPath = ServerPath.Substring(0, lastSlash);
                    string FilePath = Path.Combine(ServerPath, TailPath);
                    file.SaveAs(FilePath);
                    people.ImagePath = TailPath;
                }

                db.People.Add(people);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(people);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            People people = db.People.Find(id);
            if (people == null)
            {
                return HttpNotFound();
            }
            return View(people);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Age,Interest,Address,ImagePath")] People people, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string FileName = file.FileName;
                    string TailPath = "Image/" + FileName;
                    string ServerPath = Server.MapPath(" ");
                    int lastSlash = ServerPath.LastIndexOf("People");
                    ServerPath = ServerPath.Substring(0, lastSlash);
                    string FilePath = Path.Combine(ServerPath, TailPath);
                    file.SaveAs(FilePath);
                    people.ImagePath = TailPath;
                }

                db.Entry(people).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(people);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            People people = db.People.Find(id);
            if (people == null)
            {
                return HttpNotFound();
            }
            return View(people);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            People people = db.People.Find(id);
            db.People.Remove(people);
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

        private void Wait(int interval)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while (sw.ElapsedMilliseconds < interval)
            {
                // Allows UI to remain responsive
                Application.DoEvents();
            }
            sw.Stop();
        }

        public ActionResult Search(string Name)
        {
            //Simulate a delay
            Wait(3000);
            var PeopleList = db.People.Where(c => c.FirstName.Contains(Name) | c.LastName.Contains(Name)).ToList();

            return PartialView("PeoplePartial", PeopleList);
        }

    }
}
