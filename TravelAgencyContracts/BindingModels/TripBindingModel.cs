using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.BindingModels
{
    public class TripBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string TouristLogin { get; set; }
        public Dictionary<int, int> TripExcursions { get; set; }
        public Dictionary<int, int> TripTours { get; set; }
        public DateTime? after { get; set; }
        public DateTime? before { get; set; }
    }
}
