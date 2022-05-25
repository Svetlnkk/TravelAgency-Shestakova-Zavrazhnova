using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgencyDatabaseImplements.Models;
using TravelAgencyContracts.BindingModels;
using TravelAgencyContracts.StoragesContracts;
using TravelAgencyContracts.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TravelAgencyDatabaseImplements.Implements
{
    public class TouristStorage : ITouristStorage
    {
        public static string AutorizedWorker;
        public bool Registered(TouristBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            if (context.Tourists.FirstOrDefault(rec => rec.Login == model.Login || rec.Email == model.Email) != null)
            {
                return true;
            }
            return false;
        }
        public void Insert(TouristBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            context.Tourists.Add(new Tourist { Login = model.Login, Password = model.Password, Email = model.Email, Name = model.Name });
            context.SaveChanges();
        }
        public bool Login(TouristBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            if (!context.Tourists.Contains(new Tourist { Login = model.Login, Password = model.Password }))
            {
                return false;
            }
            if (!context.Tourists.Select(rec => rec.Login).Contains(model.Login))
            {
                return false;
            }
            if (context.Tourists.FirstOrDefault(rec => rec.Login == model.Login).Password != model.Password)
            {
                return false;
            }
            AutorizedWorker = model.Login;
            return true;
        }
        public TouristBindingModel GetAutorizedWorker()
        {
            return GetElement(new TouristBindingModel() { Login = AutorizedWorker });
        }
        public TouristBindingModel GetElement(TouristBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelAgencyDatabase();
            var worker = context.Tourists.FirstOrDefault(rec => rec.Login == model.Login);
            return worker != null ? CreateModel(worker) : null;
        }
        public TouristBindingModel CreateModel(Tourist worker)
        {
            return new TouristBindingModel()
            {
                Email = worker.Email,
                Login = worker.Login,
                Password = worker.Password,
                Name = worker.Name
            };
        }

        public TouristBindingModel GetTouristData(TouristBindingModel model)
        {
            var tourist = GetElement(model);
            tourist.Password = "";
            return tourist;
        }
    }
}
