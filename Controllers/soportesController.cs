using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SOPORTE.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SOPORTE.Controllers
{
    public class soportesController : Controller
    {
        private DBSoporteEntities db = new DBSoporteEntities();

        // GET: soportes
        public ActionResult Index()
        {

            if (Session["UserID"] != null)
            {
                var soporte = db.soporte.Include(s => s.estado1).Include(s => s.falla1).Include(s => s.it1).Include(s => s.medida1).Include(s => s.usuario1).OrderByDescending(s => s.nro_de_orden.ToString());
                return View(soporte.ToList());
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }



        }

        // GET: soportes/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                soporte soporte = db.soporte.Find(id);
                if (soporte == null)
                {
                    return HttpNotFound();
                }
                return View(soporte);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // GET: soportes/Create
        public ActionResult Create()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.estado = new SelectList(db.estado, "estado_id", "descripcion");
                ViewBag.falla = new SelectList(db.falla, "falla_id", "descripcion");
                ViewBag.it = new SelectList(db.it, "it_id", "cuenta");
                ViewBag.medida = new SelectList(db.medida, "medida_id", "descripcion");
                ViewBag.usuario = new SelectList(db.usuario, "usuario_id", "cuenta");
                return View();
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // POST: soportes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "soporte_id,equipo,nro_de_orden,it,usuario,fecha_inicio,fecha_final,division,descripcion,observaciones,estado,borrado,falla,medida")] soporte soporte)
        {

            if (Session["UserID"] != null)
            {


                DBSoporteEntities db = new DBSoporteEntities();

                //if (lista.Count == 0)
                //{
                if (ModelState.IsValid)
                {

                    db.soporte.Add(soporte);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.estado = new SelectList(db.estado, "estado_id", "descripcion", soporte.estado);
                ViewBag.falla = new SelectList(db.falla, "falla_id", "descripcion", soporte.falla);
                ViewBag.it = new SelectList(db.it, "it_id", "cuenta", soporte.it1);
                ViewBag.medida = new SelectList(db.medida, "medida_id", "descripcion", soporte.medida1);
                ViewBag.usuario = new SelectList(db.usuario, "usuario_id", "cuenta", soporte.usuario1);
                return View("Busqueda");
            }
            return RedirectToAction("error");


        }
        //else
        //{
        //    return RedirectToAction("../Home/Login");
        //}
    
        

        // GET: soportes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                soporte soporte = db.soporte.Find(id);
                if (soporte == null)
                {
                    return HttpNotFound();
                }
                ViewBag.estado = new SelectList(db.estado, "estado_id", "descripcion", soporte.estado);
                ViewBag.falla = new SelectList(db.falla, "falla_id", "descripcion", soporte.falla);
                ViewBag.it = new SelectList(db.it, "it_id", "cuenta", soporte.it);
                ViewBag.medida = new SelectList(db.medida, "medida_id", "descripcion", soporte.medida);
                ViewBag.usuario = new SelectList(db.usuario, "usuario_id", "cuenta", soporte.usuario);



                return View(soporte);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }



        // POST: soportes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "soporte_id,equipo,nro_de_orden,it,usuario,fecha_inicio,fecha_final,division,descripcion,observaciones,estado,borrado,falla,medida")] soporte soporte)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    if (soporte.descripcion.Length >= 150)
                    {
                        return RedirectToAction("JavaScriptResult");
                    }
                    else
                    {
                        //System.Data.Entity.Validation.DbEntityValidationException 
                        db.Entry(soporte).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Busqueda");
                    }

                }
                ViewBag.estado = new SelectList(db.estado, "estado_id", "descripcion", soporte.estado);
                ViewBag.falla = new SelectList(db.falla, "falla_id", "descripcion", soporte.falla);
                ViewBag.it = new SelectList(db.it, "it_id", "cuenta", soporte.it);
                ViewBag.medida = new SelectList(db.medida, "medida_id", "descripcion", soporte.medida);
                ViewBag.usuario = new SelectList(db.usuario, "usuario_id", "cuenta", soporte.usuario);
                return View(soporte);

            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // GET: soportes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["UserID"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                soporte soporte = db.soporte.Find(id);
                if (soporte == null)
                {
                    return HttpNotFound();
                }
                return View(soporte);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // POST: soportes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UserID"] != null)
            {
                soporte soporte = db.soporte.Find(id);
                db.soporte.Remove(soporte);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        public ActionResult Busqueda(string dato)
        {
            var soporte = db.soporte.Include(s => s.estado1).Include(s => s.falla1).Include(s => s.it1).Include(s => s.medida1).Include(s => s.usuario1).OrderByDescending(s => s.nro_de_orden);

            //var Busqueda_Nombre = (from s  in db.soporte select s);

            var Busqueda_Nombre = from t in db.soporte
                                  orderby t.nro_de_orden.Value descending
                                  select t;
                                  

            string nom1 = null;

            if (dato != null)
            {
                string[] nom = dato.Split(' ');
                for (int i = 0; i < nom.Length; i++)
                {
                    if (nom[i].Length != 0)
                    {
                        if (nom1 == null)
                        {
                            nom1 = nom[i];
                        }
                    }
                }
            }

            if (nom1 != null)
            {
                Busqueda_Nombre = Busqueda_Nombre.Where(

                s => s.equipo.Contains(nom1) ||
                      s.estado1.descripcion.Contains(nom1) ||
                      s.nro_de_orden.Value.ToString().Contains(nom1) ||
                      s.it1.cuenta.Contains(nom1) ||
                      s.fecha_inicio.Value.ToString().Contains(nom1)).OrderByDescending(s => s.nro_de_orden);
            }
            return View(Busqueda_Nombre);
        }

        public ActionResult CreateCopia()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.estado = new SelectList(db.estado, "estado_id", "descripcion");
                ViewBag.falla = new SelectList(db.falla, "falla_id", "descripcion");
                ViewBag.it = new SelectList(db.it, "it_id", "cuenta");
                ViewBag.medida = new SelectList(db.medida, "medida_id", "descripcion");
                ViewBag.usuario = new SelectList(db.usuario, "usuario_id", "cuenta");
                return View();
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCopia([Bind(Include = "soporte_id,equipo,nro_de_orden,it,usuario,fecha_inicio,fecha_final,division,descripcion,observaciones,estado,borrado,falla,medida")] soporte soporte)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.soporte.Add(soporte);
                    db.SaveChanges();
                    return RedirectToAction("../Home/Login");
                }

                ViewBag.estado = new SelectList(db.estado, "estado_id", "descripcion");
                ViewBag.falla = new SelectList(db.falla, "falla_id", "descripcion");
                ViewBag.it = new SelectList(db.it, "it_id", "cuenta");
                ViewBag.medida = new SelectList(db.medida, "medida_id", "descripcion");
                ViewBag.usuario = new SelectList(db.usuario, "usuario_id", "cuenta");
                return View(soporte);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }


        }

        public ActionResult CreateCopiaAV()
        {
            if (Session["UserID"] != null)
            {
                ViewBag.estado = new SelectList(db.estado, "estado_id", "descripcion");
                ViewBag.falla = new SelectList(db.falla, "falla_id", "descripcion");
                ViewBag.it = new SelectList(db.it, "it_id", "cuenta");
                ViewBag.medida = new SelectList(db.medida, "medida_id", "descripcion");
                ViewBag.usuario = new SelectList(db.usuario, "usuario_id", "cuenta");
                return View();
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCopiaAV([Bind(Include = "soporte_id,equipo,nro_de_orden,it,usuario,fecha_inicio,fecha_final,division,descripcion,observaciones,estado,borrado,falla,medida")] soporte soporte)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.soporte.Add(soporte);
                    db.SaveChanges();
                    return RedirectToAction("../Home/Login");
                }

                ViewBag.estado = new SelectList(db.estado, "estado_id", "descripcion");
                ViewBag.falla = new SelectList(db.falla, "falla_id", "descripcion");
                ViewBag.it = new SelectList(db.it, "it_id", "cuenta");
                ViewBag.medida = new SelectList(db.medida, "medida_id", "descripcion");
                ViewBag.usuario = new SelectList(db.usuario, "usuario_id", "cuenta");
                return View(soporte);
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }


        }

        public ActionResult JavaScriptResult()
        {
            /*...*/
            return JavaScript("ShowAlert(" + "Texto LArgo" + ");");
        }

        public ActionResult BusquedaAV()
        {
            var soporte = from a in db.soporte
                          orderby a.nro_de_orden.Value descending
                          select a;
            return View(soporte.ToList());
        }

        public ActionResult BusquedaOP()
        {
            usuario usu1 = new usuario();

            var usu = Session["Cuenta"];

            usu1.cuenta = Convert.ToString(usu);

            var soporte = from a in db.soporte
                          where a.usuario1.cuenta.Contains(usu1.cuenta)
                          orderby a.nro_de_orden.Value descending
                          select a;

            //var soporte = db.soporte.Include(s => s.estado1).Include(s => s.falla1).Include(s => s.it1).Include(s => s.medida1).Include(s => s.usuario1);
            return View(soporte.ToList());
        }

        public ActionResult error()
        {
            return View();
        }

        public ActionResult GeneratePDF(int? id)
        {
            soporte soporte = db.soporte.Find(id);
            return new Rotativa.ActionAsPdf("Details");
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
