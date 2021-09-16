using Arduino.Windows.Configurator.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arduino.Windows.Configurator.Persistence
{
    public class ArduinoRepositry : DbContext
    {
        private DbContextOptions<ArduinoRepositry> options;

        public ArduinoRepositry(DbContextOptions<ArduinoRepositry> options) : base(options)
        {
            this.options = options;
        }

        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Secrets> Secrets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
