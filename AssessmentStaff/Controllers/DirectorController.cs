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
    public class DirectorController : Controller
    {
        // GET: Director
        public ActionResult Index()
        {
            return View();
        }
    }
}