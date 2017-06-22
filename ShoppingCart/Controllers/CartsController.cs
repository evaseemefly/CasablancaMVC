using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Controllers
{
    public class CartsController : Controller
    {
        private readonly 

        // GET: Carts
        public ActionResult Index()
        {
            return View();
        }
    }
}