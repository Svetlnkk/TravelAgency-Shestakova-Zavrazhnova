using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.ViewModels;

namespace TravelAgencyContracts.StoragesContracts
{
    public interface IPlaceStorage
    {
        List<PlaceViewModel> GetFullList();
        List<PlaceViewModel> GetFilteredList(PlaceBindingModel model);
        PlaceViewModel GetElement(PlaceBindingModel model);
        void Insert(PlaceBindingModel model);
        void Update(PlaceBindingModel model);
        void Delete(PlaceBindingModel model);
    }
}
