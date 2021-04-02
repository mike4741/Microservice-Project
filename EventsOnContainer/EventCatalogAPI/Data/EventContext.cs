﻿using EventCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Data
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<EventAddress>  Addresses { get; set; }
        public DbSet<EventCategory>  EventCategories { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<EventItem> EventItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventAddress>(e =>
            {
                e.Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
                e.Property(a => a.StreetAddress).IsRequired().HasMaxLength(100);
                e.Property(a => a.City).IsRequired().HasMaxLength(50);
                e.Property(a => a.State).IsRequired().HasMaxLength(50);
                e.Property(a => a.ZipCode).IsRequired();
            }); //EventAddressTable

            modelBuilder.Entity<EventCategory>(c=> {
                c.Property(c => c.Id).ValueGeneratedOnAdd().IsRequired();
                c.Property(c => c.Category).IsRequired().HasMaxLength(100);
               });//EVentCategory table

            modelBuilder.Entity<EventType>(t =>
            {
                t.Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
                t.Property(t => t.Type).HasMaxLength(100);

            });//EventType Table

            modelBuilder.Entity<EventItem>(i =>
            {
                i.Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
                i.Property(i => i.EventName).IsRequired().HasMaxLength(100);
                i.Property(i => i.Description).HasMaxLength(100);
                i.Property(i => i.Price);
                i.Property(i => i.EventImageUrl).HasMaxLength(100).IsRequired();
                i.Property(i => i.EventStartTime).IsRequired();
                i.Property(i => i.EventEndTime).IsRequired();
                i.HasOne(i => i.Address).WithMany().HasForeignKey(i => i.AddressId);
                i.HasOne(i => i.EventCategory).WithMany().HasForeignKey(i => i.CatagoryId);
                i.HasOne(i => i.EventType).WithMany().HasForeignKey(i => i.TypeId);

            }); 
            //EventItem Table
  
        }

    }
}
