using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.BindingModels
{
    public class AddTripExcursionBindingModel
    {
        public int TripId { get; set; }
        public int ExcursionId { get; set; }
        public int ExcursionCount { get; set; }
        public string TouristLogin { get; set; }
    }
}
