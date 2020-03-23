namespace DimiAuto.Web.Areas.Administration.Controllers
{
    using DimiAuto.Common;
    using DimiAuto.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Administrator")]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        public IActionResult Administration()
        {
            return this.View();
        }
    }
}
