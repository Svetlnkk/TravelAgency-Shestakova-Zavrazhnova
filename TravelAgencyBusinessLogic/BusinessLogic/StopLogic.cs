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
    public class StopLogic : IStopLogic
    {
        private readonly IStopStorage stopStorage;
        public StopLogic(IStopStorage stopStorage)
        {
            this.stopStorage = stopStorage;
        }
        public List<StopViewModel> Read(StopBindingModel model)
        {
            if (model == null)
            {
                return stopStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<StopViewModel> { stopStorage.GetElement(model) };
            }
            return stopStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(StopBindingModel model)
        {
            var element = stopStorage.GetElement(new StopBindingModel
            {
                StopName = model.StopName
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть остановка с таким названием");
            }
            if (model.Id.HasValue)
            {
                stopStorage.Update(model);
            }
            else
            {
                stopStorage.Insert(model);
            }
        }
        public void Delete(StopBindingModel model)
        {
            var element = stopStorage.GetElement(model);
            if (element == null)
            {
                throw new Exception("Остановка не найдена");
            }
            stopStorage.Delete(model);
        }
    }
}
