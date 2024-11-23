﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfAppAutorisation.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SoundEntities : DbContext
    {
        private static SoundEntities _context;
        public SoundEntities()
            : base("name=SoundEntities")
        {
        }

        public static SoundEntities GetContext() 
        {
            if(_context == null)
            {
                _context = new SoundEntities();
            }
            return _context;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Albums> Albums { get; set; }
        public virtual DbSet<Authorization> Authorization { get; set; }
        public virtual DbSet<Circulations> Circulations { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Equipment> Equipment { get; set; }
        public virtual DbSet<Equipment_type> Equipment_type { get; set; }
        public virtual DbSet<Geners> Geners { get; set; }
        public virtual DbSet<Geners_Singers> Geners_Singers { get; set; }
        public virtual DbSet<Jobtitles> Jobtitles { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Orders_Servises> Orders_Servises { get; set; }
        public virtual DbSet<Personal_data> Personal_data { get; set; }
        public virtual DbSet<Producers> Producers { get; set; }
        public virtual DbSet<Recordings> Recordings { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Services> Services { get; set; }
        public virtual DbSet<Singers> Singers { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<Stories> Stories { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
