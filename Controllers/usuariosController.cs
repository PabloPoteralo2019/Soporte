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
    public class usuariosController : Controller
    {
        private DBSoporteEntities db = new DBSoporteEntities();

        // GET: usuarios
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                var usuario = db.usuario.Include(u => u.rol1);
                return View(usuario.ToList());
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // GET: usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: usuarios/Create
        public ActionResult Create()
        {
            ViewBag.rol = new SelectList(db.rol, "rol_id", "tipo");
            return View();
        }

        // POST: usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "usuario_id,cuenta,telefono,nombre,apellido,rol,activo,password")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (usuario.cuenta != null)
                {
                    if (usuario.cuenta.StartsWith("DIAP") == true || usuario.cuenta.StartsWith("diap") == true)
                    {
                        db.usuario.Add(usuario);
                        db.SaveChanges();
                        return RedirectToAction("../Home/Login");
                    }
                    else
                    {
                        return RedirectToAction("error");
                    }
                    
                }
                else
                {
                    return RedirectToAction("error");
                }


            }

            ViewBag.rol = new SelectList(db.rol, "rol_id", "tipo", usuario.rol);
            return View(usuario);
        }

        // GET: usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.rol = new SelectList(db.rol, "rol_id", "tipo", usuario.rol);
            return View(usuario);
        }

        // POST: usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "usuario_id,cuenta,telefono,nombre,apellido,rol,activo,password")] usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.rol = new SelectList(db.rol, "rol_id", "tipo", usuario.rol);
            return View(usuario);
        }

        // GET: usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            usuario usuario = db.usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            usuario usuario = db.usuario.Find(id);
            db.usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult error()
        {
            return View();
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
