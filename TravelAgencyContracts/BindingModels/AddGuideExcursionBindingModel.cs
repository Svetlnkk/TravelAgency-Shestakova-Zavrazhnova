using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.BindingModels
{
    public class AddGuideExcursionBindingModel
    {
        public int GuideId { get; set; }
        public int ExcursionId { get; set; }
        public int ExcursionCount { get; set; }
        public string OperatorLogin { get; set; }
    }
}
