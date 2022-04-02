using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyDatabaseImplements.Models
{
    public class TourGuide
    {
        public int Id { get; set; }
        public int GuideId { get; set; }
        public int TourId { get; set; }
        [Required]
        public string Method { get; set; }

        public virtual Tour Tour { get; set; }
        public virtual Guide Guide { get; set; }
    }
}
