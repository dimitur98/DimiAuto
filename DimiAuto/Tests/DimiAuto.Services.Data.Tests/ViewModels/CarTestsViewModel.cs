using DimiAuto.Models.CarModel;
using DimiAuto.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Services.Data.Tests.ViewModels
{
   public class CarTestsViewModel : IMapFrom<Car>
    {
        public string UserId { get; set; }

        public string Modification { get; set; }
    }
}
