using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.ViewModels;

namespace TravelAgencyContracts.BindingModels
{
    public class ReportOperatorBindingModel
    {
        public string FileName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }        
        public List<GuideViewModel>? guides { get; set; }
        public List<TourViewModel>? tours { get; set; }
        public String OperatorLogin { get; set; }
    }
}
