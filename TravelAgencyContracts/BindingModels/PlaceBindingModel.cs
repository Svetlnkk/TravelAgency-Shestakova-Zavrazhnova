using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.BindingModels
{
    public class PlaceBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfVisit { get; set; }
        public string TouristLogin { get; set; }
        public int PlaceExcursion { get; set; }
    }
}
