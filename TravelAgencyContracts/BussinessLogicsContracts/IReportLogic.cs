using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BindingModels;

namespace TravelAgencyContracts.BussinessLogicsContracts
{
    public interface IReportLogic
    {
        List<GuideViewModel> GetGuidesByTrips(ReportBindingModel model);
        List<ReportTripsPCView> GetTripsPCView(ReportBindingModel model);
        void saveTripsToPdfFile(ReportBindingModel model);
        void saveGuidesToWord(ReportBindingModel model);
        void saveGuidesToExcel(ReportBindingModel model);
    }
}
