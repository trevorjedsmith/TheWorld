using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using TheWorld.Models;

namespace TheWorld.ViewModels
{
    public class TripViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255,MinimumLength =5)]
        public string Name { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow; //c#6 init auto prop
        public ICollection<Stop> Stops { get; set; }
    }
}
