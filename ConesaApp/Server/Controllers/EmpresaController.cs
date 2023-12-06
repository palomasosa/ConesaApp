using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ConesaApp.Database.Data.Entities;
using ConesaApp.Database.Data;

namespace ConesaApp.Server.Controllers
{
    [Route("api/Empresa")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        private readonly DataBaseContext _dbContext;
        public EmpresaController(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/Empresas")]
        public async Task<ActionResult<List<Empresa>>> GetEmpresas()
        {
            var empresas = await _dbContext.Empresas
                                .ToListAsync();

            if (empresas == null)
            {
                return NotFound($"No hay empresas para mostrar");

            }

            return empresas;
        }
    }
}
