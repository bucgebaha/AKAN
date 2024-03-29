﻿using AKAN.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AKAN.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Banned> Banned { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<HospitalAcc> HospitalAccs { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<BloodType> BloodTypes { get; set; }
        public DbSet<AKAN.Models.Photo> Photo { get; set; }
    }
}