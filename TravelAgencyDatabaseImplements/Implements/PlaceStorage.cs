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
    public class PlaceStorage : IPlaceStorage
    {
        public List<PlaceViewModel> GetFullList()
        {
            using var context = new TravelAgencyDatabase();
            return context.Places.Select(CreateModel).ToList();
        }
        public List<PlaceViewModel> GetFilteredList(PlaceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelAgencyDatabase();
            return context.Places.Where(rec =>
                            !String.IsNullOrEmpty(model.TouristLogin) && rec.TouristLogin == model.TouristLogin)
                            .Select(CreateModel).ToList();
        }
        public PlaceViewModel GetElement(PlaceBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new TravelAgencyDatabase();
            var place = context.Places.Where(rec => !String.IsNullOrEmpty(model.TouristLogin) && rec.TouristLogin == model.TouristLogin).FirstOrDefault(rec => rec.Id == model.Id);
            return place != null ? CreateModel(place) : null;
        }
        public void Insert(PlaceBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Place place = new Place()
                {
                    Name = model.Name,
                    DateOfVisit = model.DateOfVisit,
                    TouristLogin = model.TouristLogin,
                    ExcursionId = model.PlaceExcursion,
                    Excursion = context.Excursions.FirstOrDefault(rec => rec.Id == model.PlaceExcursion)
                };
                context.Places.Add(place);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Update(PlaceBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Places.Where(rec =>
                                    !String.IsNullOrEmpty(model.TouristLogin) && rec.TouristLogin == model.TouristLogin)
                                    .FirstOrDefault(rec => rec.Id == model.Id); if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                element.Name = model.Name;
                element.DateOfVisit = model.DateOfVisit;
                element.ExcursionId = model.PlaceExcursion;
                element.Excursion = context.Excursions.FirstOrDefault(rec => rec.Id == model.PlaceExcursion);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public void Delete(PlaceBindingModel model)
        {
            using var context = new TravelAgencyDatabase();
            Place element = context.Places.Where(rec =>
                !String.IsNullOrEmpty(model.TouristLogin) && rec.TouristLogin == model.TouristLogin)
                .FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Places.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        private static PlaceViewModel CreateModel(Place place)
        {
            return new PlaceViewModel
            {
                Id = place.Id,
                Name = place.Name,
                DateOfVisit = place.DateOfVisit,
                TouristLogin = place.TouristLogin,
                PlaceExcursion = place.ExcursionId
            };
        }
    }
}
