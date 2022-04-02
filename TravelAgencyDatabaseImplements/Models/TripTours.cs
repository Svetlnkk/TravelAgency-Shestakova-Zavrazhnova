using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyDatabaseImplements.Models
{
    public class TripTours
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public int TourId { get; set; }
        [Required]
        public int TourCount { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
