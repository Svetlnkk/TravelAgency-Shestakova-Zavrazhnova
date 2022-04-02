using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplements.Models
{
    public class Tour
    {
        public int? Id { get; set; }
        [Required]
        public string TourName { get; set; }
        public string OperatorLogin { get; set; }
        [ForeignKey("TourId")]
        public virtual List<ExcursionTour> ExcursionTours { get; set; }
    }
}
