using System;

namespace TestAppCross.Models
{
    public class Movie
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public TimeSpan Length { get; set; }
        public float TicketPrice { get; set; }
        public float PremiumTicketPrice => TicketPrice * 1.25f;
        public string ImageUrl { get; set; }
    }
}