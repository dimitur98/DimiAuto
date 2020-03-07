﻿namespace DimiAuto.Web.Areas.Administration.Controllers
{
    using DimiAuto.Common;
    using DimiAuto.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
