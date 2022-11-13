using System;

namespace TestAppCross.Models
{
    public class MovieSession
    {
        public string Id { get; set; }
        public int OccupiedSeats { get; set; }
        public int OccupiedPremiumSeats { get; set;}
        public DateTime StartDateTime { get; set; }
        public Movie MoviePlayed { get; set; }
    }
}