using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.BindingModels
{
    public class ExcursionBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Time { get; set; }
        public int Price { get; set; }
        public string TouristLogin { get; set; }
        public string OperatorLogin { get; set; }
    }
}
