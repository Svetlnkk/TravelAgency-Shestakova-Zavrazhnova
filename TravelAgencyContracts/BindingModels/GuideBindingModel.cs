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
        public int Cost { get; set; }
        public DateTime Date { get; set; }
        public string OperatorLogin { get; set; }
        public Dictionary<int, int> GuideExcursions { get; set; }
        public Dictionary<int, int> GuideTours { get; set; }
        public DateTime? after { get; set; }
        public DateTime? before { get; set; }
    }
}
