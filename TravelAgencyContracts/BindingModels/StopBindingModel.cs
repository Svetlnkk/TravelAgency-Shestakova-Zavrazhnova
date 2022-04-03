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
        public string StopName { get; set; }
        public DateTime CheckinDateStop { get; set; }
        public DateTime DateofDepatureStop { get; set; }
        public string OperatorLogin { get; set; }
        public int StopTour { get; set; }
    }
}
