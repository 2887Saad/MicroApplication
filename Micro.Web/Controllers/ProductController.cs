﻿using Microsoft.AspNetCore.Mvc;

namespace Micro.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}