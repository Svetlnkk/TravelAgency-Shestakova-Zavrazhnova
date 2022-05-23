using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.BussinessLogicsContracts
{
    public interface IReportOperatorLogic
    {
        List<ExcursionViewModel> GetTourExcursion(ReportOperatorBindingModel model);
        List<ReportGuidesViewModel> GetGuides(ReportOperatorBindingModel model);
        void saveGuidesToPdfFile(ReportOperatorBindingModel model);
        void saveExcursionsToWord(ReportOperatorBindingModel model);
        void saveExcursionsToExcel(ReportOperatorBindingModel model);
    }
}
