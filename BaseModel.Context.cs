﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToursApp
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ToursBaseEntities : DbContext
    {

        private static ToursBaseEntities _context;



        public static ToursBaseEntities GetContext()
        {
            if (_context == null)
                _context = new ToursBaseEntities();

            return _context;
        }
        public ToursBaseEntities()
            : base("name=ToursBaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Hotelimage> Hotelimage { get; set; }
        public virtual DbSet<Hotell> Hotell { get; set; }
        public virtual DbSet<HotelOfTour> HotelOfTour { get; set; }
        public virtual DbSet<HottelComment> HottelComment { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tour> Tour { get; set; }
        public virtual DbSet<Type> Type { get; set; }
        public virtual DbSet<TypeOfTour> TypeOfTour { get; set; }
    }
}
