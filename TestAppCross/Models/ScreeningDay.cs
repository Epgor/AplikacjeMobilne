using System;
using System.Collections.Generic;

namespace TestAppCross.Models
{
    public class ScreeningDay
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsOpen { get; set; }
        public bool IsExtraPaid { get; set; }
        public List<CinemaHall> CinemaHalls { get; set; }

    }
}