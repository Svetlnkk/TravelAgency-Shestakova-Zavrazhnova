﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelAgencyDatabaseImplements;

namespace TravelAgencyDatabaseImplements.Migrations
{
    [DbContext(typeof(TravelAgencyDatabase))]
    [Migration("20220519213037_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TouristLogin");

                    b.ToTable("Excursions");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.ExcursionGuide", b =>
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

                    b.ToTable("ExcursionGuides");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Guide", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("GuideName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OperatorLogin")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("OperatorLogin");

                    b.ToTable("Guides");
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

                    b.ToTable("TourGuides");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Operator", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ExcursionId");

                    b.HasIndex("TouristLogin");

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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StopName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TourId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OperatorLogin");

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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TourName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OperatorLogin");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Tourist", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TouristLogin");

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

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Excursion", b =>
                {
                    b.HasOne("TravelAgencyDatabaseImplements.Models.Tourist", null)
                        .WithMany("Excursions")
                        .HasForeignKey("TouristLogin");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.ExcursionGuide", b =>
                {
                    b.HasOne("TravelAgencyDatabaseImplements.Models.Excursion", "Excursion")
                        .WithMany()
                        .HasForeignKey("ExcursionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAgencyDatabaseImplements.Models.Guide", "Guide")
                        .WithMany("ExcursionGuides")
                        .HasForeignKey("GuideId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Excursion");

                    b.Navigation("Guide");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Guide", b =>
                {
                    b.HasOne("TravelAgencyDatabaseImplements.Models.Operator", "Operator")
                        .WithMany("Guides")
                        .HasForeignKey("OperatorLogin");

                    b.Navigation("Operator");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.GuideTour", b =>
                {
                    b.HasOne("TravelAgencyDatabaseImplements.Models.Guide", "Guide")
                        .WithMany("TourGuides")
                        .HasForeignKey("GuideId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TravelAgencyDatabaseImplements.Models.Tour", "Tour")
                        .WithMany("TourGuides")
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

                    b.HasOne("TravelAgencyDatabaseImplements.Models.Tourist", null)
                        .WithMany("Places")
                        .HasForeignKey("TouristLogin");

                    b.Navigation("Excursion");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Stop", b =>
                {
                    b.HasOne("TravelAgencyDatabaseImplements.Models.Operator", "Operator")
                        .WithMany("Stops")
                        .HasForeignKey("OperatorLogin");

                    b.HasOne("TravelAgencyDatabaseImplements.Models.Tour", "Tour")
                        .WithMany()
                        .HasForeignKey("TourId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Operator");

                    b.Navigation("Tour");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Tour", b =>
                {
                    b.HasOne("TravelAgencyDatabaseImplements.Models.Operator", "Operator")
                        .WithMany("Tours")
                        .HasForeignKey("OperatorLogin");

                    b.Navigation("Operator");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Trip", b =>
                {
                    b.HasOne("TravelAgencyDatabaseImplements.Models.Tourist", null)
                        .WithMany("Trips")
                        .HasForeignKey("TouristLogin");
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
                    b.Navigation("ExcursionGuides");

                    b.Navigation("TourGuides");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Operator", b =>
                {
                    b.Navigation("Guides");

                    b.Navigation("Stops");

                    b.Navigation("Tours");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Tour", b =>
                {
                    b.Navigation("TourGuides");
                });

            modelBuilder.Entity("TravelAgencyDatabaseImplements.Models.Tourist", b =>
                {
                    b.Navigation("Excursions");

                    b.Navigation("Places");

                    b.Navigation("Trips");
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