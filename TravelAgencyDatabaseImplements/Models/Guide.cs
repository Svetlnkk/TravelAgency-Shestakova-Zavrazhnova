using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplements.Models
{
    public class Guide
    {
        public int Id { get; set; }
        [Required]
        public string GuideName { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string OperatorLogin { get; set; }
        
        [ForeignKey("GuideId")]
        public virtual List<GuideExcursion> GuideExcursions { get; set; }
    }
}
