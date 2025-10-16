using Microsoft.AspNetCore.Mvc;
using AccesoDatos.Operaciones;
using AccesoDatos.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AccesoDatos.Models;
using System.Diagnostics.Eventing.Reader;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        public ProfesorDAO profesorDAO = new ProfesorDAO();

        [HttpPost("autenticacion")]
        public string login([FromBody] Profesor profe)
        {
            var profesor = profesorDAO.login(profe.Usuario, profe.Pass);

            if (profesor != null)
            {
                return profesor.Usuario;
            }
            else
            {
                return null;
            }
        }

        // Obtener todos los profesores
        [HttpGet("profesores")]
        public ActionResult<List<Profesor>> getProfesores()
        {
            return profesorDAO.getProfesores();
        }

        // Obtener un profesor por usuario
        [HttpGet("profesor/{usuario}")]
        public ActionResult<Profesor> getProfesorID(string usuario)
        {
            var profesor = profesorDAO.getProfesor(usuario);
            if (profesor == null) return NotFound();
            return profesor;
        }

        // Insertar un nuevo profesor
        [HttpPost("profesor")]
        public IActionResult insertarProfesor([FromBody] Profesor profesor)
        {
            profesorDAO.insertarProfesor(profesor);
            return Ok("Profesor insertado correctamente");
        }

        // Actualizar un profesor existente
        [HttpPut("profesor/{usuario}")]
        public IActionResult actualizarProfesor(string usuario, [FromBody] Profesor profesor)
        {
            var profeExistente = profesorDAO.getProfesor(usuario);
            if (profeExistente == null) return NotFound();

            profesor.Usuario = usuario; // aseguramos que el Usuario no cambie
            profesorDAO.actualizarProfesor(profesor);
            return Ok("Profesor actualizado correctamente");
        }

        // Eliminar un profesor por usuario
        [HttpDelete("profesor/{usuario}")]
        public IActionResult eliminarProfesor(string usuario)
        {
            var profeExistente = profesorDAO.getProfesor(usuario);
            if (profeExistente == null) return NotFound();

            profesorDAO.eliminarProfesor(usuario);
            return Ok("Profesor eliminado correctamente");
        }

    }
}