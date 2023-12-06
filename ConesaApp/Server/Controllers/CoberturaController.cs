using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ConesaApp.Database.Data.Entities;
using ConesaApp.Database.Data;

namespace ConesaApp.Server.Controllers
{
    [Route("api/Cobertura")]
    [ApiController]
    public class CoberturaController : ControllerBase
    {
        private readonly DataBaseContext _dbContext;
        public CoberturaController(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/Coberturas")]
        public async Task<ActionResult<List<Cobertura>>> GetCoberturas()
        {
            var coberturas = await _dbContext.Coberturas
                                .ToListAsync();

            if (coberturas == null)
            {
                return NotFound($"No hay coberturas para mostrar");

            }

            return coberturas;
        }
    }
}
