using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ConesaApp.Database.Data.Entities;
using ConesaApp.Database.Data;


namespace ConesaApp.Server.Controllers
{
    [Route("api/Pago")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly DataBaseContext _dbContext;
        public PagoController(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("/Pagos")]
        public async Task<ActionResult<List<Pago>>> GetPagos()
        {
            //if (_dbContext.Pagos == null)
            //{
            //    return NotFound();
            //}
            //return await _dbContext.Pagos.ToListAsync();
            return await _dbContext.Pagos.Include(x => x.Poliza).Include(p=>p.Poliza.Vehiculo).Include(y => y.Cliente).Include(z=>z.Usuario).Include(v=>v.MetodoPago).ToListAsync();
        }

        [HttpGet("/Pagos/Busqueda")]
        public async Task<ActionResult<List<Pago>>> GetPagosBusqueda(string query)
        {
            bool IsInt(string s)
            {
                int temp;
                return int.TryParse(s, out temp);
            }

            var pagos = _dbContext.Pagos.Include(x => x.Poliza).Include(p=>p.Poliza.Vehiculo).Include(y => y.Cliente).Include(z => z.Usuario).Include(p=>p.MetodoPago).ToList();

            if (IsInt(query))
            {
                pagos = pagos.Where(p => p.Monto.ToString().Contains(query) || p.Poliza.NroPoliza.ToString().Contains(query)).ToList();
            }
            else
            {
                query = query.ToLower();
                pagos = pagos.Where(p => p.Usuario.Nombre.ToLower().Contains(query) || p.Usuario.Apellido.ToLower().Contains(query) || p.Cliente.Nombre.ToLower().Contains(query) || p.Cliente.Apellido.ToLower().Contains(query) || p.Poliza.Vehiculo.Patente.ToLower().Contains(query)).ToList();
            }
            return pagos;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pago>> GetPagoId(int id)
        {
            if (_dbContext.Pagos == null)
            {
                return NotFound();
            }
            var pago = await _dbContext.Pagos.Where(x => x.PagoID == id).FirstOrDefaultAsync();
            if (pago == null)
            {
                return NotFound($"No existe un pago de ID= {id}");
            }

            return Ok(pago);
        }

        [HttpPost]
        public async Task<ActionResult<int>> PostPago(Pago pago)
        {
            try
            {
                _dbContext.Pagos.Add(pago);
                await _dbContext.SaveChangesAsync();
                //Aca nos devuelve el cliente recién creado
                return pago.PagoID;
            }
            catch (Exception err)
            {

                return BadRequest(err.Message);
            }

        }
//public ActionResult Put(int id, [FromBody] Huesped huesped)
        [HttpPut("{id}")]
        
        public async Task<ActionResult<Pago>> PutPago(int id, Pago pago)
        {
            //if (id != pago.clienteID)
            //{
            //    return BadRequest();
            //}
            //_dbContext.Pagos.Entry(pago).State = EntityState.Modified;
            //try
            //{
            //    await _dbContext.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{

            //    throw;
            //}
            //return Ok($"Se ha modificado el pago de ID {pago.pagoID}");
            var pagoSolicitado = _dbContext.Pagos
   .Where(e => e.PagoID == id).FirstOrDefault();

            if (pagoSolicitado == null)
            {
                return NotFound("No se encontró el pago a modificar");
            }

            pagoSolicitado.UsuarioID = pago.UsuarioID;
            pagoSolicitado.PolizaID = pago.PolizaID;
            pagoSolicitado.ClienteID = pago.ClienteID;
            pagoSolicitado.Fecha = pago.Fecha;
            pagoSolicitado.MetodoPagoID = pago.MetodoPagoID;
            pagoSolicitado.Monto = pago.Monto;
            pagoSolicitado.PolizaID = pago.PolizaID;

            try
            {
                _dbContext.Pagos.Update(pagoSolicitado);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Los datos no se han podido actualizar");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeletePago(int id)
        {
            var pago = _dbContext.Pagos.Where(x => x.PagoID == id).FirstOrDefault();
            if (pago == null)
            {
                return NotFound($"No se encontró el pago de Id {id}");
            }
            try
            {
                _dbContext.Pagos.Remove(pago);
                await _dbContext.SaveChangesAsync();
                return Ok($"El registro de  ID: {pago.PagoID} se ha eliminado correctamente");
            }
            catch (Exception e)
            {

                return BadRequest($"Los datos no pudieron ser eliminados por: {e.Message}");
            }
        }
    }
}
