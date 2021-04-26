using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTextAnalyzer.Models;

namespace WebTextAnalyzer.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            Form form = new Form();

            return View(form);

        }

        [HttpPost]
        public ActionResult Index(Form form)
        {
            if (!String.IsNullOrEmpty(form.Input))
            {
                if (ModelState.IsValid)
                {
                    form.ComputeStats();
                }
                return View("Result", form);
            }
            else
            {
                return View();
            }

        }
    }
}