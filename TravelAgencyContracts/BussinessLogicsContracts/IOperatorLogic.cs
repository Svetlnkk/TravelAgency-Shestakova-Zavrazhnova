using TravelAgencyContracts.ViewModels;
using TravelAgencyContracts.BindingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyContracts.BussinessLogicsContracts
{
    public interface IOperatorLogic
    {
        void Create(OperatorBindingModel model);
        bool Login(OperatorBindingModel model);
    }
}
