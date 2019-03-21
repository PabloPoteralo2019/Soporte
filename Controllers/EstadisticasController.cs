using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOPORTE.Models;
using System.Data.SqlClient;

namespace SOPORTE.Controllers
{
    public class EstadisticasController : Controller
    {
        private DBSoporteEntities db = new DBSoporteEntities();

        // GET: Estadisticas
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                var fallas = db.falla.ToList();
                var lista_fallas = new SelectList(fallas, "falla_id", "descripcion");
                
                ViewData["fallas"] = lista_fallas;
                return View();
                //var soporte = db.soporte.Include(s => s.estado1).Include(s => s.falla1).Include(s => s.it1).Include(s => s.medida1).Include(s => s.usuario1);
                //return View(soporte.ToList());
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

        }

        // GET: Estadisticas/Details/5
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

        // GET: Estadisticas/Create
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

        // POST: Estadisticas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "soporte_id,equipo,nro_de_orden,it,usuario,fecha_inicio,fecha_final,division,descripcion,observaciones,estado,borrado,falla,medida")] soporte soporte)
        {
            if (Session["UserID"] != null)
            {
                if (ModelState.IsValid)
                {
                    db.soporte.Add(soporte);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.estado = new SelectList(db.estado, "estado_id", "descripcion", soporte.estado);
                ViewBag.falla = new SelectList(db.falla, "falla_id", "descripcion", soporte.falla1);
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

        // GET: Estadisticas/Edit/5
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

        // POST: Estadisticas/Edit/5
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
                    db.Entry(soporte).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
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

        // GET: Estadisticas/Delete/5
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

        // POST: Estadisticas/Delete/5
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

        public ActionResult Busqueda(string fallas, DateTime? fecha_desde, DateTime? fecha_hasta)
        {

            if (fecha_desde == null && fecha_hasta == null)
            {
                if (fallas != null)
                {
                    using (var context = new DBSoporteEntities())
                    {
                        if (fallas == "23")
                        {
                            return RedirectToAction("BusquedaTodos");
                        }
                        else
                        {
                            var Busqueda_Nombre = from s in db.soporte where s.falla.Value.ToString().Contains(fallas) select s;
                                                  //;

                            Busqueda_Nombre = Busqueda_Nombre.Where(
//||
//                            (s.fecha_inicio >= fecha_desde && s.fecha_final <= fecha_hasta)
//                            )
                            s => (s.falla.Value.ToString().Contains(fallas))) ;

                            var tot = Busqueda_Nombre.ToList();
                            string totality = tot.Count.ToString();
                            ViewData["total"] = totality;

                            return View(Busqueda_Nombre.ToList());
                        }
                        
                    }
                }
                else
                {
                    return View();
                }
            }

            else
            {
                var busqueda_fechas = from s in db.soporte select s;

                busqueda_fechas = busqueda_fechas.Where(

                s => (s.falla.Value.ToString().Contains(fallas)) &&
                (s.fecha_inicio >= fecha_desde && s.fecha_final <= fecha_hasta)
                );

                var tot = busqueda_fechas.ToList();
                string totality = tot.Count.ToString();
                ViewData["total"] = totality;

                return View(busqueda_fechas.ToList());
                //return RedirectToAction("../Home/Login");

            }
        }

        public ActionResult BusquedaTodos()
        {
            var Busqueda_Todos = from s in db.soporte select s;
            var tot = Busqueda_Todos.ToList();
            string totality = tot.Count.ToString();
            ViewData["total"] = totality;

            List<falla> Total_Fallas = new List<falla>();

            using (var context = new DBSoporteEntities())
            {
                Total_Fallas = context.Database
                .SqlQuery<falla>("sp_estadistica").ToList();



                var num = 1;
                for (int i = 0; i < Total_Fallas.Count; i++)
                {
                    ViewData["cantidades" + num] = Total_Fallas[i].descripcion + Total_Fallas[i].falla_id;
                    num++;
                }
            }



            return View(Total_Fallas.ToList());
        }

        public ActionResult GeneratePDF()
        {
            return new Rotativa.ViewAsPdf("BusquedaTodos");
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
