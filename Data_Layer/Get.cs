using Model_Layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data_Layer
{
    public class Get: connection
    {

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
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT Producto.Producto_Id AS Id, Producto.Descripcion, " +
                               "Proveedor.Nombre AS ProveedorNombre, Categoria.Nombre AS CategoriaNombre, " +
                               "Marca.Nombre AS MarcaNombre, Producto.Cantidad, Producto.Costo, Producto.Precio " +
                               "FROM Producto " +
                               "INNER JOIN Proveedor ON Producto.Proveedor_Id = Proveedor.Proveedor_Id " +
                               "INNER JOIN Categoria ON Producto.Categoria_Id = Categoria.Categoria_Id " +
                               "INNER JOIN Marca ON Producto.Marca_Id = Marca.Marca_Id " +
                               "WHERE Producto.Producto_Id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Product
                        {
                            Id = reader.GetInt32(0),
                            Descripcion = reader.GetString(1),
                            ProveedorNombre = reader.GetString(2),
                            CategoriaNombre = reader.GetString(3),
                            MarcaNombre = reader.GetString(4),
                            Cantidad = reader.GetInt32(5),
                            Costo = reader[6] is DBNull ? 0 : Convert.ToDecimal(reader[6]),
                            Precio = reader[7] is DBNull ? 0 : Convert.ToDecimal(reader[7])
                        };
                    }
                }
            }
            return null;
        }

        public List<Venta> ObtenerTodasLasVentas()
        {
            List<Venta> ventas = new List<Venta>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT Factura.Numero_Factura AS Id, Cliente.Nombre + ' ' + Cliente.Apellido AS 'Cliente', Factura.Fecha, Producto.Descripcion AS Producto, Detalle_Factura.Cantidad, " +
                               "Tipo_Moneda.Tipo, Detalle_Factura.Precio AS Monto, Detalle_Factura.Total_ventas AS Total, Factura.Monto_final AS Final " +
                               "FROM Factura INNER JOIN Cliente " +
                               "ON Factura.Cliente_Id = Cliente.Cliente_Id INNER JOIN Tipo_Moneda " +
                               "ON Factura.Moneda_Id = Tipo_Moneda.Moneda_Id INNER JOIN Detalle_Factura " +
                               "ON Factura.Numero_Factura = Detalle_Factura.Numero_Factura INNER JOIN Producto " +
                               "ON Detalle_Factura.Producto_Id = Producto.Producto_Id " +
                               "ORDER BY Factura.Fecha";


                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Venta venta = new Venta
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Cliente = reader["Cliente"].ToString(),
                        Fecha = reader["Fecha"].ToString(),
                        Producto = reader["Producto"].ToString(),
                        Cantidad = Convert.ToInt32(reader["Cantidad"]),
                        TipoMoneda = reader["Tipo"].ToString(),
                        Precio = (decimal)Convert.ToSingle(reader["Monto"]),
                        Total = (decimal)Convert.ToSingle(reader["Total"]),
                        Final = (decimal)Convert.ToSingle(reader["Final"]),
                    };

                    ventas.Add(venta);
                }

                reader.Close();
            }

            return ventas;
        }

        public bool ExisteProveedor(int id)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT COUNT(1) FROM Proveedor WHERE Proveedor_Id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    return (int)command.ExecuteScalar() > 0;
                }
            }
            catch (SqlException ex)
            {
                // Manejar excepciones de SQL Server
                throw new Exception("Error al verificar la existencia del proveedor.", ex);
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                throw new Exception("Error inesperado al verificar la existencia del proveedor.", ex);
            }
        }

        public bool ExisteMarca(int id)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT COUNT(1) FROM Marca WHERE Marca_Id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    return (int)command.ExecuteScalar() > 0;
                }
            }
            catch (SqlException ex)
            {
                // Manejar excepciones de SQL Server
                throw new Exception("Error al verificar la existencia de la marca.", ex);
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                throw new Exception("Error inesperado al verificar la existencia de la marca.", ex);
            }
        }

        public bool ExisteCategoria(int id)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT COUNT(1) FROM Categoria WHERE Categoria_Id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    return (int)command.ExecuteScalar() > 0;
                }
            }
            catch (SqlException ex)
            {
                // Manejar excepciones de SQL Server
                throw new Exception("Error al verificar la existencia de la categoría.", ex);
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                throw new Exception("Error inesperado al verificar la existencia de la categoría.", ex);
            }
        }

        public int ObtenerProveedorIdPorNombre(string nombre)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT Proveedor_Id FROM Proveedor WHERE Nombre = @nombre";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", nombre);

                object result = command.ExecuteScalar();

                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public int ObtenerMarcaIdPorNombre(string nombre)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT Marca_Id FROM Marca WHERE Nombre = @nombre";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", nombre);

                object result = command.ExecuteScalar();

                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public int ObtenerCategoriaIdPorNombre(string nombre)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT Categoria_Id FROM Categoria WHERE Nombre = @nombre";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", nombre);

                object result = command.ExecuteScalar();

                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public bool ExisteProducto(int id)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Producto WHERE Producto_Id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", id);

                    return (int)command.ExecuteScalar() > 0;
                }
            }
            catch (SqlException ex)
            {
                // Manejar excepciones de SQL Server
                throw new Exception("Error al verificar la existencia del producto.", ex);
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                throw new Exception("Error inesperado al verificar la existencia del producto.", ex);
            }
        }

        public bool ActualizarProducto(string name)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Producto WHERE Descripcion = @name";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@name", name);

                    return (int)command.ExecuteScalar() > 0;
                }
            }
            catch (SqlException ex)
            {
                // Manejar excepciones de SQL Server
                throw new Exception("Error al verificar la existencia del producto.", ex);
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                throw new Exception("Error inesperado al verificar la existencia del producto.", ex);
            }
        }

        public int ObtenerProductoIdPorDescripcion(string descripcion)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT Producto_Id FROM Producto WHERE Descripcion = @descripcion";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@descripcion", descripcion);

                object result = command.ExecuteScalar();

                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public int ObtenerTipoMonedaIdPorTipo(string tipo)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Moneda_Id FROM Tipo_Moneda WHERE Tipo = @tipo";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@tipo", tipo);

                    object result = command.ExecuteScalar();

                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch (SqlException ex)
            {
                // Manejar excepciones de SQL Server
                throw new Exception("Error al obtener el ID del tipo de moneda.", ex);
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                throw new Exception("Error inesperado al obtener el ID del tipo de moneda.", ex);
            }
        }

        public int ObtenerClienteIdPorNombre(string nombreCompleto)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Cliente_Id FROM Cliente WHERE CONCAT(Nombre, ' ', Apellido) = @nombreCompleto";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);

                    object result = command.ExecuteScalar();

                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch (SqlException ex)
            {
                // Manejar excepciones de SQL Server
                throw new Exception("Error al obtener el ID del cliente.", ex);
            }
            catch (Exception ex)
            {
                // Si no se encuentra el cliente, intentar insertarlo
                if (ex.Message.Contains("Error al obtener el ID del cliente") && ex.InnerException is SqlException innerException && innerException.Number == 208)
                {
                    // Intentar insertar al cliente
                    return InsertarClienteYObtenerId(nombreCompleto);
                }

                // Manejar otras excepciones
                throw new Exception("Error inesperado al obtener el ID del cliente.", ex);
            }
        }

        public int ObtenerPrescioDeProducto(string name)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    string query = "SELECT Precio FROM Producto WHERE Descripcion = @name";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@name", name);
                    object result = command.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : 0;
                }
            }
            catch (SqlException ex)
            {
                // Manejar excepciones de SQL Server
                throw new Exception("Error al obtener el precio.", ex);
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                throw new Exception("Error inesperado al obtener el precio.", ex);
            }
        }


        public int InsertarClienteYObtenerId(string nombreCompleto)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                // Separar el nombre y apellido
                string[] partesNombre = nombreCompleto.Split(' ');
                string nombre = partesNombre[0];
                string apellido = partesNombre.Length > 1 ? partesNombre[1] : "";

                // Insertar al cliente
                string insertClienteQuery = "INSERT INTO Cliente (Nombre, Apellido) VALUES (@nombre, @apellido); SELECT SCOPE_IDENTITY();";
                SqlCommand insertClienteCommand = new SqlCommand(insertClienteQuery, connection);
                insertClienteCommand.Parameters.AddWithValue("@nombre", nombre);
                insertClienteCommand.Parameters.AddWithValue("@apellido", apellido);

                // Obtener el ID del cliente insertado
                return Convert.ToInt32(insertClienteCommand.ExecuteScalar());
            }
        }

        public List<Venta> ObtenerTodosNombresDeLosProductos()
        {
            List<Venta> productos = new List<Venta>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT Descripcion as Producto FROM Producto";

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Venta producto = new Venta
                    {
                        Producto = reader["Producto"].ToString(),
                    };

                    productos.Add(producto);
                }

                reader.Close();
            }

            return productos;
        }


        /*public List<ProductItem> ObtenerCategoriasPorProducto(int productoId)
        {
            List<ProductItem> categorias = new List<ProductItem>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT Categoria.Nombre FROM Categoria " +
                               "INNER JOIN Producto ON Categoria.Categoria_Id = Producto.Categoria_Id " +
                               "WHERE Producto.Producto_Id = @productoId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productoId", productoId);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductItem categoria = new ProductItem
                        {
                            Nombre = reader["Nombre"].ToString()
                        };

                        categorias.Add(categoria);
                    }

                    reader.Close();
                }
            }

            return categorias;
        }

        public List<ProductItem> ObtenerMarcasPorProducto(int productoId)
        {
            List<ProductItem> marcas = new List<ProductItem>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT Marca.Nombre FROM Marca " +
                               "INNER JOIN Producto ON Marca.Marca_Id = Producto.Marca_Id " +
                               "WHERE Producto.Producto_Id = @productoId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productoId", productoId);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductItem marca = new ProductItem
                        {
                            Nombre = reader["Nombre"].ToString()
                        };

                        marcas.Add(marca);
                    }

                    reader.Close();
                }
            }

            return marcas;
        }

        public List<ProductItem> ObtenerProveedoresPorProducto(int productoId)
        {
            List<ProductItem> proveedores = new List<ProductItem>();

            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "SELECT Proveedor.Nombre FROM Proveedor " +
                               "INNER JOIN Proveedor_Producto ON Proveedor.Proveedor_Id = Proveedor_Producto.Proveedor_Id " +
                               "INNER JOIN Producto ON Proveedor_Producto.Producto_Id = Producto.Producto_Id " +
                               "WHERE Producto.Producto_Id = @productoId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@productoId", productoId);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ProductItem proveedor = new ProductItem
                        {
                            Nombre = reader["Nombre"].ToString()
                        };

                        proveedores.Add(proveedor);
                    }

                    reader.Close();
                }
            }

            return proveedores;
        }*/
    }
}
