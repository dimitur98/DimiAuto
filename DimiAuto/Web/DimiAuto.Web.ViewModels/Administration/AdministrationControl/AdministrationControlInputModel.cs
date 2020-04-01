using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DimiAuto.Web.ViewModels.Administration.AdministrationControl
{
   public class AdministrationControlInputModel
    {
        [Required]
        public string Id { get; set; }
    }
}
