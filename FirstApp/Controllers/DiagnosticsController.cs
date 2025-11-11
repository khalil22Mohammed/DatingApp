using FirstApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstApp.Controllers
{
    public class DiagnosticsController(AppDbContext context, IConfiguration configuration) : BaesAPIController
    {
        [AllowAnonymous]
        [HttpGet("db")]
        public async Task<IActionResult> CheckDatabase()
        {
            try
            {
                var canConnect = await context.Database.CanConnectAsync();
                var provider = context.Database.ProviderName;
                var connStr = configuration.GetConnectionString("DefaultConnection") ?? "<null>";
                return Ok(new { canConnect, provider, connectionString = connStr });
            }
            catch (Exception ex)
            {
                return Problem(title: "Database connection failed", detail: ex.Message);
            }
        }
    }
}


