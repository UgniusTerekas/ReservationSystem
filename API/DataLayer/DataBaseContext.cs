﻿using DataLayer.Entities.Category;
using DataLayer.Entities.City;
using DataLayer.Entities.EntertainmentItem;
using DataLayer.Entities.Gallery;
using DataLayer.Entities.Reservation;
using DataLayer.Entities.Review;
using DataLayer.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataLayer
{
    public class DataBaseContext : DbContext
    {
        public virtual DbSet<UserEntity> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<State> States { get; set; }

        public virtual DbSet<CategoryEntity> Categories { get; set; }

        public virtual DbSet<CityEntity> Cities { get; set; }

        public virtual DbSet<GalleryEntity> Gallery { get; set; }

        public virtual DbSet<ReviewEntity> Reviews { get; set; }

        public virtual DbSet<EntertainmentItemEntity> Entertainments { get; set; }

        public virtual DbSet<ReservationEntity> Reservations { get; set; }

        public DataBaseContext()
        {
        }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.State)
                .WithMany()
                .HasForeignKey(u => u.StateId);

            modelBuilder.Entity<EntertainmentItemEntity>()
                .HasOne(x => x.User)
                .WithMany(x => x.Entertainments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public static string GetConnectionString()
        {
            return "Server=localhost\\SQLEXPRESS;Database=reservation;Trusted_Connection=True;Encrypt=False;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(GetConnectionString(), sqlServerOptions => sqlServerOptions.CommandTimeout(360));
            }
        }
    }
}