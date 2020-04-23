using DimiAuto.Data.Models.CarModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DimiAuto.Web.ViewModels.Home
{
    public class MostWatchedUserCarViewModel
    {
        public string Id { get; set; }

        public Make Make { get; set; }

        public string ModelToString { get; set; }

        public string Modification { get; set; }

        public string MakeModel { get; set; }

        public string ImgPath { get; set; }

        public int Views { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
