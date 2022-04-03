using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyDatabaseImplements.Models
{
    public class GuideExcursion
    {
        public int Id { get; set; }
        public int ExcursionId { get; set; }
        public int GuideId { get; set; }
        [Required]
        public int ExcursionCount { get; set; }
        public virtual Excursion Excursion { get; set; }
        public virtual Guide Guide { get; set; }
    }
}
