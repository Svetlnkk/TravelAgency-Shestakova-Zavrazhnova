using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.ViewModels
{
    public class ReportTripsPCView
    {
        public DateTime DateCreate { get; set; }
        public string Name { get; set; }
        public List<PlaceViewModel> Places { get; set; }
        public List<GuideViewModel> Guides { get; set; }
    }
}
