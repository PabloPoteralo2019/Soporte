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
    public class medidasController : Controller
    {
        private DBSoporteEntities db = new DBSoporteEntities();

        // GET: medidas
        public ActionResult Index()
        {

            if (Session["UserID"] != null)
            {
                return View(db.medida.ToList());
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // GET: medidas/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                medida medida = db.medida.Find(id);
                if (medida == null)
                {
                    return HttpNotFound();
                }
                return View(medida);

            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // GET: medidas/Create
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

        // POST: medidas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "medida_id,descripcion")] medida medida)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.medida.Add(medida);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(medida);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // GET: medidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                medida medida = db.medida.Find(id);
                if (medida == null)
                {
                    return HttpNotFound();
                }
                return View(medida);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // POST: medidas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "medida_id,descripcion")] medida medida)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(medida).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(medida);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // GET: medidas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                medida medida = db.medida.Find(id);
                if (medida == null)
                {
                    return HttpNotFound();
                }
                return View(medida);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // POST: medidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserID"] != null)
            {
                medida medida = db.medida.Find(id);
                db.medida.Remove(medida);
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
