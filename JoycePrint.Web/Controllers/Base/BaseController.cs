using System.Web.Mvc;
using JoycePrint.Domain.Configuration;

// ReSharper disable once CheckNamespace
namespace JoycePrint.UI.Controllers
{
    public class BaseController : Controller
    {
        protected IConfig Config { get; }

        public BaseController()
        {
            Config = new Config();
        }
    }
}