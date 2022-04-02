using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.StoragesContracts
{
    public interface IStopStorage
    {
        List<StopViewModel> GetFullList();
        List<StopViewModel> GetFilteredList(StopBindingModel model);
        StopViewModel GetElement(StopBindingModel model);
        void Insert(StopBindingModel model);
        void Update(StopBindingModel model);
        void Delete(StopBindingModel model);
    }
}
