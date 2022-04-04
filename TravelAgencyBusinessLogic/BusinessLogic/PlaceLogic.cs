using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.StoragesContracts;
using TravelAgencyContracts.BussinessLogicsContracts;
using TravelAgencyContracts.BindingModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class PlaceLogic : IPlaceLogic
    {
        private readonly IPlaceStorage placeStorage;
        public PlaceLogic(IPlaceStorage placeStorage)
        {
            this.placeStorage = placeStorage;
        }
        public List<PlaceViewModel> Read(PlaceBindingModel model)
        {
            if (model == null)
            {
                return placeStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<PlaceViewModel> { placeStorage.GetElement(model) };
            }
            return placeStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(PlaceBindingModel model)
        {
            if (model.Id.HasValue)
            {
                placeStorage.Update(model);
            }
            else
            {
                placeStorage.Insert(model);
            }
        }
        public void Delete(PlaceBindingModel model)
        {
            var element = placeStorage.GetElement(new PlaceBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элмент не найден");
            }
            placeStorage.Delete(model);
        }
    }
}
