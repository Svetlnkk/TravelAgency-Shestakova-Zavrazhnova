using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplements.Models
{
    public class Trip
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string TouristLogin { get; set; }

        [ForeignKey("TripId")]
        public virtual List<TripExcursions> TripExcursions { get; set; }
        [ForeignKey("TripId")]
        public virtual List<TripTours> TripTours { get; set; }

    }
}
