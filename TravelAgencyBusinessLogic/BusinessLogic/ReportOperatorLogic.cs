using TravelAgencyContracts.BussinessLogicsContracts;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.StoragesContracts;
using TravelAgencyContracts.BindingModels;
using TravelAgencyBusinessLogic.OfficePackage;
using TravelAgencyBusinessLogic.OfficePackage.HelperEnums;
using TravelAgencyBusinessLogic.OfficePackage.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class ReportOperatorLogic : IReportOperatorLogic
    {
        private readonly IExcursionStorage excursionStorage;
        private readonly IGuideStorage guideStorage;
        private readonly ITourStorage tourStorage;
        private readonly IOperatorStorage operatorStorage;
        private readonly AbstractSaveToPdf saveToPdf;
        private readonly AbstractSaveToWord saveToWord;
        private readonly AbstractSaveToExcel saveToExcel;
        public ReportOperatorLogic(IExcursionStorage excursionStorage, IGuideStorage guideStorage, ITourStorage tourStorage,
            IOperatorStorage operatorStorage, AbstractSaveToPdf saveToPdf, AbstractSaveToWord saveToWord, AbstractSaveToExcel saveToExcel)
        {
            this.excursionStorage = excursionStorage;
            this.guideStorage = guideStorage;            
            this.tourStorage = tourStorage;
            this.operatorStorage = operatorStorage;
            this.saveToPdf = saveToPdf;
            this.saveToWord = saveToWord;
            this.saveToExcel = saveToExcel;
        }
       
        public List<ReportGuidesViewModel> GetGuides(ReportOperatorBindingModel model)
        {
            var list = new List<ReportGuidesViewModel>();
            var guides =guideStorage.GetFilteredList(new GuideBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
            foreach (var guide in guides)
            {
                var record = new ReportGuidesViewModel
                {
                    GuideName = guide.GuideName,
                    DateCreate = guide.Date,
                    Excursions = new List<ExcursionViewModel>(),
                    Tours = new List<TourViewModel>()
                };
                foreach (var excursionG in guide.GuideExcursions)
                {
                    var excursion = excursionStorage.GetElement(new ExcursionBindingModel { Id = excursionG.Key });
                    record.Excursions.Add(excursion);
                }
                record.Tours = tourStorage.GetFullList()
                    .Where(rec => rec.TourGuides.Keys.ToList().Contains(guide.Id)).ToList();
                list.Add(record);
            }

            return list;
        }

        public List<ReportTourExcursionViewModel> GetTourExcursion(ReportOperatorBindingModel model)
        {
            var tours = model.tours;
            var list = new List<ReportTourExcursionViewModel>();
            foreach (var tour in tours)
            {
                var record = new ReportTourExcursionViewModel
                {
                    TourName = tour.TourName,
                    Excursions = new List<Tuple<string, string>>(),
                    GuideName = string.Empty
                };
                foreach (var guide in tour.TourGuides)
                {
                    var guidee = guideStorage.GetElement(new GuideBindingModel { Id = guide.Key });
                    foreach (var excursion in guidee.GuideExcursions)
                    {
                        record.Excursions.Add(new Tuple<string, string>(excursion.Value.Item1, excursion.Value.Item2));
                        record.GuideName = guidee.GuideName;
                    }
                }
                list.Add(record);
            }
            return list;
        }
        public void saveGuidesToPdfFile(ReportOperatorBindingModel model)
        {
            //var operatorr = operatorStorage.GetAutorizedOperator();
            saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список гидов",
                DateAfter = model.DateFrom.Value,
                DateBefore = model.DateTo.Value,
                //Guides = GetGuidesView(model)
            });
        }
       
        public void saveExcursionsToExcel(ReportOperatorBindingModel model)
        {
            saveToExcel.CreateReport(new ExcelInfo()
            {
                FileName = model.FileName,
                Title = "Список экскурсий:",
                //Excursions = GetExcursionsByTours(model)
            });
        }
        public void saveExcursionsToWord(ReportOperatorBindingModel model)
        {
            saveToWord.CreateDoc(new WordInfo()
            {
                FileName = model.FileName,
                Title = "Список экскурсий",
               // Excursions = GetExcursionsByTours(model)
            });
        }
    }
}
