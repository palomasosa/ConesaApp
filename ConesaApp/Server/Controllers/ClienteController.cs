using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ConesaApp.Database.Data.Entities;
using ConesaApp.Database.Data;


namespace ConesaApp.Server.Controllers
{
    [Route("api/Cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly DataBaseContext _dbContext;
        public ClienteController(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/Clientes")]
        public async Task<ActionResult<List<Cliente>>> GetClientes()
        {
            var clientes = await _dbContext.Clientes
                                .ToListAsync();

            if (clientes == null)
            {
                return NotFound($"No hay clientes para mostrar");

            }

            return clientes;
        }

        [HttpGet("/Clientes/Filtro")]
        public async Task<ActionResult<Cliente>> GetClienteFiltro(string query)
        {
            if (_dbContext.Clientes == null)
            {
                return NotFound();
            }
            var clientes = _dbContext.Clientes.Where(x => x.Nombre.ToLower().Contains(query.ToLower()) || x.Apellido.ToLower().Contains(query.ToLower())).ToList();
            return Ok(clientes);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Cliente>> GetClienteId(int id)
        {
            if (_dbContext.Clientes == null)
            {
                return NotFound();
            }
            var cliente = await _dbContext.Clientes.Where(x => x.ClienteID == id).FirstOrDefaultAsync();
            if (cliente == null)
            {
                return NotFound($"No existe un cliente de ID= {id}");
            }

            return Ok(cliente);
        }

        [HttpGet("{clienteID}/Vehiculos")]
        public IActionResult ObtenerVehiculosDeCliente(int clienteId)
        {
            try
            {
                var vehiculos = _dbContext.Vehiculos
                    .Where(v => v.ClienteID == clienteId)
                    .ToList();

                return Ok(vehiculos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener vehículos: {ex.Message}");
            }
        }

        [HttpPost("/cliente/ID")]

        public async Task<ActionResult<int>> PostCliente(Cliente cliente)
        {
            try
            {
                _dbContext.Clientes.Add(cliente);
                await _dbContext.SaveChangesAsync();
                // Devuelve el ClienteID recién creado
                return Ok(Convert.ToInt32(cliente.ClienteID));
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Cliente>> PutCliente(int id, Cliente cliente)
        {
            //if (id != cliente.clienteID)
            //{
            //    return BadRequest();
            //}
            //_dbContext.Clientes.Entry(cliente).State = EntityState.Modified;
            //try
            //{
            //    await _dbContext.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{

            //    throw;
            //}
            //return Ok($"Se ha modificado el cliente {cliente.nombre} {cliente.apellido}");
            var clienteSolicitado = _dbContext.Clientes
               .Where(e => e.ClienteID == id).FirstOrDefault();

            if (clienteSolicitado == null)
            {
                return NotFound("No se encontró el cliente a modificar");
            }

            clienteSolicitado.Telefono = cliente.Telefono;
            clienteSolicitado.Mail = cliente.Mail;
            clienteSolicitado.Nombre = cliente.Nombre;
            clienteSolicitado.Apellido = cliente.Apellido;
            clienteSolicitado.Ciudad = cliente.Ciudad;
            clienteSolicitado.Direccion = cliente.Direccion;

            try
            {
                _dbContext.Clientes.Update(clienteSolicitado);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Los datos no se han podido actualizar");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteCliente(int id)
        {
            var cliente = _dbContext.Clientes.Where(x => x.ClienteID == id).FirstOrDefault();
            if (cliente == null)
            {
                return NotFound($"No se encontró el cliente de Id {id}");
            }
            try
            {
                _dbContext.Clientes.Remove(cliente);
                await _dbContext.SaveChangesAsync();
                return Ok($"El registro de {cliente.Nombre} {cliente.Apellido} se ha eliminado correctamente");
            }
            catch (Exception e)
            {

                return BadRequest($"Los datos no pudieron ser eliminados por: {e.Message}");
            }
        }
    }
}
