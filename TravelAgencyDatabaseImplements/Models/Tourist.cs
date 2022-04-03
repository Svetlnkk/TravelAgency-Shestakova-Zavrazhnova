using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplements.Models
{
    public class Tourist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("TouristLogin")]
        public virtual List<Excursion> Excursions { get; set; }
        [ForeignKey("TouristLogin")]
        public virtual List<Trip> Trips { get; set; }
        [ForeignKey("TouristLogin")]
        public virtual List<Place> Places { get; set; }
    }
}
