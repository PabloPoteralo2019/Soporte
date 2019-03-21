using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using SOPORTE.Models;

namespace DIAPCIVIL001.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(usuario objUserAV, it usuario_it)
        {
            
            DBSoporteEntities db = new DBSoporteEntities();
            if (ModelState.IsValid)
            {
                using (db)
                {

                    using (var context = new DBSoporteEntities())
                    {
                        
                        List<it> lista_IT = new List<it>();
                        List<usuario> lista_AV = new List<usuario>();

                        var cuentaIT = new SqlParameter("@CuentaIT", usuario_it.cuenta);
                        var pasguordIT = new SqlParameter("@PasswordIT", usuario_it.password);

                        var cuenta_AV = new SqlParameter("@CuentaAV", objUserAV.cuenta);
                        var pasguord_AV = new SqlParameter("@PasswordAV", objUserAV.password);

                        if (objUserAV.cuenta == null || objUserAV.password == null || usuario_it.cuenta == null || usuario_it.password == null)
                        {

                        }
                        else
                        {
                            lista_IT = context.Database
                                .SqlQuery<it>("Ingresar_Aplicacion_IT @CuentaIT, @PasswordIT", cuentaIT, pasguordIT).ToList();


                            lista_AV = context.Database
                                .SqlQuery<usuario>("Ingresar_Aplicacion_AV @CuentaAV, @PasswordAV", cuenta_AV, pasguord_AV).ToList();

                           

                            if (lista_IT.Count != 0)
                            {
                                if (lista_IT[0].rol == 1)
                                {
                                    Session["UserID"] = lista_IT[0].it_id.ToString();

                                    Session["User"] = lista_IT[0].nombre.ToString();

                                    Session["Ape"] = lista_IT[0].apellido.ToString();

                                    Session["cuenta"] = lista_IT[0].cuenta.ToString();
                                    

                                    return RedirectToAction("UserDashBoard");
                                }
                            }
                            else if (lista_AV.Count != 0)
                            {
                                if (lista_AV[0].rol == 2)
                                {
                                    Session["UserID"] = lista_AV[0].usuario_id.ToString();

                                    Session["User"] = lista_AV[0].nombre.ToString();

                                    return RedirectToAction("UserDashBoard_2");
                                }
                                else if (lista_AV[0].rol == 3)
                                {

                                    Session["User"] = lista_AV[0].nombre.ToString();
                                    Session["Cuenta"] = lista_AV[0].cuenta.ToString();
                                    Session["UserID"] = lista_AV[0].usuario_id.ToString();

                                    return RedirectToAction("UserDashBoard_3");
                                }
                            }
                        }
                    }
                }
            }
            return View();
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult UserDashBoard_1()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult UserDashBoard_2()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");

            }
        }

        public ActionResult UserDashBoard_3()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult cambiarclave()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        public ActionResult CambiarContrasenia(string dato)
        {
            DBSoporteEntities db = new DBSoporteEntities();
            var cuenta_Usuario = new SqlParameter("@usuario", Session["cuenta"].ToString());
            var clavenueva = new SqlParameter("@nuevaclave", dato);
            List<it> list = new List<it>();
            list = db.Database.SqlQuery<it>("sp_Cambiar_Clave @usuario, @nuevaclave", cuenta_Usuario, clavenueva).ToList();
            return View("../Home/Login");
        }

        public ActionResult cerrarsesion()
        {
            Session.Contents.RemoveAll();
            return RedirectToAction("Login");
        }

    }
}