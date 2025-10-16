using AccesoDatos.Context;
using AccesoDatos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Operaciones
{
    public class FacturasDAO
    {
        // Instancia del contexto de la base de datos.
        public ProyectoContext contexto = new ProyectoContext();

        // Selecciona todos los productos
        public List<Facturas> seleccionarTodos() => contexto.Facturas.ToList();

        // Selecciona un producto por su ID
        public Facturas seleccionarId(int id) => contexto.Facturas.FirstOrDefault(p => p.idfacturas == id);


        // Inserta un nuevo producto
        public void insertar(string descripcion, int stock, decimal? precioventa,
                             int? idcategoria, DateTime? fechaingreso, DateTime? fechacaducidad)
        {
            Facturas producto = new Facturas
            {
                descripcion = descripcion,
                stock = stock,
                precioventa = precioventa,
                idcategoria = idcategoria,
                fechaingreso = fechaingreso,
                fechacaducidad = fechacaducidad
            };

            contexto.Facturas.Add(producto);
            contexto.SaveChanges();
        }

        // Actualiza un producto existente
        public void actualizar(int id, string descripcion, int stock, decimal? precioventa,
                               int? idcategoria, DateTime? fechaingreso, DateTime? fechacaducidad)
        {
            var producto = seleccionarId(id);
            if (producto == null)
                throw new KeyNotFoundException($"No se encontró un producto con ID {id}.");

            producto.descripcion = descripcion;
            producto.stock = stock;
            producto.precioventa = precioventa;
            producto.idcategoria = idcategoria;
            producto.fechaingreso = fechaingreso;
            producto.fechacaducidad = fechacaducidad;

            contexto.SaveChanges();
        }

        // Elimina un producto
        public void eliminar(int id)
        {
            var producto = seleccionarId(id);
            if (producto == null)
                throw new KeyNotFoundException($"No se encontró un producto con ID {id}.");

            contexto.Facturas.Remove(producto);
            contexto.SaveChanges();
        }
    }
}
