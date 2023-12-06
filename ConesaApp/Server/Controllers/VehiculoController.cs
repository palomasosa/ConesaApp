using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ConesaApp.Database.Data.Entities;
using ConesaApp.Database.Data;


namespace ConesaApp.Server.Controllers
{
    [Route("api/Vehiculos")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly DataBaseContext _dbContext;
        public VehiculoController(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/Vehiculos")]
        public async Task<ActionResult<List<Vehiculo>>> GetVehiculos()
        {
             return await _dbContext.Vehiculos.Include(x => x.Poliza).Include(x=>x.Poliza.Cobertura).Include(y => y.Cliente).ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Vehiculo>> GetVehiculoId(int id)
        {
            if (_dbContext.Vehiculos == null)
            {
                return NotFound();
            }
            var vehiculo = await _dbContext.Vehiculos.Where(x => x.VehiculoID == id).FirstOrDefaultAsync();
            if (vehiculo == null)
            {
                return NotFound($"No existe un vehiculo de ID= {id}");
            }

            return Ok(vehiculo);
        }

        [HttpGet("/VehiculoPoliza/{id:int}")]
        public async Task<ActionResult<Vehiculo>> GetVehiculoIdPoliza(int id)
        {
            if (_dbContext.Vehiculos == null)
            {
                return NotFound();
            }
            var vehiculo = await _dbContext.Vehiculos.Where(x => x.PolizaID == id).FirstOrDefaultAsync();
            if (vehiculo == null)
            {
                return NotFound($"No existe un vehiculo de ID= {id}");
            }

            return Ok(vehiculo);    
        }

        [HttpGet("/Vehiculos/Actualizados")]
        public async Task<ActionResult<List<Vehiculo>>> GetVehiculosActualizados()
        {
            return await _dbContext.Vehiculos.Include(x => x.Poliza).Include(x => x.Poliza.Cobertura).Include(y => y.Cliente).Where(x=>x.Poliza.Actualizado == true).ToListAsync();
        }
        #region
        [HttpGet("/Vehiculos/Busqueda")]
        public async Task<ActionResult<List<Vehiculo>>> GetVehiculosBusqueda(string query)
        {
            bool IsInt(string s)
            {
                int temp;
                return int.TryParse(s, out temp);
            }

            var vehiculos = _dbContext.Vehiculos.Include(v => v.Cliente).Include(v => v.Poliza).Include(v=>v.Poliza.Cobertura).ToList();

            if (IsInt(query))
            {
                // Si el string se puede parsear a int, obtenga los vehículos que su año contenga este número.
                vehiculos = vehiculos.Where(v => v.Año.HasValue && v.Año.Value.ToString().Contains(query) || v.Poliza.NroPoliza.ToString().Contains(query)).ToList();
            }
            else
            {
                query = query.ToLower();
                // Si no se pudo parsear a int, obtenga todos los vehículos que tengan una Patente, una Marca, un Poliza.TipoSeguro, un Cliente.Nombre o un Cliente.Telefono
                vehiculos = vehiculos.Where(v => v.Patente.ToLower().Contains(query) || v.Marca.ToLower().Contains(query) || v.Poliza.Cobertura.Tipo.ToLower().Contains(query) || v.Cliente.Nombre.ToLower().Contains(query) || v.Cliente.Apellido.ToLower().Contains(query) || v.Cliente.Telefono.ToLower().Contains(query)).ToList();
            }

            return vehiculos;
        }

        [HttpGet("/Vehiculos/Busqueda/Actualizados")]
        public async Task<ActionResult<List<Vehiculo>>> GetVehiculosBusquedaActualizados(string query)
        {
            bool IsInt(string s)
            {
                int temp;
                return int.TryParse(s, out temp);
            }

            var vehiculos = _dbContext.Vehiculos.Include(v => v.Cliente).Include(v => v.Poliza).Include(v => v.Poliza.Cobertura).ToList();

            if (IsInt(query))
            {
                // Si el string se puede parsear a int, obtenga los vehículos que su año contenga este número.
                vehiculos = vehiculos.Where(v => v.Año.HasValue && v.Año.Value.ToString().Contains(query)).ToList();
            }
            else
            {
                query = query.ToLower();
                // Si no se pudo parsear a int, obtenga todos los vehículos que tengan una Patente, una Marca, un Poliza.TipoSeguro, un Cliente.Nombre o un Cliente.Telefono
                vehiculos = vehiculos.Where(v => v.Patente.ToLower().Contains(query) || v.Marca.ToLower().Contains(query) || v.Poliza.Cobertura.Tipo.ToLower().Contains(query) || v.Cliente.Nombre.ToLower().Contains(query) || v.Cliente.Apellido.ToLower().Contains(query) || v.Cliente.Telefono.ToLower().Contains(query)).ToList();
            }

            vehiculos = vehiculos.Where(x => x.Poliza.Actualizado == true).ToList();

            return vehiculos;
        }

        [HttpGet("/Vehiculos/Vencimiento")]
        public async Task<ActionResult<List<Vehiculo>>> GetVehiculosPorFechaVencimiento(string fechaInicio, string fechaFin)
        {
            bool IsDateTime(string s)
            {
                DateTime temp;
                return DateTime.TryParse(s, out temp);
            }

            if (!IsDateTime(fechaInicio) || !IsDateTime(fechaFin))
            {
                return BadRequest("Las fechas deben estar en formato válido.");
            }

            DateTime fechaInicioDateTime = DateTime.Parse(fechaInicio);
            DateTime fechaFinDateTime = DateTime.Parse(fechaFin);

            var vehiculos = _dbContext.Vehiculos
                                    .Include(v => v.Poliza)
                                    .Include(v=>v.Cliente)
                                    .Include(v=>v.Poliza.Cobertura)
                                    .Where(v => v.Poliza.FinVigencia >= fechaInicioDateTime &&
                                                v.Poliza.FinVigencia <= fechaFinDateTime)
                                    .ToList();

            return vehiculos;
        }


        #endregion

        [HttpPost]
        public async Task<ActionResult<int>> PostVehiculo(Vehiculo vehiculo)
        {
            try
            {
                _dbContext.Vehiculos.Add(vehiculo);
                await _dbContext.SaveChangesAsync();
                //Aca nos devuelve el cliente recién creado
                return vehiculo.VehiculoID;
            }
            catch (Exception err)
            {

                return BadRequest(err.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Vehiculo>> PutVehiculo(int id, Vehiculo vehiculo)
        {
            var vehiculoSolicitado = _dbContext.Vehiculos
               .Where(e => e.VehiculoID == id).FirstOrDefault();

            if (vehiculoSolicitado == null)
            {
                return NotFound("No se encontró el vehiculo a modificar");
            }

            vehiculoSolicitado.Patente = vehiculo.Patente;
            vehiculoSolicitado.Año = vehiculo.Año;
            vehiculoSolicitado.Marca = vehiculo.Marca;
            vehiculoSolicitado.Poliza = vehiculo.Poliza;
            vehiculoSolicitado.Cliente = vehiculo.Cliente;

            try
            {
                _dbContext.Vehiculos.Update(vehiculoSolicitado);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Los datos no se han podido actualizar");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteVehiculo(int id)
        {
            var vehiculo = _dbContext.Vehiculos.Where(x => x.VehiculoID == id).FirstOrDefault();
            if (vehiculo == null)
            {
                return NotFound($"No se encontró el vehiculo de Id {id}");
            }
            try
            {
                _dbContext.Vehiculos.Remove(vehiculo);
                await _dbContext.SaveChangesAsync();
                return Ok($"El registro de patente {vehiculo.Patente} se ha eliminado correctamente");
            }
            catch (Exception e)
            {

                return BadRequest($"Los datos no pudieron ser eliminados por: {e.Message}");
            }
        }
    }
}
