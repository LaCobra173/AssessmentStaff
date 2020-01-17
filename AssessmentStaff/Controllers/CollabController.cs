using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssessmentStaff.Models;

namespace AssessmentStaff.Controllers
{
    public class CollabController : Controller
    {
        private RootEntities db = new RootEntities();

        // GET: User
        public ActionResult Index()
        {
            var user = db.User;
            return View(user.ToList());
        }


        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Http400", "ИсключительныеСитуации");
            }
            var user = db.User.FirstOrDefault(a => a.IDUser == id);
            if (user == null)
            {
                return RedirectToAction("Http404", "ИсключительныеСитуации");
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(string login, string firstpassword, string secondpassword, string surname, string name, string patronymic, string bornDate, string roleEnum)
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

                var newuser = db.User.FirstOrDefault(a => a.Login == login);
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
            catch (Exception ex)
            {
                return RedirectToAction("DbException", "Errors");
            }
        }
        
        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Http400", "ИсключительныеСитуации");
            }
            var user = db.User.FirstOrDefault(a => a.IDUser == id);
            if (user == null)
            {
                return RedirectToAction("Http404", "ИсключительныеСитуации");
            }

            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        

        public ActionResult Edit(string login, string surname, string name, string patronymic, string bornDate)
        {
            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(surname) ||
                    String.IsNullOrEmpty(name) || String.IsNullOrEmpty(patronymic) ||
                    String.IsNullOrEmpty(bornDate))
            {
                return RedirectToAction("WrongInputData", "Errors");
            }

            var user = db.User.FirstOrDefault(a => a.Login == login);
            if (user == null)
            {
                return RedirectToAction("Http404", "ИсключительныеСитуации");
            }
            
            DateTime.TryParse(bornDate, out var Date);

            if (Date == new DateTime())
            {
                ViewBag.ErrorDate = "Введена некорректная дата! Проверьте данные!";
                return View();
            }

            user.Surname = surname;
            user.Name = name;
            user.Patronymic = patronymic;
            user.BornDate = Date;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Http400", "ИсключительныеСитуации");
            }

            var user = db.User.FirstOrDefault(a => a.IDUser == id);
            if (user == null)
            {
                return RedirectToAction("Http404", "ИсключительныеСитуации");
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var user = db.User.FirstOrDefault(a => a.IDUser == id);
            if (user == null)
            {
                return RedirectToAction("Http404", "ИсключительныеСитуации");
            }

            db.User.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
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