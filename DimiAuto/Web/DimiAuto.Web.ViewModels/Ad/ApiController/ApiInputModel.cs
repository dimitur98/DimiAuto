using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DimiAuto.Web.ViewModels.Ad.ApiController
{
    public class ApiInputModel
    {
        [Required]
        public string CarId { get; set; }
    }
}
