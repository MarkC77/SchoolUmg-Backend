using AccesoDatos.Context;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Operaciones
{
    public class ClientesDAO
    {
        // Instancia del contexto de la base de datos (Entity Framework).
        public ProyectoContext contexto = new ProyectoContext();


        //Selecciona todos los alumnos de la base de datos.
        public List<Clientes> seleccionarTodos() => contexto.Clientes.ToList();

        public Clientes seleccionarPorEmail(string email) => contexto.Clientes.Where(a => a.email == email).FirstOrDefault();
        // Busca un alumno por su ID.
        public Clientes seleccionarId(int id) => contexto.Clientes.Where(a => a.idclientes == id).FirstOrDefault();


        // Inserta un nuevo alumno en la base de datos.
        public void insertar(string direccion, int telefono, string email)
        {
            // Valida que el alumno no exista antes de la inserción para evitar duplicados.
            if (seleccionarPorEmail(email) != null)
            {
                throw new InvalidOperationException("El alumno con ese DNI ya existe.");
            }

            // Crea un nuevo objeto Alumno con los datos proporcionados.
            Clientes alumno = new Clientes
            {
                direccion = direccion,
                telefono = telefono,
                email = email
            };

            // Agrega el nuevo objeto al contexto y guarda los cambios en la base de datos.
            contexto.Clientes.Add(alumno);
            contexto.SaveChanges();
        }

        // Actualiza los datos de un alumno.
        public void actualizar(int idclientes, string direccion, int telefono, string email)
        {
            // Busca el alumno por su ID.
            var alumno = seleccionarId(idclientes);
            if (alumno == null)
            {
                // Lanza una excepción si no se encuentra el alumno.
                throw new KeyNotFoundException("No se encontró el alumno con el ID proporcionado.");
            }

            // Actualiza las propiedades del objeto encontrado.
            alumno.idclientes = idclientes;
            alumno.direccion = direccion;
            alumno.telefono = telefono;
            alumno.email = email;

            // Guarda los cambios en la base de datos.
            contexto.SaveChanges();
        }

        
        // Elimina un alumno y todos sus datos relacionados (matrículas y calificaciones) en una transacción.
        public void eliminarAlumno(int idcliente)
        {
            // Inicia una transacción para asegurar que todas las eliminaciones se hagan o se reviertan.
            using (var transaction = contexto.Database.BeginTransaction())
            {
                try
                {
                    // Busca el alumno.
                    var alumno = contexto.Clientes.FirstOrDefault(a => a.idclientes == idcliente);
                    if (alumno == null)
                    {
                        throw new KeyNotFoundException("No se encontró el alumno para eliminar.");
                    }

                    // Elimina el alumno.
                    contexto.Clientes.Remove(alumno);

                    // Guarda los cambios y confirma la transacción.
                    contexto.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // En caso de error, revierte la transacción para no dejar datos inconsistentes.
                    transaction.Rollback();
                    throw new InvalidOperationException("Error al eliminar el alumno y sus datos relacionados.", ex);
                }
            }
        }


        }
    }