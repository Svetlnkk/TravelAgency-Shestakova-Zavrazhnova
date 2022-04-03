using TravelAgencyDatabaseImplements.Models;
using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.StoragesContracts;
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
        public static string AutorizedOperator;
        public void Insert(OperatorBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            if (context.Operators.FirstOrDefault(rec => rec.Login == model.Login) != null)
            {
                throw new Exception("Пользователь с таким логином уже существует");
            }
            context.Operators.Add(new Operator { Login = model.Login, Password = model.Password });
            context.SaveChanges();
        }
        public bool Login(OperatorBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
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
            AutorizedOperator = model.Login;
            return true;
        }
    }
}
