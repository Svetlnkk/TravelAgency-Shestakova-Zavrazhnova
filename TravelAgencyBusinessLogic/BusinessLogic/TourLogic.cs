using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.StoragesContracts;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BussinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class TourLogic : ITourLogic
    {
        private readonly ITourStorage tourStorage;
        public TourLogic(ITourStorage tourStorage)
        {
            this.tourStorage = tourStorage;
        }
        public List<TourViewModel> Read(TourBindingModel model)
        {
            if (model == null)
            {
                return tourStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TourViewModel> { tourStorage.GetElement(model) };
            }
            return tourStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(TourBindingModel model)
        {
            var element = tourStorage.GetElement(new TourBindingModel
            {
                TourName = model.TourName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть тур с таким названием");
            }
            if (model.Id.HasValue)
            {
                tourStorage.Update(model);
            }
            else
            {
                tourStorage.Insert(model);
            }
        }
        public void Delete(TourBindingModel model)
        {
            var element = tourStorage.GetElement(new TourBindingModel
            {
                Id = model.Id
            });
            if (element == null)
            {
                throw new Exception("Тур не найден");
            }
            tourStorage.Delete(model);
        }
    }
}
