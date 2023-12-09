using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConesaApp.Database.Data.Entities;
using ConesaApp.Database.Data;


namespace ConesaApp.Server.Controllers
{
    [Route("api/Polizas")]
    [ApiController]
    public class PolizaController : ControllerBase
    {
        private readonly DataBaseContext _dbContext;
        public PolizaController(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/Polizas")]
        public async Task<ActionResult<List<Poliza>>> GetPolizas()
        {
            //var polizas = await _dbContext.Polizas
            //                    .ToListAsync();

            //if (polizas == null)
            //{
            //    return NotFound($"No hay polizas para mostrar");

            //}

            //return polizas;
            return await _dbContext.Polizas.Include(x => x.Vehiculo).Include(x=>x.Empresa).Include(x=>x.Cobertura).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Poliza>> GetPolizaId(int id)
        {
            if (_dbContext.Polizas == null)
            {
                return NotFound();
            }
            var poliza = await _dbContext.Polizas.Where(x => x.PolizaID == id).FirstOrDefaultAsync();
            if (poliza == null)
            {
                return NotFound($"No existe una poliza de ID= {id}");
            }

            return Ok(poliza);
        }

        [HttpPost("/poliza/ID")]
        public async Task<ActionResult<int>> PostPoliza(Poliza poliza)
        {
            try
            {
                _dbContext.Polizas.Add(poliza);
                await _dbContext.SaveChangesAsync();
                //Aca nos devuelve el cliente recién creado
                return poliza.PolizaID;
            }
            catch (Exception err)
            {

                return BadRequest(err.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Poliza>> PutPoliza(int id, Poliza poliza)
        {
            var polizaSolicitada = _dbContext.Polizas
               .Where(e => e.PolizaID == id).FirstOrDefault();

            if (polizaSolicitada == null)
            {
                return NotFound("No se encontró la poliza a modificar");
            }

            polizaSolicitada.CoberturaID = poliza.CoberturaID;
            polizaSolicitada.NroPoliza = poliza.NroPoliza;
            polizaSolicitada.ValorCuota = poliza.ValorCuota;
            polizaSolicitada.EmpresaID = poliza.EmpresaID;
            polizaSolicitada.ValorAsegurado = poliza.ValorAsegurado;
            polizaSolicitada.Actualizado = poliza.Actualizado;
            polizaSolicitada.FinVigencia = poliza.FinVigencia;
            polizaSolicitada.InicioVigencia = poliza.InicioVigencia;
            polizaSolicitada.Pagos = poliza.Pagos;


            try
            {
                _dbContext.Polizas.Update(polizaSolicitada);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Los datos no se han podido actualizar");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeletePoliza(int id)
        {
            var poliza = _dbContext.Polizas.Where(x => x.PolizaID == id).FirstOrDefault();
            if (poliza == null)
            {
                return NotFound($"No se encontró la póliza de Id {id}");
            }
            try
            {
                _dbContext.Polizas.Remove(poliza);
                await _dbContext.SaveChangesAsync();
                return Ok($"El registro de número {poliza.NroPoliza} se ha eliminado correctamente");
            }
            catch (Exception e)
            {

                return BadRequest($"Los datos no pudieron ser eliminados por: {e.Message}");
            }
        }
    }
}
