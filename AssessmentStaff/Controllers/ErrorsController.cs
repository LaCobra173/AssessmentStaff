using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AssessmentStaff.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors

        // Ошибка на сервере(в случае, если что-то не найдено)
        public ActionResult Http404()
        {
            return View();
        }
        // Обрабатывает некорректный ввод данных в базу
        public ActionResult DbException()
        {
            return View();
        }
        // Неопределенная ошибка на сервере
        public ActionResult Http400()
        {
            return View();
        }

        public ActionResult WrongInputData()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}