using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BindingModels;

namespace TravelAgencyContracts.StoragesContracts
{
    public interface ITouristStorage
    {
        TouristBindingModel GetAutorizedWorker();
        bool Registered(TouristBindingModel model);
        void Insert(TouristBindingModel model);
        bool Login(TouristBindingModel model);
    }
}
