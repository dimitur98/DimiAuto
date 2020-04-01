using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DimiAuto.Web.ViewModels.Img
{
    public class ImgDeleteInputModel
    {
        [Required]
        public string CarId { get; set; }

        [Required]
        public string ImgToDel { get; set; }
    }
}
