using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.ViewModels;

namespace TravelAgencyContracts.BindingModels
{
    public class ReportBindingModel
    {
        public string FileName { get; set; }
        public DateTime? DateAfter { get; set; }
        public DateTime? DateBefore { get; set; }
        public List<TripViewModel>? trips { get; set; }
    }
}
