using Model_Layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Data_Layer
{
    public class Sales: connection
    {
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
