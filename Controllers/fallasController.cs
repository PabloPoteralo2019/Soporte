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
    public class fallasController : Controller
    {
        private DBSoporteEntities db = new DBSoporteEntities();

        // GET: fallas
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                return View(db.falla.ToList());
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // GET: fallas/Details/5
        public ActionResult Details(int? id)
        {

            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                falla falla = db.falla.Find(id);
                if (falla == null)
                {
                    return HttpNotFound();
                }
                return View(falla);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }



        }

        // GET: fallas/Create
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

        // POST: fallas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "falla_id,descripcion")] falla falla)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.falla.Add(falla);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(falla);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }



        }

        // GET: fallas/Edit/5
        public ActionResult Edit(int? id)
        {

            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                falla falla = db.falla.Find(id);
                if (falla == null)
                {
                    return HttpNotFound();
                }
                return View(falla);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }



        }

        // POST: fallas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "falla_id,descripcion")] falla falla)
        {

            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(falla).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(falla);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }


        }

        // GET: fallas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                falla falla = db.falla.Find(id);
                if (falla == null)
                {
                    return HttpNotFound();
                }
                return View(falla);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }


        }

        // POST: fallas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserID"] != null)
            {
                falla falla = db.falla.Find(id);
                db.falla.Remove(falla);
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
