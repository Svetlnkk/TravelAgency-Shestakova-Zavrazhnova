using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.StoragesContracts;
using TravelAgencyContracts.BussinessLogicsContracts;
using TravelAgencyContracts.BindingModels;

namespace TravelAgencyBusinessLogic.BusinessLogics
{
    public class TripLogic : ITripLogic
    {
        private readonly ITripStorage tripStorage;
        private readonly IExcursionStorage excursionStorage;
        public TripLogic(ITripStorage tripStorage, IExcursionStorage excursionStorage)
        {
            this.tripStorage = tripStorage;
            this.excursionStorage = excursionStorage;
        }
        public List<TripViewModel> Read(TripBindingModel model)
        {
            if (model == null)
            {
                return tripStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<TripViewModel> { tripStorage.GetElement(model) };
            }
            return tripStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(TripBindingModel model)
        {
            if (model.Id.HasValue)
            {
                tripStorage.Update(model);
            }
            else
            {
                tripStorage.Insert(model);
            }
        }
        public void Delete(TripBindingModel model)
        {
            var element = tripStorage.GetElement(new TripBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            tripStorage.Delete(model);
        }
        public void AddExcursion((int, (int, int)) addedExcursion)
        {
            var trip = tripStorage.GetElement(new TripBindingModel { Id = addedExcursion.Item1 });
            if (trip == null)
            {
                throw new Exception("Путешествие не найдено");
            }
            var excursion = excursionStorage.GetElement(new ExcursionBindingModel { Id = addedExcursion.Item2.Item1 });
            if (excursion == null)
            {
                throw new Exception("Экскурсия не найдена");
            }
            tripStorage.AddExcursion(addedExcursion);
        }
    }
}
