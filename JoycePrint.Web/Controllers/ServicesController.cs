﻿using System.Web.Mvc;

namespace JoycePrint.UI.Controllers
{
    [Route("/")]
    public class ServicesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}