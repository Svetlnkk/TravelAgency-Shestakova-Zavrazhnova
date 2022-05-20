using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.BindingModels
{
    public class GuideBindingModel
    {
        public int? Id { get; set; }
        public string GuideName { get; set; }
        public decimal Cost { get; set; }
        public DateTime Date { get; set; }        
        public Dictionary<int, int> GuideExcursions { get; set; }
        public string OperatorLogin { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
