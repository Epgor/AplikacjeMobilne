using System.Collections.Generic;

namespace TestAppCross.Models
{
    public class CinemaDTO
    {
        public string Name = "Super Kino";

        public List<ScreeningDay> ScreeningDays { get; set; }
    }
}

