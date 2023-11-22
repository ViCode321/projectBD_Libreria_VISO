using Model_Layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    // En el Data Layer
    public class Insert : connection
    {
        public void InsertarProducto(Product product)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                string query = "INSERT INTO Producto (Proveedor_Id, Descripcion, Marca_Id, Categoria_Id, Cantidad, Costo, Precio) " +
                               "VALUES (@ProveedorId, @Descripcion, @MarcaId, @CategoriaId, @Cantidad, @Costo, @Precio)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    /*command.Parameters.AddWithValue("@ProveedorId", product.ProveedorId);
                    command.Parameters.AddWithValue("@Descripcion", product.Descripcion);
                    command.Parameters.AddWithValue("@MarcaId", product.MarcaId);
                    command.Parameters.AddWithValue("@CategoriaId", product.CategoriaId);
                    command.Parameters.AddWithValue("@Cantidad", product.Cantidad);
                    command.Parameters.AddWithValue("@Costo", product.Costo);
                    command.Parameters.AddWithValue("@Precio", product.Precio);

                    command.ExecuteNonQuery();*/
                }
            }
        }

        public void ActualizarProducto(Product product)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();

                string query = "UPDATE Producto " +
                               "SET Descripcion = @descripcion, " +
                               "Proveedor_Id = @proveedorId, " +
                               "Categoria_Id = @categoriaId, " +
                               "Marca_Id = @marcaId, " +
                               "Cantidad = @cantidad, " +
                               "Costo = @costo, " +
                               "Precio = @precio " +
                               "WHERE Producto_Id = @id";

                SqlCommand command = new SqlCommand(query, connection);

                // Parámetros
                command.Parameters.AddWithValue("@descripcion", product.Descripcion);
                command.Parameters.AddWithValue("@proveedorId", product.ProveedorId);
                command.Parameters.AddWithValue("@categoriaId", product.CategoriaId);
                command.Parameters.AddWithValue("@marcaId", product.MarcaId);
                command.Parameters.AddWithValue("@cantidad", product.Cantidad);
                command.Parameters.AddWithValue("@costo", product.Costo);
                command.Parameters.AddWithValue("@precio", product.Precio);
                command.Parameters.AddWithValue("@id", product.Id);

                command.ExecuteNonQuery();
            }
        }

    }

}
