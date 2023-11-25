using Model_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public class Update: connection
    {
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
        
        public string ActualizarProductoSearch(int id, string descripcion, string proveedor, string categoria, string marca, int cantidad, decimal costo, decimal precio)
        {
            try
            {
                int proveedorId = ObtenerProveedorIdPorNombre(proveedor);
                int marcaId = ObtenerMarcaIdPorNombre(marca);
                int categoriaId = ObtenerCategoriaIdPorNombre(categoria);

                if (!ExisteProveedor(proveedorId) || !ExisteMarca(marcaId) || !ExisteCategoria(categoriaId))
                {
                    // Devolver un mensaje de error
                    return "Proveedor, marca o categoría no existe en la base de datos.";
                }

                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            string query = "UPDATE Producto " +
                                           "SET Descripcion = @descripcion, " +
                                           "Proveedor_Id = @proveedorId, " +
                                           "Categoria_Id = @categoriaId, " +
                                           "Marca_Id = @marcaId, " +
                                           "Cantidad = @cantidad, " +
                                           "Costo = @costo, " +
                                           "Precio = @precio " +
                                           "WHERE Producto_Id = @id";

                            using (SqlCommand command = new SqlCommand(query, connection, transaction))
                            {
                                // Parámetros
                                command.Parameters.AddWithValue("@descripcion", descripcion);
                                command.Parameters.AddWithValue("@proveedorId", proveedorId);
                                command.Parameters.AddWithValue("@categoriaId", categoriaId);
                                command.Parameters.AddWithValue("@marcaId", marcaId);
                                command.Parameters.AddWithValue("@cantidad", cantidad);
                                command.Parameters.AddWithValue("@costo", costo);
                                command.Parameters.AddWithValue("@precio", precio);
                                command.Parameters.AddWithValue("@id", id);

                                command.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                        catch (SqlException ex)
                        {
                            transaction.Rollback();
                            // Manejar excepciones de SQL Server
                            throw new Exception("Error al actualizar el producto. Detalles: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            // Manejar otras excepciones
                            throw new Exception("Error inesperado al actualizar el producto. Detalles: " + ex.Message);
                        }
                    }
                }
                return null; // Sin error
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                throw new Exception("Error inesperado al actualizar el producto. Detalles: " + ex.Message);
            }
        }

    }
}
