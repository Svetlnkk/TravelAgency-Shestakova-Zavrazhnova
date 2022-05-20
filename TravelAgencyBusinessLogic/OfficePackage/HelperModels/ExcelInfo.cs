using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.ViewModels;

namespace TravelAgencyBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<GuideViewModel> Guides { get; set; }
        public List<ExcursionViewModel> Excursions { get; set; }
    }
}
