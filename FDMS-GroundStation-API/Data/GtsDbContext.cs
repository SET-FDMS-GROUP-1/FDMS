/*
* FILE : GtsDbContext.cs
* PROJECT : PROG3070 - PROJECT – MANUFACTURING SCENARIO
* PROGRAMMER : Nicholas Aguilar
* FIRST VERSION : 2025-11-22
* DESCRIPTION : File defining the Entity Framework database context for the Ground terminal station application.
*/
using Microsoft.EntityFrameworkCore;

namespace FDMS_GroundStation_API.Data {
    /*
     * NAME : GtsDbContext
     * PURPOSE : Configures the database context for the Ground terminal station application, defining DbSets for each model.
     */
    public class GtsDbContext : DbContext {
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

        }
    }
}
