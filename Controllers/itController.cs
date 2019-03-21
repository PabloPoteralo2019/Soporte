using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOPORTE.Models;

namespace SOPORTE.Controllers
{
    public class itController : Controller
    {
        private DBSoporteEntities db = new DBSoporteEntities();

        // GET: it
        public ActionResult Index()
        {

            if (Session["UserID"] != null)
            {
                return View(db.it.ToList());
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }


        }

        // GET: it/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                it it = db.it.Find(id);
                if (it == null)
                {
                    return HttpNotFound();
                }
                return View(it);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // GET: it/Create
        public ActionResult Create()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // POST: it/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "it_id,cuenta,nombre,apellido,estado, rol,password")] it it)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.it.Add(it);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(it);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // GET: it/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                it it = db.it.Find(id);
                if (it == null)
                {
                    return HttpNotFound();
                }
                return View(it);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // POST: it/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "it_id,cuenta,nombre,apellido,estado, rol, password")] it it)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(it).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(it);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // GET: it/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                it it = db.it.Find(id);
                if (it == null)
                {
                    return HttpNotFound();
                }
                return View(it);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // POST: it/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserID"] != null)
            {
                it it = db.it.Find(id);
                db.it.Remove(it);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

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
