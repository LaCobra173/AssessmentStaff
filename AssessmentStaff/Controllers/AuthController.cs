using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net; 
using System.Web;
using System.Web.Mvc;
using AssessmentStaff.Models;

namespace AssessmentStaff.Controllers
{
    public class AuthController : Controller
    {
        private RootEntities db = new RootEntities();

        // GET: Auth
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string login, string password)
        {
            ViewBag.Error = "";
            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
            {
                return RedirectToAction("WrongInputData", "Errors");
            }

            var user = db.User.FirstOrDefault(a => a.Login == login && a.Password == password);
            if (user == null) 
            {
                ViewBag.Error = "Введены некорректные логин или пароль! Проверьте данные!";
            }

            return View();
        }

    }
}