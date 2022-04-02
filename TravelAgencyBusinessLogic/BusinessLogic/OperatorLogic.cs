using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.StoragesContracts;
using TravelAgencyContracts.BussinessLogicsContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class OperatorLogic : IOperatorLogic
    {
        private readonly IOperatorStorage roleStorage;
        public OperatorLogic(IOperatorStorage roleStorage)
        {
            this.roleStorage = roleStorage;
        }
        public void Create(OperatorBindingModel model)
        {
            roleStorage.Insert(model);
        }
        public Boolean Login(OperatorBindingModel model)
        {
            return roleStorage.Login(model);
        }
    }
}
