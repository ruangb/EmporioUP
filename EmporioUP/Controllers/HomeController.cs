using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmporioUP.Models;

namespace EmporioUP.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        //This the Login method. It passes a object of my Model Class named "User".
        [HttpPost]
        public ActionResult Login(Funcionario func)
        {
            if (ModelState.IsValid)
            {
                //message will collect the String value from the model method.
                String message = func.LoginProcess(func.nome, func.senha);
                //RedirectToAction("actionName/ViewName_ActionResultMethodName", "ControllerName");
                if (message.Equals("1"))
                {
                    //this will add cookies for the username.
                    Response.Cookies.Add(new HttpCookie("Func1", func.nome));
                    //This is a different Controller for the User Homepage. Redirecting after successful process.
                    return RedirectToAction("Index");
                }
                else
                    ViewBag.ErrorMessage = "Usuário ou senha inválidos";
            }
            return View(func);
        }
    }
}