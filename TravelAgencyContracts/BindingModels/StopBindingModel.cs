using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.BindingModels
{
   public class StopBindingModel
    {
        public int? Id { get; set; }
        public String OperatorLogin { get; set; }
        public int TourId { get; set; }
        public string StopName { get; set; }
        public DateTime CheckinDateStop { get; set; }
        public DateTime DateofDepatureStop { get; set; }
        
    }
}
