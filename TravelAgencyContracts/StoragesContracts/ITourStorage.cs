using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.StoragesContracts
{
    public interface ITourStorage
    {
        List<TourViewModel> GetFullList();
        List<TourViewModel> GetFilteredList(TourBindingModel model);
        TourViewModel GetElement(TourBindingModel model);
        void Insert(TourBindingModel model);
        void Update(TourBindingModel model);
        void Delete(TourBindingModel model);
        
    }
}
