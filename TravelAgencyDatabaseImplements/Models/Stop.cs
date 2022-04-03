using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyDatabaseImplements.Models
{
    public class Stop
    {
        public int Id { get; set; }
        [Required]
        public string StopName { get; set; }
        [Required]
        public DateTime CheckinDateStop { get; set; }
        [Required]
        public DateTime DateofDepatureStop { get; set; }
        public string OperatorLogin { get; set; }
        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
