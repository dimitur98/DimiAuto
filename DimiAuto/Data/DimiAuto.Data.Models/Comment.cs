using DimiAuto.Common;
using DimiAuto.Data.Common.Models;
using DimiAuto.Data.Models;
using DimiAuto.Models.CarModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DimiAuto.Data.Models
{
    public class Comment : BaseDeletableModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }


        public string Title { get; set; }
        public string Content { get; set; }

        public string UserId { get; set; }

        public string CarId { get; set; }

    }
}
