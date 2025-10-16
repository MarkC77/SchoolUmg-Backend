using Microsoft.AspNetCore.Mvc;
using AccesoDatos.Operaciones;
using AccesoDatos.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class AsignaturaController : ControllerBase
    {
        private readonly AsignaturaDAO asignaturaDAO = new AsignaturaDAO();

        // GET api/getAsignaturas
        [HttpGet("getAsignaturas")]
        public IActionResult getAsignaturas()
        {
            try
            {
                var result = asignaturaDAO.seleccionarTodos();
                if (result == null || result.Count == 0)
                    return NotFound("No se encontraron asignaturas.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }
        // Obtener todas las asignaturas completas
        [HttpGet("getAllAsignaturas")]
        public IActionResult GetAllAsignaturas()
        {
            try
            {
                var lista = asignaturaDAO.getAsignaturas();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las asignaturas: {ex.Message}");
            }
        }

        // Obtener una asignatura por ID
        [HttpGet("getAsignaturaById/{id}")]
        public IActionResult GetAsignaturaById(int id)
        {
            try
            {
                var asignatura = asignaturaDAO.getAsignaturaById(id);
                if (asignatura == null)
                    return NotFound($"No se encontró la asignatura con ID {id}");
                return Ok(asignatura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener la asignatura: {ex.Message}");
            }
        }

        // Insertar una nueva asignatura
        [HttpPost("insertarAsignatura")]
        public IActionResult InsertarAsignatura([FromBody] Asignatura asignatura)
        {
            try
            {
                asignaturaDAO.insertarAsignatura(asignatura);
                return Ok("Asignatura insertada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar la asignatura: {ex.Message}");
            }
        }

        // Actualizar una asignatura existente
        [HttpPut("actualizarAsignatura")]
        public IActionResult ActualizarAsignatura([FromQuery] int id, [FromBody] Asignatura asignatura)
        {
            try
            {
                var existente = asignaturaDAO.getAsignaturaById(id);
                if (existente == null)
                    return NotFound($"No se encontró la asignatura con ID {id}");

                asignatura.Id = id;
                asignaturaDAO.actualizarAsignatura(asignatura);
                return Ok("Asignatura actualizada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar la asignatura: {ex.Message}");
            }
        }

        // Eliminar una asignatura
        [HttpDelete("eliminarAsignatura")]
        public IActionResult EliminarAsignatura([FromQuery] int id)
        {
            try
            {
                var existente = asignaturaDAO.getAsignaturaById(id);
                if (existente == null)
                    return NotFound($"No se encontró la asignatura con ID {id}");

                asignaturaDAO.eliminarAsignatura(id);
                return Ok("Asignatura eliminada correctamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la asignatura: {ex.Message}");
            }
        }

    }

}
