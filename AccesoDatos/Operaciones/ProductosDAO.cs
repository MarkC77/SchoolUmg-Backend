using AccesoDatos.Context;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccesoDatos.Operaciones
{
    public class ProductosDAO
    {
        // Instancia del contexto de la base de datos.
        public ProyectoContext contexto = new ProyectoContext();

        // Selecciona todos los productos
        public List<Productos> seleccionarTodos() => contexto.Productos.ToList();

        // Selecciona un producto por su ID
        public Productos seleccionarId(int id) => contexto.Productos.FirstOrDefault(p => p.idproducto == id);

        // Selecciona productos por descripción
        public List<Productos> seleccionarPorDescripcion(string descripcion) =>
            contexto.Productos.Where(p => p.descripcion.Contains(descripcion)).ToList();

        // Inserta un nuevo producto
        public void insertar(string descripcion, int stock, decimal? precioventa,
                             int? idcategoria, DateTime? fechaingreso, DateTime? fechacaducidad)
        {
            Productos producto = new Productos
            {
                descripcion = descripcion,
                stock = stock,
                precioventa = precioventa,
                idcategoria = idcategoria,
                fechaingreso = fechaingreso,
                fechacaducidad = fechacaducidad
            };

            contexto.Productos.Add(producto);
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

            contexto.Productos.Remove(producto);
            contexto.SaveChanges();
        }
    }
}
