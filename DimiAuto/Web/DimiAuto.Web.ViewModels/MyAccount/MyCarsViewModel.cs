using DimiAuto.Data.Models.CarModel;
using DimiAuto.Models.CarModel;
using DimiAuto.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Web.ViewModels.MyAccount
{
   public class MyCarsViewModel : IMapFrom<Car>
    {
        public string Id { get; set; }

        public Make Make { get; set; }

        public string Model { get; set; }

        public string Modification { get; set; }

        public string Price { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsApproved { get; set; }

        public string Statuse => this.IsApproved ? "Approved" : "Waiting";
    }
}
