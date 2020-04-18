namespace DimiAuto.Web.ViewModels.Img
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using DimiAuto.Services;
    using DimiAuto.Web.ViewModels.Attribute;
    using Microsoft.AspNetCore.Http;

    public class ImgUploadInputModel
    {
        [ImgValidation(ErrorMessage = "First photo doesn't comply rules!")]
        public IFormFile? File1 { get; set; }

        [ImgValidation(ErrorMessage = "Second photo doesn't comply rules!")]
        public IFormFile? File2 { get; set; }

        [ImgValidation(ErrorMessage = "Third photo doesn't comply rules!")]
        public IFormFile? File3 { get; set; }

        [ImgValidation(ErrorMessage = "Fourth  photo doesn't comply rules!")]
        public IFormFile? File4 { get; set; }

        [ImgValidation(ErrorMessage = "Fifth  photo doesn't comply rules!")]
        public IFormFile? File5 { get; set; }

        [ImgValidation(ErrorMessage = "Sixth  photo doesn't comply rules!")]
        public IFormFile? File6 { get; set; }

        [ImgValidation(ErrorMessage = "Seventh photo doesn't comply rules!")]
        public IFormFile? File7 { get; set; }

        [ImgValidation(ErrorMessage = "Eighth photo doesn't comply rules!")]
        public IFormFile? File8 { get; set; }

        [ImgValidation(ErrorMessage = "Ninth photo doesn't comply rules!")]
        public IFormFile? File9 { get; set; }

        [ImgValidation(ErrorMessage = "Thenth photo doesn't comply rules!")]
        public IFormFile? File10 { get; set; }

    }
}
