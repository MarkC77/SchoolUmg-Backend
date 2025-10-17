using AccesoDatos.Context;
using AccesoDatos.Models;
using System.Collections.Generic;
using System.Linq;

namespace AccesoDatos.Operaciones
{
    public class AsignaturaDAO
    {
        private readonly ProyectoContext contexto = new ProyectoContext();

        // Devuelve todas las asignaturas con solo id y nombre
        public List<Asignatura> seleccionarTodos()
        {
            return contexto.Asignaturas
                .Select(a => new Asignatura
                {
                    Id = a.Id,
                    Nombre = a.Nombre
                })
                .ToList();
        }

        public List<Asignatura> getAsignaturas()
        {
            return contexto.Asignaturas.ToList();
        }

        public Asignatura getAsignaturaById(int id)
        {
            return contexto.Asignaturas.FirstOrDefault(a => a.Id == id);
        }

        //Insertar una nueva asignatura
        public void insertarAsignatura(Asignatura asignatura)
        {
            contexto.Asignaturas.Add(asignatura);
            contexto.SaveChanges();
        }

        //Actualizar una nueva asignatura
        public void actualizarAsignatura(Asignatura asignatura)
        {
            var asignaturaExistente = contexto.Asignaturas.FirstOrDefault(a => a.Id == asignatura.Id);
            if (asignaturaExistente != null)
            {
                asignaturaExistente.Nombre = asignatura.Nombre;
                asignaturaExistente.Creditos = asignatura.Creditos;
                asignaturaExistente.Profesor = asignatura.Profesor;
            }
        }

        //Eliminar
        public void eliminarAsignatura(int id)
        {
            var asignatura = contexto.Asignaturas.FirstOrDefault(a => a.Id == id);
            if (asignatura != null)
            {
                contexto.Asignaturas.Remove(asignatura);
                contexto.SaveChanges();
            }
        }
    }
}
