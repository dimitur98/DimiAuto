namespace DimiAuto.Web.ViewModels.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DimiAuto.Data.Models;
    using DimiAuto.Data.Models.CarModel;
    using DimiAuto.Models.CarModel;
    using DimiAuto.Services.Mapping;
    using DimiAuto.Web.ViewModels.Administration;

    public class AdViewModel : IMapFrom<Car>
    {
        public string Id { get; set; }

        public Make Make { get; set; }

        public string Model { get; set; }

        public string Modification { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool IsApproved { get; set; }

        public bool IsDeleted { get; set; }


        public string Statuse
        {
            get
            {
                if (this.IsApproved)
                {
                    if (this.IsDeleted)
                    {
                        return "Approved, Deleted";

                    }
                    else
                    {
                        return "Approved, Not deleted";

                    }
                }
                else
                {
                    if (this.IsDeleted)
                    {
                        return "Not approved, Deleted";

                    }
                    else
                    {
                        return "Not approved, Not deleted";

                    }
                }
                
            }

            
        }

    }
}
