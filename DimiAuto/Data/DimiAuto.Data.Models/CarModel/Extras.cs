using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models.CarModel
{
    public  class Extras
    {
        public Extras()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string AirCondiitioning { get; set; }
        public string Climatronic { get; set; }
        public string LeatherInterior { get; set; }
        public string ElectrinWindows { get; set; }

        public string ElectricMirrors { get; set; }

        public string ElectricSeats { get; set; }

        public string SeatHeating { get; set; }
        public string Sunroof { get; set; }
        public string Stereo { get; set; }
        public string AlloyWheels { get; set; }
        public string DvdTV { get; set; }
        public string MultiSteeringWheel { get; set; }
        
        public string AllWheelDrive { get; set; }
        public string ABS { get; set; }
        public string ESP { get; set; }
        public string Airbag { get; set; }
        public string Xenonlights { get; set; }
        public string HalogenHeadlights { get; set; }
        public string TractionControl { get; set; }
        public string Parktronic { get; set; }
        public string Alarm { get; set; }
        public string Immobilizer { get; set; }
        public string CentralLock { get; set; }
        public string Insurance { get; set; }
        public string Armored { get; set; }
        public string StartStopSystem { get; set; }
        public string KeylessGo { get; set; }
        //
        public string TiptronicMultitronic { get; set; }
        
        public string Autopilot { get; set; }

        public string PowerSteering { get; set; }
        public string OnboardComputer { get; set; }
        public string ServiceBook { get; set; }
        public string Warranty { get; set; }
        public string NavigationSystem { get; set; }
        public string RightHandDrive { get; set; }
        public string Tuning { get; set; }
        public string PanoramicRoof { get; set; }
        public string Taxi { get; set; }
        public string Retro { get; set; }
        public string Tow { get; set; }
        public string MoreSeats { get; set; }
        public string Refrigerated { get; set; }
    }
}
