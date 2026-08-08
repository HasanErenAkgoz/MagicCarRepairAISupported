using MagicCarRepairAISupported.Persistence.Context;

using MagicCarRepairAISupported.Persistence.Seeds;

using MagicCarRepairAISupported.WebAPI.Authorization;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicCarRepairAISupported.WebAPI.Controllers

{

    [ApiController]

    [Route("api/[controller]")]

    [Authorize(Policy = AuthPolicyNames.SystemAdminOnly)]

    public class SeedDataController : ControllerBase

    {

        private readonly BaseDbContext _context;

        private readonly ILogger<SeedDataController> _logger;



        public SeedDataController(BaseDbContext context, ILogger<SeedDataController> logger)

        {

            _context = context;

            _logger = logger;

        }



        /// <summary>

        /// Seed data'yı manuel olarak çalıştırır (Development için)

        /// </summary>

        /// <param name="clearExisting">Mevcut verileri temizle</param>

        [HttpPost("run")]

        public async Task<IActionResult> RunSeedData([FromQuery] bool clearExisting = false)

        {

            try

            {

                _logger.LogInformation("Manual seed data process started (ClearExisting: {ClearExisting})...", clearExisting);



                if (clearExisting)

                {

                    await ClearAllTablesAsync();

                }



                await DevelopmentDemoDataSeedRunner.SeedCoreBusinessTablesAsync(_context, _logger);

                await DevelopmentDemoDataSeedRunner.EnsureAnchorCustomerAsync(_context, _logger);

                await ExtendedDemoDataSeeder.SeedAsync(_context, _logger);



                _logger.LogInformation("Manual seed data process completed successfully.");



                return Ok(new { message = "Seed data completed successfully" });

            }

            catch (Exception ex)

            {

                _logger.LogError(ex, "Error in manual seed data: {Message}", ex.Message);

                return StatusCode(500, new { error = ex.Message, stackTrace = ex.StackTrace });

            }

        }



        private async Task ClearAllTablesAsync()

        {

            _logger.LogInformation("Clearing all tables...");



            await _context.Database.ExecuteSqlRawAsync("""DELETE FROM "PartStocks" """);

            await _context.Database.ExecuteSqlRawAsync("""DELETE FROM "Parts" """);

            await _context.Database.ExecuteSqlRawAsync("""DELETE FROM "Incomes" """);

            await _context.Database.ExecuteSqlRawAsync("""DELETE FROM "Expenses" """);

            await _context.Database.ExecuteSqlRawAsync("""DELETE FROM "WorkOrderTimeline" """);

            await _context.Database.ExecuteSqlRawAsync("""DELETE FROM "WorkOrderPhotos" """);

            await _context.Database.ExecuteSqlRawAsync("""DELETE FROM "WorkOrderItems" """);

            await _context.Database.ExecuteSqlRawAsync("""DELETE FROM "WorkOrderLabors" """);

            await _context.Database.ExecuteSqlRawAsync("""DELETE FROM "WorkOrders" """);

            await _context.Database.ExecuteSqlRawAsync("""DELETE FROM "Vehicles" """);

            await _context.Database.ExecuteSqlRawAsync("""DELETE FROM "Customers" """);



            _logger.LogInformation("All tables cleared.");

        }

    }

}


