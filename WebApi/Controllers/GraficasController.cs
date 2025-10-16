using Microsoft.AspNetCore.Mvc;
using AccesoDatos.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class GraficasController : ControllerBase
    {
        private readonly ProyectoContext _ctx;
        public GraficasController(ProyectoContext ctx) { _ctx = ctx; }

        [HttpGet("getAlumnosPorAsignatura")]
        public IActionResult GetAlumnosPorAsignatura()
        {
            var result = _ctx.AlumnosPorAsignatura.AsNoTracking().ToList();
            return Ok(result);
        }

        [HttpGet("getDistribucionCalificaciones")]
        public IActionResult GetDistribucionCalificaciones()
        {
            var datos = _ctx.DistribucionCalificaciones.AsNoTracking().ToList();
            var distribucion = new
            {
                A = datos.Count(d => d.NotaFinal >= 90 && d.NotaFinal <= 100),
                B = datos.Count(d => d.NotaFinal >= 80 && d.NotaFinal < 90),
                C = datos.Count(d => d.NotaFinal >= 70 && d.NotaFinal < 80),
                D = datos.Count(d => d.NotaFinal >= 60 && d.NotaFinal < 70),
                F = datos.Count(d => d.NotaFinal < 60)
            };
            return Ok(distribucion);
        }
    }
}