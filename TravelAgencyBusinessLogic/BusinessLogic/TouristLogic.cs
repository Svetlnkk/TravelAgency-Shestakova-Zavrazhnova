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
    public class TouristLogic : ITouristLogic
    {
        private readonly ITouristStorage touristStorage;
        public TouristLogic(ITouristStorage touristStorage)
        {
            this.touristStorage = touristStorage;
        }
        public void Create(TouristBindingModel model)
        {
            touristStorage.Insert(model);
        }
        public Boolean Login(TouristBindingModel model)
        {
            return touristStorage.Login(model);
        }
    }
}
