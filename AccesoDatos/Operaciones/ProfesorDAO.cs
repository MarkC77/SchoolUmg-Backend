using AccesoDatos.Context;
using AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Operaciones
{
    public class ProfesorDAO
    {
        public ProyectoContext contexto = new ProyectoContext();

        public Profesor login(string usuario, string pass)
        {
            var profe = contexto.Profesors.Where(p => p.Usuario == usuario && p.Pass == pass).FirstOrDefault();
            return profe;
        }


        public List<Profesor> getProfesores()
        {
            return contexto.Profesors.ToList();
        }

        public Profesor getProfesor(string usuario)
        {
            return contexto.Profesors.FirstOrDefault(p => p.Usuario == usuario);
        }

        public void insertarProfesor(Profesor profesor)
        {
            contexto.Profesors.Add(profesor);
            contexto.SaveChanges();
        }

        public void actualizarProfesor(Profesor profesor)
        {
            var profeExistente = contexto.Profesors.FirstOrDefault(p => p.Usuario == profesor.Usuario);
            if (profeExistente != null)
            {
                profeExistente.Nombre = profesor.Nombre;
                profeExistente.Pass = profesor.Pass;
                profeExistente.Email = profesor.Email;
                contexto.SaveChanges();
            }
        }

        public void eliminarProfesor(string usuario)
        {
            var profesor = contexto.Profesors.FirstOrDefault(p => p.Usuario == usuario);
            if (profesor != null)
            {
                contexto.Profesors.Remove(profesor);
                contexto.SaveChanges();
            }
        }
    }
}