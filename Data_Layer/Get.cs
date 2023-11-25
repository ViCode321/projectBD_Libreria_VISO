using Model_Layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
                        Monto = (decimal)Convert.ToSingle(reader["Monto"]),
                        Total = (decimal)Convert.ToSingle(reader["Total"]),
                        Final = (decimal)Convert.ToSingle(reader["Final"]),
                    };

                    ventas.Add(venta);
                }

                reader.Close();
            }

            return ventas;
        }

    }
}
