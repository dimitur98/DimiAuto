namespace DimiAuto.Web.ViewModels.Ad.ApiController
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class ApiInputModel
    {
        public string CarId { get; set; }

        public string Make { get; set; }
    }
}
