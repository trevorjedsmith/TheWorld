using System;
using System.ComponentModel.DataAnnotations;

namespace TheWorld.Models
{
    public class StopViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        [Required]
        public DateTime Arrival { get; set; }
    }
}