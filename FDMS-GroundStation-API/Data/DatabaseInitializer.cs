/*
 * FILE : DatabaseInitializer.cs
 * PROJECT : SENG3020 - Flight Data Management System
 * PROGRAMMER : Nicholas Aguilar
 * FIRST VERSION : 2025-11-22
 * DESCRIPTION : Initializes the database ensuring its creation.
 */

using FDMS_GroundStation_API.Models;

namespace FDMS_GroundStation_API.Data {
    /*
     * NAME : DatabaseInitializer
     * PURPOSE : Database initialization and seeding with default data.
     */
    public class DbInitializer {
        /*
         *	FUNCTION : Initialize
         *	DESCRIPTION	: Initializes the database and seeds it with default data if necessary.
         *	PARAMETERS :
         *      IServiceProvider serviceProvider - Service provider for dependency injection, used to obtain the database context and logger.
         *	RETURNS : Nothing
         */
        public static void Initialize(IServiceProvider serviceProvider) {
            using(IServiceScope scope = serviceProvider.CreateScope()) {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<DbInitializer>>();
                var context = scope.ServiceProvider.GetRequiredService<GtsDbContext>();

                try {
                    logger.LogInformation("Check if database exists");
                    context.Database.EnsureCreated();

                } catch(Exception ex) {
                    logger.LogError(ex, "An error has occured while trying to initialize the database");
                }
            }
        }
    }
}