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
                DateTo = model.DateTo,
                OperatorLogin=model.OperatorLogin
            });
            foreach (var guide in guides)
            {
                var record = new ReportGuidesViewModel
                {
                    GuideName = guide.GuideName,
                    DateCreate = guide.Date,
                    Cost=guide.Cost,
                    Excursions = new List<ExcursionViewModel>(),
                    Tours = new List<TourViewModel>()
                };
                foreach (var excursionG in guide.GuideExcursions)
                {
                    var excursion = excursionStorage.GetElement(new ExcursionBindingModel { Id = excursionG.Key });
                    record.Excursions.Add(excursion);
                }
                record.Tours = tourStorage.GetFullList().Where(rec => rec.GuideTours.Keys.ToList().Contains(guide.Id)).ToList();
                list.Add(record);
            }

            return list;
        }

        public List<ExcursionViewModel> GetTourExcursion(ReportOperatorBindingModel model)
        {
            /*var tours = model.tours;
            var list = new List<ReportTourExcursionViewModel>();
            foreach (var tour in tours)
            {
                var record = new ReportTourExcursionViewModel
                {
                    TourName = tour.TourName,
                    Excursions = new List<int>(),
                    GuideName = string.Empty
                };
                foreach (var guideKVP in tour.GuideTours)
                {
                    var guide = guideStorage.GetElement(new GuideBindingModel { Id = guideKVP.Key });
                    foreach (var excursion in guide.GuideExcursions)
                    {
                        record.Excursions.Add(excursion.Value.Item1);
                        record.GuideName = guide.GuideName;
                    }
                }
                list.Add(record);
            }*/
            var list = new List<ExcursionViewModel>();
            var listExcurId = new List<int>();
            foreach (var tour in model.tours)
            {
                var tourGuides = tour.GuideTours.Keys.ToList().Select(rec => guideStorage.GetElement(new GuideBindingModel { Id = rec }));
                foreach (var elem in tourGuides)
                {
                    listExcurId.AddRange(elem.GuideExcursions.Keys.ToList());
                }
            }
            list = listExcurId.Distinct().ToList().Select(rec => excursionStorage.GetElement(new ExcursionBindingModel { Id = rec })).ToList();
            return list;
        }
        public void saveGuidesToPdfFile(ReportOperatorBindingModel model)
        {            
            saveToPdf.CreateDocOperator(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список гидов",
                DateAfter = model.DateFrom.Value,
                DateBefore = model.DateTo.Value,
                Guides = GetGuides(model)
            });
        }
       
        public void saveExcursionsToExcel(ReportOperatorBindingModel model)
        {
            saveToExcel.CreateReportOperator(new ExcelInfo()
            {
                FileName = model.FileName,
                Title = "Список экскурсий:",
                Excursions = GetTourExcursion(model)
            });
        }
        public void saveExcursionsToWord(ReportOperatorBindingModel model)
        {
            saveToWord.CreateDocOperator(new WordInfo()
            {
                FileName = model.FileName,
                Title = "Список экскурсий",
                Excursions = GetTourExcursion(model)
            });
        }
    }
}
