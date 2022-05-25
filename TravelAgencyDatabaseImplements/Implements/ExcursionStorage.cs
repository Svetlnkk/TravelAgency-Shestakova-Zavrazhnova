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
    public class ExcursionStorage : IExcursionStorage
    {
        public List<ExcursionViewModel> GetFullList()
        {
            using var context = new TravelAgencyDatabase();
            return context.Excursions.Select(CreateModel).ToList();
        }
        public List<ExcursionViewModel> GetFilteredList(ExcursionBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelAgencyDatabase();
            return context.Excursions
               // .Where(rec => !String.IsNullOrEmpty(model.TouristLogin) && rec.TouristLogin == model.TouristLogin)
                            .Select(CreateModel).ToList();
        }
        public ExcursionViewModel GetElement(ExcursionBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelAgencyDatabase();
            var excursion = context.Excursions
                //.Where(rec => !String.IsNullOrEmpty(model.TouristLogin) && rec.TouristLogin == model.TouristLogin)
                            .FirstOrDefault(rec => rec.Id == model.Id); return excursion != null ? CreateModel(excursion) : null;
        }
        public void Insert(ExcursionBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            model.TouristLogin = model.TouristLogin;
            context.Excursions.Add(CreateModel(model, new Excursion()));
            context.SaveChanges();
        }
        public void Update(ExcursionBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            var element = context.Excursions
                //.Where(rec => !String.IsNullOrEmpty(model.TouristLogin) && rec.TouristLogin == model.TouristLogin)
                            .FirstOrDefault(rec => rec.Id == model.Id); if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }
        public void Delete(ExcursionBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            Excursion element = context.Excursions
                //.Where(rec => !String.IsNullOrEmpty(model.TouristLogin) && rec.TouristLogin == model.TouristLogin)
                            .FirstOrDefault(rec => rec.Id == model.Id); if (element != null)
            {
                context.Excursions.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static Excursion CreateModel(ExcursionBindingModel model, Excursion excursion)
        {
            excursion.Name = model.Name;
            excursion.Type = model.Type;
            excursion.Time = model.Time;
            excursion.Price = model.Price;
            excursion.TouristLogin = model.TouristLogin;
            return excursion;
        }
        private static ExcursionViewModel CreateModel(Excursion excursion)
        {
            return new ExcursionViewModel
            {
                Id = excursion.Id,
                Name = excursion.Name,
                Type = excursion.Type,
                Time = excursion.Time,
                Price = excursion.Price,
                TouristLogin = excursion.TouristLogin
            };
        }
    }
}
