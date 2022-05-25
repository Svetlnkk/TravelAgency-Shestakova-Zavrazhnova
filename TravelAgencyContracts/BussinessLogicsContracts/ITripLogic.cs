using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BindingModels;

namespace TravelAgencyContracts.BussinessLogicsContracts
{
    public interface ITripLogic
    {
        List<TripViewModel> Read(TripBindingModel model);
        void CreateOrUpdate(TripBindingModel model);
        void Delete(TripBindingModel model);
        void AddExcursion(AddTripExcursionBindingModel addedExcursion);
    }
}
