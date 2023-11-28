using Model_Layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data_Layer
{
    // En el Data Layer
    public class Insert : connection
    {
        public string InsertarProducto(int id, string descripcion, string proveedor, string categoria, string marca, int cantidad, decimal costo, decimal precio)
        {
            try
            {
                Get get = new Get();
                int proveedorId = get.ObtenerProveedorIdPorNombre(proveedor);
                int marcaId = get.ObtenerMarcaIdPorNombre(marca);
                int categoriaId = get.ObtenerCategoriaIdPorNombre(categoria);

                if (!get.ExisteProveedor(proveedorId) || !get.ExisteMarca(marcaId) || !get.ExisteCategoria(categoriaId))
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
                            string query = "INSERT INTO Producto (Proveedor_Id, Descripcion, Marca_Id, Categoria_Id, Cantidad, Costo, Precio) " +
                                           "VALUES (@proveedorId, @descripcion, @marcaId, @categoriaId, @cantidad, @costo, @precio)";

                            using (SqlCommand command = new SqlCommand(query, connection, transaction))
                            {
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
                            throw new Exception("Error al insertar el producto. Detalles: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            // Manejar otras excepciones
                            throw new Exception("Error inesperado al insertar el producto. Detalles: " + ex.Message);
                        }
                    }
                }
                return null; // Sin error

            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al insertar el producto. Detalles: " + ex.Message);
            }
        }


    }

}
