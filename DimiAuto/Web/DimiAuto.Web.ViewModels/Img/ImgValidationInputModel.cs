namespace DimiAuto.Web.ViewModels.Img
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class ImgValidationInputModel
    {
        public string Format { get; set; }

        public long Size { get; set; }

        public string Type { get; set; }
    }
}
