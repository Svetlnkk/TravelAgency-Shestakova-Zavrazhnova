using System;
using TravelAgencyDatabaseImplements.Models;
using Microsoft.EntityFrameworkCore;

namespace TravelAgencyDatabaseImplements
{
    public class TravelAgencyDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-0K83PAS6\SQLEXPRESS;Initial Catalog=TravelAgencyDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Excursion> Excursions { get; set; }
        public virtual DbSet<GuideExcursion> GuideExcursions { get; set; }
        public virtual DbSet<ExcursionTour> ExcursionTours { get; set; }
        public virtual DbSet<Guide> Guides { get; set; }
        public virtual DbSet<Operator> Operators { get; set; }
        public virtual DbSet<Place> Places { get; set; }
        public virtual DbSet<Stop> Stops { get; set; }
        public virtual DbSet<Tour> Tours { get; set; }
        public virtual DbSet<GuideTour> GuideTours { get; set; }
        public virtual DbSet<Tourist> Tourists { get; set; }
        public virtual DbSet<Trip> Trips { get; set; }
        public virtual DbSet<TripExcursions> TripExcursions { get; set; }
        public virtual DbSet<TripTours> TripTours { get; set; }
    }
}
