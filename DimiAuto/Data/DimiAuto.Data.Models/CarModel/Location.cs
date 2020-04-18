namespace DimiAuto.Data.Models.CarModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public enum Location
    {
        All = 1,
        Sofia = 2,
        Plovdiv = 3,
        Varna = 4,
        Vratsa = 5,
        Dupnitsa = 6,
        [Display(Name = "Stara Zagora")]
        StaraZagora = 7,
        Montana = 8,
        Burgas = 9,
        Pleven = 10,
        Ruse = 11,
    }
}
