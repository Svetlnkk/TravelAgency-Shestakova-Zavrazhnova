using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.ViewModels
{
    public class ReportTourExcursionViewModel
    {
        public string TourName { get; set; }
        public string GuideName { get; set; }
        public List<int> Excursions { get; set; }
    }
}
