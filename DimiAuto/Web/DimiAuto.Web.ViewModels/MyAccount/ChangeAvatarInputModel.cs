namespace DimiAuto.Web.ViewModels.MyAccount
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class ChangeAvatarInputModel
    {
        public IFormFile File { get; set; }
    }
}
