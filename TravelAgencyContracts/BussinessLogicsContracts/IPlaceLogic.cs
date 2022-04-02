using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BindingModels;

namespace TravelAgencyContracts.BussinessLogicsContracts
{
    public interface IPlaceLogic
    {
        List<PlaceViewModel> Read(PlaceBindingModel model);
        void CreateOrUpdate(PlaceBindingModel model);
        void Delete(PlaceBindingModel model);
    }
}
