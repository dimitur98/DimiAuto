using DimiAuto.Web.ViewModels.Ad;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DimiAuto.Web.Controllers
{
    public class AdController : Controller
    {
        public IActionResult CreateAd()
        {
            return this.View();
        }
        [HttpPost]
        public IActionResult CreateAd(CreateAdInputModel input)
        {
            return this.View();
        }
    }
}
