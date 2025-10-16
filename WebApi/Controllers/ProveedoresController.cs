using AccesoDatos.Models;
using AccesoDatos.Operaciones;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly ProveedoresDAO dao = new ProveedoresDAO();

        // =========================
        // GET: api/Proveedores
        // =========================
        [HttpGet]
        public ActionResult<IEnumerable<Proveedores>> GetProveedores()
        {
            try
            {
                var lista = dao.seleccionarTodos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener proveedores: {ex.Message}");
            }
        }

        // =========================
        // GET: api/Proveedores/{id}
        // =========================
        [HttpGet("{id:int}")]
        public ActionResult<Proveedores> GetProveedorById(int id)
        {
            try
            {
                var proveedor = dao.seleccionarId(id);
                if (proveedor == null)
                    return NotFound($"No se encontró el proveedor con ID {id}");
                return Ok(proveedor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener proveedor: {ex.Message}");
            }
        }

        // =========================
        // GET: api/Proveedores/nit/{nit}
        // =========================
        [HttpGet("nit/{nit:int}")]
        public ActionResult<Proveedores> GetProveedorByNit(int nit)
        {
            try
            {
                var proveedor = dao.seleccionarPorNit(nit);
                if (proveedor == null)
                    return NotFound($"No se encontró el proveedor con NIT {nit}");
                return Ok(proveedor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener proveedor por NIT: {ex.Message}");
            }
        }

        // =========================
        // POST: api/Proveedores
        // =========================
        [HttpPost]
        public ActionResult CrearProveedor([FromBody] Proveedores proveedor)
        {
            try
            {
                dao.insertarProveedor(
                    proveedor.descripcion,
                    proveedor.direccion,
                    proveedor.nit,
                    proveedor.estado
                );
                return Ok("Proveedor agregado correctamente.");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar el proveedor: {ex.Message}");
            }
        }

        // =========================
        // PUT: api/Proveedores/{id}
        // =========================
        [HttpPut("{id:int}")]
        public ActionResult ActualizarProveedor(int id, [FromBody] Proveedores proveedor)
        {
            try
            {
                dao.actualizarProveedor(
                    id,
                    proveedor.descripcion,
                    proveedor.direccion,
                    proveedor.nit,
                    proveedor.estado
                );
                return Ok("Proveedor actualizado correctamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el proveedor: {ex.Message}");
            }
        }

        // =========================
        // DELETE: api/Proveedores/{id}
        // =========================
        [HttpDelete("{id:int}")]
        public ActionResult EliminarProveedor(int id)
        {
            try
            {
                dao.eliminarProveedor(id);
                return Ok("Proveedor eliminado correctamente.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el proveedor: {ex.Message}");
            }
        }
    }
}
