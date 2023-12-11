using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConesaApp.Database.Data.Entities;
using ConesaApp.Database.Data;
using ConesaApp.Server.Data.DTO;

namespace ConesaApp.Server.Controllers
{
    [Route("api/Usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DataBaseContext _dbContext;
        public UsuarioController(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost]
        public async Task<ActionResult<int>> PostUsuario(Usuario_SignInDTO usuario)
        {
            try
            {
                Usuario add = new Usuario();

                add.Nombre = usuario.Nombre;
                add.Apellido = usuario.Apellido;
                add.Mail = usuario.Mail;
                add.Contraseña = usuario.Contraseña;

                _dbContext.Usuarios.Add(add);
                await _dbContext.SaveChangesAsync();
                return Ok(add);
            }
            catch (Exception err)
            {

                return BadRequest(err.Message);
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<int>> LoginRequest(Usuario_LoginDTO usuario)
        {
            try
            {
                Usuario? temp = await _dbContext.Usuarios.Where(user => user.Mail == usuario.Mail).FirstOrDefaultAsync();

                if (temp == null)
                {
                    return Unauthorized("Usuario o contraseña incorrectos");
                }

                if (temp.Contraseña == usuario.Contraseña)
                {
                    Usuario response = new Usuario();
                    response.UsuarioID = temp.UsuarioID;
                    response.Nombre = temp.Nombre;
                    response.Apellido = temp.Apellido;
                    response.Mail = temp.Mail;
                    response.Contraseña = "";

                    return Ok(response);
                }
                else
                {

                    return Unauthorized("Usuario o contraseña incorrectos");
                }
            }
            catch (Exception err)
            {

                return BadRequest(err.Message);
            }

        }

        //[HttpGet("/Usuarios")]
        //public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        //{
        //    var usuarios = await _dbContext.Usuarios
        //                        .ToListAsync();

        //    if (usuarios == null)
        //    {
        //        return NotFound($"No hay usuarios para mostrar");

        //    }

        //    return usuarios;
        //}
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Pagos>>> GetPagos()
        //{
        //    if (_dbContext.Pagos == null)
        //    {
        //        return NotFound();
        //    }
        //    return await _dbContext.Pagos.ToListAsync();
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Pagos>> GetPagoId(int id)
        //{
        //    if (_dbContext.Pagos == null)
        //    {
        //        return NotFound();
        //    }
        //    var pago = await _dbContext.Pagos.Where(x => x.pagoID == id).FirstOrDefaultAsync();
        //    if (pago == null)
        //    {
        //        return NotFound($"No existe un pago de ID= {id}");
        //    }

        //    return Ok(pago);
        //}

        //[HttpPost]
        //public async Task<ActionResult<int>> PostPago(Pagos pago)
        //{
        //    try
        //    {
        //        _dbContext.Pagos.Add(pago);
        //        await _dbContext.SaveChangesAsync();
        //        //Aca nos devuelve el cliente recién creado
        //        return pago.pagoID;
        //    }
        //    catch (Exception err)
        //    {

        //        return BadRequest(err.Message);
        //    }

        //}

        //[HttpPut("{id}")]
        //public async Task<ActionResult<Pagos>> PutPago(int id, Pagos pago)
        //{
        //    if (id != pago.clienteID)
        //    {
        //        return BadRequest();
        //    }
        //    _dbContext.Pagos.Entry(pago).State = EntityState.Modified;
        //    try
        //    {
        //        await _dbContext.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {

        //        throw;
        //    }
        //    return Ok($"Se ha modificado el pago de ID {pago.pagoID}");
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult<int>> DeletePago(int id)
        //{
        //    var pago = _dbContext.Pagos.Where(x => x.pagoID == id).FirstOrDefault();
        //    if (pago == null)
        //    {
        //        return NotFound($"No se encontró el pago de Id {id}");
        //    }
        //    try
        //    {
        //        _dbContext.Pagos.Remove(pago);
        //        await _dbContext.SaveChangesAsync();
        //        return Ok($"El registro de  ID: {pago.pagoID} se ha eliminado correctamente");
        //    }
        //    catch (Exception e)
        //    {

        //        return BadRequest($"Los datos no pudieron ser eliminados por: {e.Message}");
        //    }
        //}
    }
}
