﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernAppliances
{
    public class Dishwasher : Appliance
    {
        public string Feature { get; set; }
        public string SoundRating { get; set; }

        public Dishwasher(string itemNumber, string brand, int quantity, int wattage, string color, double price, string feature, string soundRating)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Feature = feature;
            SoundRating = soundRating;
        }

        public override string ToString()
        {
            return base.ToString() + $"Feature: {Feature}\nSoundRating: {SoundRating}\n";
        }
    }
}
