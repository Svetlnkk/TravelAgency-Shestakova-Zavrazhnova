using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.BussinessLogicsContracts;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.StoragesContracts;
using TravelAgencyContracts.BindingModels;
using TravelAgencyBusinessLogic.OfficePackage;
using TravelAgencyBusinessLogic.OfficePackage.HelperEnums;
using TravelAgencyBusinessLogic.OfficePackage.HelperModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class ReportLogic : IReportLogic
    {
        private readonly ITripStorage tripStorage;
        private readonly IPlaceStorage placeStorage;
        private readonly IGuideStorage guideStorage;
        private readonly ITourStorage tourStorage;
        private readonly ITouristStorage workerStorage;        
        private readonly AbstractSaveToPdf saveToPdf;
        private readonly AbstractSaveToWord saveToWord;
        private readonly AbstractSaveToExcel saveToExcel;
        public ReportLogic(ITripStorage tripStorage, IPlaceStorage placeStorage, IGuideStorage guideStorage, ITourStorage tourStorage,
            ITouristStorage workerStorage, AbstractSaveToPdf saveToPdf, AbstractSaveToWord saveToWord, AbstractSaveToExcel saveToExcel)
        {
            this.guideStorage = guideStorage;
            this.placeStorage = placeStorage;
            this.tripStorage = tripStorage;
            this.tourStorage = tourStorage;
            this.workerStorage = workerStorage;            
            this.saveToPdf = saveToPdf;
            this.saveToWord = saveToWord;
            this.saveToExcel = saveToExcel;
        }
        public List<ReportTripsPCView> GetTripsPCView(ReportBindingModel model)
        {
            var list = new List<ReportTripsPCView>();
            var trips = tripStorage.GetFilteredList(new TripBindingModel
            {
                after = model.DateAfter,
                before = model.DateBefore,
                TouristLogin = model.TouristLogin
            });
            foreach (var trip in trips)
            {
                var record = new ReportTripsPCView
                {
                    DateCreate = trip.Date,
                    Name = trip.Name,
                    Guides = new List<GuideViewModel>(),
                    Places = new List<PlaceViewModel>()
                };
                var tripTours = trip.TripTours.Keys.ToList().Select(rec => tourStorage.GetElement(new TourBindingModel { Id = rec }));
                var listGuideIds = new List<int>();
                foreach (var elem in tripTours)
                {
                   listGuideIds.AddRange(elem.GuideTours.Keys.ToList());

                }
                record.Guides = listGuideIds.Distinct().ToList().Select(rec => guideStorage.GetElement(new GuideBindingModel { Id = rec })).ToList();
                var tripExcursions = trip.TripExcursions.Keys.ToList();
                var tripPlaces = placeStorage.GetFilteredList(new PlaceBindingModel { TouristLogin = model.TouristLogin }).Where(rec => tripExcursions.Contains(rec.PlaceExcursion)).ToList();
                record.Places = tripPlaces;
                record.Places = tripPlaces;
                list.Add(record);
            }
            return list;
        }
        
        public void saveTripsToPdfFile(ReportBindingModel model)
        {
            saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список экскурсий",
                DateAfter = model.DateAfter.Value,
                DateBefore = model.DateBefore.Value,
                Trips = GetTripsPCView(model)
            });
        }
        public List<GuideViewModel> GetGuidesByTrips(ReportBindingModel model)
        {
            var list = new List<GuideViewModel>();
            var listGuideIds = new List<int>();
            foreach (var trip in model.trips)
            {
                var tripTours = trip.TripTours.Keys.ToList().Select(rec => tourStorage.GetElement(new TourBindingModel { Id = rec }));
                foreach (var elem in tripTours)
                {
                    Console.WriteLine(elem.TourName, elem.Id);
                    listGuideIds.AddRange(elem.GuideTours.Keys.ToList());
                }
            }
            list = listGuideIds.Distinct().ToList().Select(rec => guideStorage.GetElement(new GuideBindingModel { Id = rec })).ToList();
            return list;
        }
        public void saveGuidesToExcel(ReportBindingModel model)
        {
            saveToExcel.CreateReport(new ExcelInfo()
            {
                FileName = model.FileName,
                Title = "Список гидов:",
                Guides = GetGuidesByTrips(model)
            });
        }
        public void saveGuidesToWord(ReportBindingModel model)
        {
            saveToWord.CreateDoc(new WordInfo()
            {
                FileName = model.FileName,
                Title = "Список гидов",
                Guides = GetGuidesByTrips(model)
            });
        }
    }
}
