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
                    form.Compute();
                }
                return View("Result", form);
            }
            else
            {
                return View();
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}