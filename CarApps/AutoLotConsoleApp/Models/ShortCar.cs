﻿namespace AutoLotConsoleApp.Models
{
    public class ShortCar
    {
        public int CarId { get; set; }
        public string Make { get; set; }
        public override string ToString() => $"{this.Make} with ID {this.CarId}.";
    }
}