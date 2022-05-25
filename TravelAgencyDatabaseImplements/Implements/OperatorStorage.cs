using TravelAgencyDatabaseImplements.Models;
using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.StoragesContracts;
using TravelAgencyContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TravelAgencyDatabaseImplements.Implements
{
    public class OperatorStorage : IOperatorStorage
    {
        public bool Registered(OperatorBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                if (context.Operators.FirstOrDefault(rec => rec.Login == model.Login || rec.Email == model.Email) != null)
                {
                    return true;
                }
                return false;
            }
        }
        public void Insert(OperatorBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                context.Operators.Add(new Operator { Login = model.Login, Password = model.Password, Email = model.Email, Name = model.Name });
                context.SaveChanges();
            }
        }
        public bool Login(OperatorBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                if (!context.Operators.Contains(new Operator { Login = model.Login, Password = model.Password }))
                {
                    return false;
                }
                if (!context.Operators.Select(rec => rec.Login).Contains(model.Login))
                {
                    return false;
                }
                if (context.Operators.FirstOrDefault(rec => rec.Login == model.Login).Password != model.Password)
                {
                    return false;
                }
                return true;
            }
        }
        public OperatorBindingModel GetOperatorData(OperatorBindingModel model)
        {
            var oper = GetElement(model);
            oper.Password = "";
            return oper;
        }
        public OperatorBindingModel GetElement(OperatorBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new TravelAgencyDatabase())
            {
                var oper = context.Operators.FirstOrDefault(rec => rec.Login == model.Login);
                return oper != null ? CreateModel(oper) : null;
            }
        }
        public OperatorBindingModel CreateModel(Operator oper)
        {
            return new OperatorBindingModel()
            {
                Email = oper.Email,
                Login = oper.Login,
                Password = oper.Password,
                Name = oper.Name
            };
        }
    }
}

