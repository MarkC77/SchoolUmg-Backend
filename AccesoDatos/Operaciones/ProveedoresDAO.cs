using AccesoDatos.Context;
using AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Operaciones
{
    public class ProveedoresDAO
    {
        // Instancia del contexto de la base de datos (Entity Framework).
        public ProyectoContext contexto = new ProyectoContext();


        //Selecciona todos los alumnos de la base de datos.
        public List<Proveedores> seleccionarTodos() => contexto.Proveedores.ToList();

       public Proveedores seleccionarPorNit(int nit) => contexto.Proveedores.Where(a => a.nit == nit).FirstOrDefault();
        // Busca un alumno por su ID.
       public Proveedores seleccionarId(int id) => contexto.Proveedores.Where(a => a.idproveedor == id).FirstOrDefault();


        // Inserta un nuevo alumno en la base de datos.
        public void insertarProveedor(string descripcion, string direccion, int nit, string estado)
        {
            // Valida que el alumno no exista antes de la inserción para evitar duplicados.
            if (seleccionarPorNit(nit) != null)
            {
                throw new InvalidOperationException("El proveedor ya existe.");
            }

            // Crea un nuevo objeto Alumno con los datos proporcionados.
            Proveedores proveedores = new Proveedores
            {
                descripcion = descripcion,
                direccion = direccion,
                nit = nit,
                estado = estado,
            };

            // Agrega el nuevo objeto al contexto y guarda los cambios en la base de datos.
            contexto.Proveedores.Add(proveedores);
            contexto.SaveChanges();
        }

        // Actualiza los datos de un alumno.
        public void actualizarProveedor(int idproveedor, string descripcion, string direccion, int nit, string estado)
        {
            // Busca el alumno por su ID.
            var proveedores = seleccionarId(idproveedor);
            if (proveedores == null)
            {
                // Lanza una excepción si no se encuentra el alumno.
                throw new KeyNotFoundException("No se encontró el alumno con el ID proporcionado.");
            }

            // Actualiza las propiedades del objeto encontrado.
            proveedores.idproveedor = idproveedor;
            proveedores.descripcion= descripcion;
            proveedores.nit = nit;
            proveedores.direccion = direccion;
            proveedores.estado = estado;

            // Guarda los cambios en la base de datos.
            contexto.SaveChanges();
        }


        // Elimina un alumno y todos sus datos relacionados (matrículas y calificaciones) en una transacción.
        public void eliminarProveedor(int idproveedor)
        {
            // Inicia una transacción para asegurar que todas las eliminaciones se hagan o se reviertan.
            using (var transaction = contexto.Database.BeginTransaction())
            {
                try
                {
                    // Busca el alumno.
                    var proveedor = contexto.Proveedores.FirstOrDefault(a => a.idproveedor == idproveedor);
                    if (proveedor == null)
                    {
                        throw new KeyNotFoundException("No se encontró el alumno para eliminar.");
                    }

                    // Elimina el alumno.
                    contexto.Proveedores.Remove(proveedor);

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
