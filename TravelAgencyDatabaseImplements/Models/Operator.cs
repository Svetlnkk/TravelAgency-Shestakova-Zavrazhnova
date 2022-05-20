using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplements.Models
{
    public class Operator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey("OperatorLogin")]
        public virtual List<Stop> Stops { get; set; }
        [ForeignKey("OperatorLogin")]
        public virtual List<Tour> Tours { get; set; }
        [ForeignKey("OperatorLogin")]
        public virtual List<Guide> Guides { get; set; }        
    }
}
