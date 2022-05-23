using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.ViewModels
{
    public class ReportGuidesViewModel
    {
        public DateTime DateCreate { get; set; }
        public string GuideName { get; set; }
        public decimal Cost { get; set; }
        public List<TourViewModel> Tours { get; set; }
        public List<ExcursionViewModel> Excursions { get; set; }
    }
}
