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
    public class CreateUserController : Controller
    {
        private RootEntities db = new RootEntities();
        [HttpGet]
        // GET: CreateUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string login, string firstpassword, string secondpassword, string surname, string name, string patronymic, string bornDate, string roleEnum)
        {
            try
            {
                ViewBag.Error = "";
                ViewBag.ErrorDate = "";
                ViewBag.ErrorRole = "";
                ViewBag.ErrorLogin = "";
                if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(firstpassword) ||
                    String.IsNullOrEmpty(secondpassword) || String.IsNullOrEmpty(surname) ||
                    String.IsNullOrEmpty(name) || String.IsNullOrEmpty(patronymic) ||
                    String.IsNullOrEmpty(bornDate) || roleEnum == null)
                {
                    return RedirectToAction("WrongInputData", "Errors");
                }

                if (firstpassword != secondpassword)
                {
                    ViewBag.Error = "Пароли не совпадают!";
                    return View();
                }

                var newuser = db.User.FirstOrDefault(a=>a.Login == login);
                if (newuser != null)
                {
                    ViewBag.ErrorLogin = "Пользователь с таким логином уже существует!";
                    return View();
                }

                int.TryParse(roleEnum, out var rEnum);
                if (rEnum != 1 && rEnum != 2 && rEnum != 3)
                {
                    ViewBag.ErrorRole = "Введена неправильная роль! 1 - Сотрудник, 2 - Администратор, 3 - Директор";
                    return View();
                }

                if (rEnum == 3)
                {
                    var newUserDirector = db.User.FirstOrDefault(a => a.Role == 3);
                    if (newUserDirector != null)
                    {
                        ViewBag.ErrorRole = "Роль директора уже занята!";
                        return View();
                    }

                }

                DateTime.TryParse(bornDate, out var Date);
                
                if (Date == new DateTime())
                {
                    ViewBag.ErrorDate = "Введена некорректная дата! Проверьте данные!";
                    return View();
                }

                db.User.Add(new User() { Surname = surname, Name = name, Patronymic = patronymic, BornDate = Date, Role = rEnum, Login = login, Password = firstpassword });
                db.SaveChanges();
                return View();
            }
            catch(Exception ex)
            {
                return RedirectToAction("DbException", "Errors");
            }

           
        }
    }
}