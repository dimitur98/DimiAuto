﻿using System.ComponentModel.DataAnnotations;

namespace DimiAuto.Data.Models.CarModel
{
    public enum Make
    {
        All = 1,
        Audi = 2,
        [Display(Name = "Aston Martin")]
        AstonMartin = 3,
        Bmw = 4,
        Bentley = 5,
        Bugatti = 6,
        Citroen = 7,
        Chevrolet = 8,
        Dodge = 9,
        Ferrari = 10,
        Fiat = 11,
        Ford = 12,
        Honda = 13,
        Hyundai = 14,
        Hummer = 15,
        Jeep = 16,
        Kia = 17,
        Lamborghini = 18,
        [Display(Name = "Land Rover")]
        LandRover = 19,
        Maserati = 20,
        Mazda = 21,
        McLaren = 22,
        [Display(Name = "Mercedes Benz")]
        MercedesBenz = 23,
        Mitsubishi = 24,
        Nissan = 25,
        Opel = 26,
        Peugoet = 27,
        Porsche = 28,
        Renault = 29,
        [Display(Name = "Rolls Royce")]
        RollsRoyce = 30,
        Seat = 31,
        Skoda = 32,
        Tesla = 33,
        Toyota = 34,
        Vw = 35,

    }
}