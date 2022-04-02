using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplements.Models
{
    public class Excursion
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public double Time { get; set; }
        [Required]
        public int Price { get; set; }
        public string TouristLogin { get; set; }
        [ForeignKey("ExcursionId")]
        public virtual List<TripExcursions> TripExcursions { get; set; }
    }
}
