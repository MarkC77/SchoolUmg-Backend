using AccesoDatos.Models;
using AccesoDatos.Operaciones;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
            private readonly ClientesDAO _dao;

            public ClientesController()
            {
                _dao = new ClientesDAO(); // Usa el DAO
            }

            // GET api/getProductos
            [HttpGet("getClientes")]
            public IActionResult getClientes()
            {
                try
                {
                    var result = _dao.seleccionarTodos();
                    if (result == null || result.Count == 0)
                        return NotFound("No se encontraron productos.");
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error interno del servidor: {ex.Message}");
                }
            }

            // GET api/getProductoId?id=123
            [HttpGet("getClientesId")]
            public IActionResult getClientesId(int idclientes)
            {
                try
                {
                    var p = _dao.seleccionarId(idclientes);
                    if (p == null)
                        return NotFound($"No se encontró ningún producto con el ID: {idclientes}.");
                    return Ok(p);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error interno del servidor: {ex.Message}");
                }
            }

            // POST api/insertarProducto
            [HttpPost("insertarClientes")]
            public IActionResult insertarClientes([FromBody] Clientes producto)
            {
                try
                {
                    if (producto == null)
                        return BadRequest("El producto no es válido.");

                    _dao.insertar(
                        producto.direccion,
                        producto.telefono,
                        producto.email
                    );

                    return Ok("Producto insertado exitosamente.");
                }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error al insertar el cliente: {ex.InnerException?.Message ?? ex.Message}");
            }

        }

        // PUT api/actualizarProducto
        [HttpPut("actualizarClientes")]
            public IActionResult actualizarProducto([FromBody] Clientes producto)
            {
                try
                {
                    if (producto == null || producto.idclientes <= 0)
                        return BadRequest("El producto no es válido o su ID es incorrecto.");

                    _dao.actualizar(
                        producto.idclientes,
                        producto.direccion,
                        producto.telefono,
                        producto.email
                    );

                    return Ok("Producto actualizado exitosamente.");
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error al actualizar el producto: {ex.Message}");
                }
            }

            // DELETE api/eliminarProducto?id=123
            [HttpDelete("eliminarClientes/{idcliente}")]
            public IActionResult eliminarCliente(int idcliente)
            {
                try
                {
                    _dao.eliminarAlumno(idcliente);
                    return Ok($"Producto con ID {idcliente} eliminado exitosamente.");
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        $"Error al eliminar el producto: {ex.Message}");
                }
            }
        }
    }