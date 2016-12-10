using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JoycePrint.Domain.Configuration;

namespace JoycePrint.UI.Controllers
{
    public class BaseController : Controller
    {
        public IConfig Config { get; }

        public BaseController()
        {
            Config = new Config();
        }
    }
}