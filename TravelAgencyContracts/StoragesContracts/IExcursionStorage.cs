using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.ViewModels;

namespace TravelAgencyContracts.StoragesContracts
{
    public interface IExcursionStorage
    {
        List<ExcursionViewModel> GetFullList();
        List<ExcursionViewModel> GetFilteredList(ExcursionBindingModel model);
        ExcursionViewModel GetElement(ExcursionBindingModel model);
        void Insert(ExcursionBindingModel model);
        void Update(ExcursionBindingModel model);
        void Delete(ExcursionBindingModel model);
    }
}
