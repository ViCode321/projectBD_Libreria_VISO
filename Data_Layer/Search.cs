using Model_Layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Data_Layer
{
    public class Search : connection
    {
        public List<Product> BuscarProducto(string searchTerm, string searchBy)
        {
            List<Product> productos = new List<Product>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                // Verificar si searchTerm no es nulo ni vacío antes de ejecutar la consulta
                if (!string.IsNullOrEmpty(searchTerm) && !string.IsNullOrEmpty(searchBy))
                {
                    // Utilizaremos parámetros para evitar la inyección de SQL
                    string query = "";

                    #region Query de SQL
                    if (searchBy == "name")
                    {
                        query = "SELECT Producto.Producto_Id AS Código, Producto.Descripcion, Proveedor.Nombre AS Proveedor, " +
                                "Categoria.Nombre AS Categoría, Marca.Nombre AS Marca, Producto.Cantidad, Producto.Costo, Producto.Precio " +
                                "FROM Producto INNER JOIN Proveedor ON Producto.Proveedor_Id = Proveedor.Proveedor_Id " +
                                "INNER JOIN Categoria ON Producto.Categoria_Id = Categoria.Categoria_Id " +
                                "INNER JOIN Marca ON Producto.Marca_Id = Marca.Marca_Id " +
                                "WHERE Producto.Descripcion LIKE @searchTerm " +
                                "ORDER BY Producto.Producto_Id";
                    }
                    else if (searchBy == "code")
                    {
                        query = "SELECT Producto.Producto_Id AS Código, Producto.Descripcion, Proveedor.Nombre AS Proveedor, " +
                                "Categoria.Nombre AS Categoría, Marca.Nombre AS Marca, Producto.Cantidad, Producto.Costo, Producto.Precio " +
                                "FROM Producto INNER JOIN Proveedor ON Producto.Proveedor_Id = Proveedor.Proveedor_Id " +
                                "INNER JOIN Categoria ON Producto.Categoria_Id = Categoria.Categoria_Id " +
                                "INNER JOIN Marca ON Producto.Marca_Id = Marca.Marca_Id " +
                                "WHERE Producto.Producto_Id LIKE @searchTerm " + 
                                "ORDER BY Producto.Producto_Id";
                    
                    }
                    else if (searchBy == "category")
                    {
                        query = "SELECT Producto.Producto_Id AS Código, Producto.Descripcion, Proveedor.Nombre AS Proveedor, " +
                                "Categoria.Nombre AS Categoría, Marca.Nombre AS Marca, Producto.Cantidad, Producto.Costo, Producto.Precio " +
                                "FROM Producto INNER JOIN Proveedor ON Producto.Proveedor_Id = Proveedor.Proveedor_Id " +
                                "INNER JOIN Categoria ON Producto.Categoria_Id = Categoria.Categoria_Id " +
                                "INNER JOIN Marca ON Producto.Marca_Id = Marca.Marca_Id " +
                                "WHERE Categoria.Nombre LIKE @searchTerm " +
                                "ORDER BY Producto.Producto_Id";
                    }               
                    else if (searchBy == "marca")
                    {
                        query = "SELECT Producto.Producto_Id AS Código, Producto.Descripcion, Proveedor.Nombre AS Proveedor, " +
                                "Categoria.Nombre AS Categoría, Marca.Nombre AS Marca, Producto.Cantidad, Producto.Costo, Producto.Precio " +
                                "FROM Producto INNER JOIN Proveedor ON Producto.Proveedor_Id = Proveedor.Proveedor_Id " +
                                "INNER JOIN Categoria ON Producto.Categoria_Id = Categoria.Categoria_Id " +
                                "INNER JOIN Marca ON Producto.Marca_Id = Marca.Marca_Id " +
                                "WHERE Marca.Nombre LIKE @searchTerm " +
                                "ORDER BY Producto.Producto_Id";                
                    }
                    #endregion

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Product producto = new Product
                        {
                            Id = Convert.ToInt32(reader["Código"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            ProveedorNombre = reader["Proveedor"].ToString(),
                            CategoriaNombre = reader["Categoría"].ToString(),
                            MarcaNombre = reader["Marca"].ToString(),
                            Cantidad = Convert.ToInt32(reader["Cantidad"]),
                            Costo = (decimal)Convert.ToSingle(reader["Costo"]),
                            Precio = (decimal)Convert.ToSingle(reader["Precio"])
                        };

                        productos.Add(producto);
                    }

                    reader.Close();
                }
            }

            return productos;
        }

        public List<Product> ObtenerTodosLosProductos()
        {
            List<Product> productos = new List<Product>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT Producto.Producto_Id AS Id, Producto.Descripcion, " +
                               "Proveedor.Nombre AS Proveedor, Categoria.Nombre AS Categoria, " +
                               "Marca.Nombre AS Marca, Producto.Cantidad, Producto.Costo, Producto.Precio " +
                               "FROM Producto INNER JOIN Proveedor ON Producto.Proveedor_Id = Proveedor.Proveedor_Id " +
                               "INNER JOIN Categoria ON Producto.Categoria_Id = Categoria.Categoria_Id " +
                               "INNER JOIN Marca ON Producto.Marca_Id = Marca.Marca_Id";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Product producto = new Product
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Descripcion = reader["Descripcion"].ToString(),
                        ProveedorNombre = reader["Proveedor"].ToString(),
                        CategoriaNombre = reader["Categoria"].ToString(),
                        MarcaNombre = reader["Marca"].ToString(),
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        Costo = (decimal)Convert.ToSingle(reader["Costo"]),
                        Precio = (decimal)Convert.ToSingle(reader["Precio"])
                    };

                    productos.Add(producto);
                }

                reader.Close();
            }

            return productos;
        }

        public Product ObtenerProductoPorId(int id)
        {
            Product producto = null;

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT Producto.Producto_Id AS Id, Producto.Descripcion, " +
                               "Proveedor.Nombre AS Proveedor, Categoria.Nombre AS Categoria, " +
                               "Marca.Nombre AS Marca, Producto.Cantidad, Producto.Costo, Producto.Precio " +
                               "FROM Producto INNER JOIN Proveedor ON Producto.Proveedor_Id = Proveedor.Proveedor_Id " +
                               "INNER JOIN Categoria ON Producto.Categoria_Id = Categoria.Categoria_Id " +
                               "INNER JOIN Marca ON Producto.Marca_Id = Marca.Marca_Id " +
                               "WHERE Producto.Producto_Id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    producto = new Product
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Descripcion = reader["Descripcion"].ToString(),
                        ProveedorNombre = reader["Proveedor"].ToString(),
                        CategoriaNombre = reader["Categoria"].ToString(),
                        MarcaNombre = reader["Marca"].ToString(),
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        Costo = (decimal)Convert.ToSingle(reader["Costo"]),
                        Precio = (decimal)Convert.ToSingle(reader["Precio"])
                    };
                }

                reader.Close();
            }

            return producto;
        }
    }
}
