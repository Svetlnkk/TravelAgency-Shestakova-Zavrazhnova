using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.StoragesContracts
{
   public interface IOperatorStorage
    {
        bool Registered(OperatorBindingModel model);
        void Insert(OperatorBindingModel model);
        bool Login(OperatorBindingModel model);
        OperatorBindingModel GetOperatorData(OperatorBindingModel model);
    }
}
