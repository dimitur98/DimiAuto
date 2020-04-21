namespace DimiAuto.Web.ViewModels.MyAccount
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using DimiAuto.Web.ViewModels.Attribute;
    using Microsoft.AspNetCore.Http;

   public class ChangeAvatarInputModel
    {
        [ImgValidation(ErrorMessage = "Photo doesn't comply rules!")]
        public IFormFile File1 { get; set; }
    }
}
