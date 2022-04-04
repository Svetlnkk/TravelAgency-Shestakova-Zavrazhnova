using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.ViewModels;

namespace TravelAgencyBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateAfter { get; set; }
        public DateTime DateBefore { get; set; }
        public List<ReportTripsPCView> Trips { get; set; }
    }
}
