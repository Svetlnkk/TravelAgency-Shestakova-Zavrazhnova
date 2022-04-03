﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgencyDatabaseImplements;

namespace TravelAgencyDatabaseImplements.Migrations
{
    [DbContext(typeof(TravelAgencyDatabase))]
    partial class TravelAgencyDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Excursion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<double>("Time")
                        .HasColumnType("float");

                    b.Property<string>("TouristLogin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Excursions");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.ExcursionTour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExcursionId")
                        .HasColumnType("int");

                    b.Property<int>("TourCount")
                        .HasColumnType("int");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExcursionId");

                    b.HasIndex("TourId");

                    b.ToTable("ExcursionTours");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Guide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("GuideName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperatorLogin")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Guides");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.GuideExcursion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExcursionCount")
                        .HasColumnType("int");

                    b.Property<int>("ExcursionId")
                        .HasColumnType("int");

                    b.Property<int>("GuideId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExcursionId");

                    b.HasIndex("GuideId");

                    b.ToTable("GuideExcursions");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.GuideTour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GuideId")
                        .HasColumnType("int");

                    b.Property<int>("TourCount")
                        .HasColumnType("int");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GuideId");

                    b.HasIndex("TourId");

                    b.ToTable("GuideTours");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Operator", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Login");

                    b.ToTable("Operators");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfVisit")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExcursionId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TouristLogin")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExcursionId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Stop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CheckinDateStop")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateofDepatureStop")
                        .HasColumnType("datetime2");

                    b.Property<string>("OperatorLogin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StopName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TourId");

                    b.ToTable("Stops");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OperatorLogin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TourName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Tourist", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Login");

                    b.ToTable("Tourists");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Trip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TouristLogin")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.TripExcursions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ExcursionCount")
                        .HasColumnType("int");

                    b.Property<int>("ExcursionId")
                        .HasColumnType("int");

                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExcursionId");

                    b.HasIndex("TripId");

                    b.ToTable("TripExcursions");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.TripTours", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("TourCount")
                        .HasColumnType("int");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TourId");

                    b.HasIndex("TripId");

                    b.ToTable("TripTours");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.ExcursionTour", b =>
                {
                    b.HasOne("TravelAgencyDatabaseImplements.Models.Excursion", "Excursion")
                        .WithMany()
                        .HasForeignKey("ExcursionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAgencyDatabaseImplements.Models.Tour", "Tour")
                        .WithMany("ExcursionTours")
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Excursion");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.GuideExcursion", b =>
                {
                    b.HasOne("TravelAgencyDatabaseImplements.Models.Excursion", "Excursion")
                        .WithMany()
                        .HasForeignKey("ExcursionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAgencyDatabaseImplements.Models.Guide", "Guide")
                        .WithMany("GuideExcursions")
                        .HasForeignKey("GuideId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Excursion");

                    b.Navigation("Guide");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.GuideTour", b =>
                {
                    b.HasOne("TravelAgencyDatabaseImplements.Models.Guide", "Guide")
                        .WithMany("GuideTours")
                        .HasForeignKey("GuideId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAgencyDatabaseImplements.Models.Tour", "Tour")
                        .WithMany()
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Guide");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Place", b =>
                {
                    b.HasOne("TravelAgencyDatabaseImplements.Models.Excursion", "Excursion")
                        .WithMany()
                        .HasForeignKey("ExcursionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Excursion");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Stop", b =>
                {
                    b.HasOne("TravelAgencyDatabaseImplements.Models.Tour", "Tour")
                        .WithMany()
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.TripExcursions", b =>
                {
                    b.HasOne("TravelAgencyDatabaseImplements.Models.Excursion", "Excursion")
                        .WithMany("TripExcursions")
                        .HasForeignKey("ExcursionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAgencyDatabaseImplements.Models.Trip", "Trip")
                        .WithMany("TripExcursions")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Excursion");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.TripTours", b =>
                {
                    b.HasOne("TravelAgencyDatabaseImplements.Models.Tour", "Tour")
                        .WithMany()
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAgencyDatabaseImplements.Models.Trip", "Trip")
                        .WithMany("TripTours")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tour");

                    b.Navigation("Trip");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Excursion", b =>
                {
                    b.Navigation("TripExcursions");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Guide", b =>
                {
                    b.Navigation("GuideExcursions");

                    b.Navigation("GuideTours");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Tour", b =>
                {
                    b.Navigation("ExcursionTours");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Trip", b =>
                {
                    b.Navigation("TripExcursions");

                    b.Navigation("TripTours");
                });
#pragma warning restore 612, 618
        }
    }
}
