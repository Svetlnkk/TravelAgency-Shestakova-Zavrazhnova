using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.StoragesContracts;
using TravelAgencyContracts.BussinessLogicsContracts;
using TravelAgencyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class OperatorLogic : IOperatorLogic
    {
        private readonly IOperatorStorage operatorStorage;
        public OperatorLogic(IOperatorStorage operatorStorage)
        {
            this.operatorStorage = operatorStorage;
        }
        public void Create(OperatorBindingModel model)
        {
            if (!operatorStorage.Registered(model))
            {
                operatorStorage.Insert(model);
            }
            else
            {
                throw new Exception("Оператор с таким логином или почтой уже существует");
            }
        }
        public Boolean Login(OperatorBindingModel model)
        {
            return operatorStorage.Login(model);
        }
        public OperatorBindingModel GetOperatorData(OperatorBindingModel model)
        {
            return operatorStorage.GetOperatorData(model);
        }
    }
}
