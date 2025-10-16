using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AccesoDatos.Models;
using AccesoDatos.Operaciones;
using System;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly ProductosDAO _dao;

        public ProductoController()
        {
            _dao = new ProductosDAO(); // Usa el DAO
        }

        // GET api/getProductos
        [HttpGet("getProductos")]
        public IActionResult getProductos()
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
        [HttpGet("getProductoId")]
        public IActionResult getProductoId(int id)
        {
            try
            {
                var p = _dao.seleccionarId(id);
                if (p == null)
                    return NotFound($"No se encontró ningún producto con el ID: {id}.");
                return Ok(p);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST api/insertarProducto
        [HttpPost("insertarProducto")]
        public IActionResult insertarProducto([FromBody] Productos producto)
        {
            try
            {
                if (producto == null)
                    return BadRequest("El producto no es válido.");

                _dao.insertar(
                    producto.descripcion,
                    producto.stock,
                    producto.precioventa,
                    producto.idcategoria,
                    producto.fechaingreso,
                    producto.fechacaducidad
                );

                return Ok("Producto insertado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error al insertar el producto: {ex.Message}");
            }
        }

        // PUT api/actualizarProducto
        [HttpPut("actualizarProducto")]
        public IActionResult actualizarProducto([FromBody] Productos producto)
        {
            try
            {
                if (producto == null || producto.idproducto <= 0)
                    return BadRequest("El producto no es válido o su ID es incorrecto.");

                _dao.actualizar(
                    producto.idproducto,
                    producto.descripcion,
                    producto.stock,
                    producto.precioventa,
                    producto.idcategoria,
                    producto.fechaingreso,
                    producto.fechacaducidad
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
        [HttpDelete("eliminarProducto")]
        public IActionResult eliminarProducto(int id)
        {
            try
            {
                _dao.eliminar(id);
                return Ok($"Producto con ID {id} eliminado exitosamente.");
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
