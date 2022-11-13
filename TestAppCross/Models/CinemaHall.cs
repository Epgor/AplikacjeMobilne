using System;
using System.Collections.Generic;

namespace TestAppCross.Models
{
    public class CinemaHall
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Seats { get; set; }
        public int PremiumSeats { get; set; }
        public bool IsOpen { get; set; }
        public List<MovieSession> MovieSessions { get; set; }
    }
}