/*
* FILE : GtsDbContext.cs
* PROJECT : SENG3020 - Flight Data Management System
* PROGRAMMER : Nicholas Aguilar
* FIRST VERSION : 2025-11-22
* DESCRIPTION : File defining the Entity Framework database context for the Ground terminal station application.
*/

using FDMS_GroundStation_API.Models;
using Microsoft.EntityFrameworkCore;

namespace FDMS_GroundStation_API.Data {
    /*
     * NAME : GtsDbContext
     * PURPOSE : Configures the database context for the Ground terminal station application, defining DbSets for each model.
     */
    public class GtsDbContext : DbContext {
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<GForceData> GForceData { get; set; }
        public DbSet<AltitudeData> AltitudeData { get; set; }
        public DbSet<DataError> DataErrors { get; set; }

        /*
         *	FUNCTION : GtsDbContext -- Constructor
         *	DESCRIPTION	: Constructor that accepts DbContext options and passes them to the base DbContext class.
         *	PARAMETERS :
         *      DbContextOptions<GtsDbContext> options - Options for configuring the database context.
         *	RETURNS : Nothing
         */
        public GtsDbContext(DbContextOptions<GtsDbContext> options) : base(options) { }

        /*
        *	FUNCTION : OnModelCreating
        *	DESCRIPTION	: Configures entity properties and relationships in the model.
        *	PARAMETERS :
        *      ModelBuilder modelBuilder - The model builder for configuring entity properties.
        *	RETURNS : Nothing
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Aircraft>(entity => {
                entity.Property(e => e.Id).IsUnicode(false);
            });


            modelBuilder.Entity<GForceData>(entity => {
                entity.Property(e => e.Weight).HasPrecision(10, 6);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.CreatedDate)
                    .ValueGeneratedOnAdd();

                entity.HasOne(e => e.Aircraft)
                      .WithMany(a => a.GForceData)
                      .HasForeignKey(e => e.AircraftId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<AltitudeData>(entity => {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.CreatedDate)
                    .ValueGeneratedOnAdd();

                entity.HasOne(e => e.Aircraft)
                      .WithMany(a => a.AltitudeData)
                      .HasForeignKey(e => e.AircraftId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DataError>(entity => {
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.CreatedDate)
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.RawData).IsUnicode(false);
            });
        }
    }
}