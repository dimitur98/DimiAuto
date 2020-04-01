using DimiAuto.Common;
using DimiAuto.Data.Models;
using DimiAuto.Models.CarModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DimiAuto.Web.ViewModels.Ad
{
    public class CarCommentsInputModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string CarId { get; set; }

        [Required]
        [StringLength(GlobalConstants.CommentTitleLenght)]
        public string Title { get; set; }
    }
}
