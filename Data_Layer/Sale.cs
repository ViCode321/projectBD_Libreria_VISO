using Model_Layer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer
{
    public class Sale : connection
    {
        //        public void InsertarVenta(string cliente, string producto, string fecha, int cantidad, string tipoMoneda, decimal precio, out int numeroFactura)
        public void InsertarVenta(Venta venta)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();

                    Get get = new Get();
                    // Obtener IDs necesarios

                    // Modificar ObtenerClienteIdPorNombre para aceptar el nombre completo
                    int clienteId = get.ObtenerClienteIdPorNombre(venta.Cliente);

                    // Si el cliente no existe, intentar insertarlo
                    if (clienteId == 0)
                    {
                        clienteId = get.InsertarClienteYObtenerId(venta.Cliente);
                    }

                    int productoId = get.ObtenerProductoIdPorDescripcion(venta.Producto);
                    int tipoMonedaId = get.ObtenerTipoMonedaIdPorTipo(venta.TipoMoneda);

                    // Insertar factura
                    string insertFacturaQuery = "INSERT INTO Factura (Fecha, Cliente_Id, Monto_final, Moneda_Id) VALUES (@fecha, @clienteId, @montoFinal, @tipoMonedaId); SELECT SCOPE_IDENTITY();";
                    SqlCommand insertFacturaCommand = new SqlCommand(insertFacturaQuery, connection);
                    insertFacturaCommand.Parameters.AddWithValue("@fecha", venta.Fecha);
                    insertFacturaCommand.Parameters.AddWithValue("@clienteId", clienteId);
                    insertFacturaCommand.Parameters.AddWithValue("@tipoMonedaId", tipoMonedaId);
                    insertFacturaCommand.Parameters.AddWithValue("@montoFinal", Math.Round(venta.Final, 2));

                    // Obtener el número de factura generado
                    venta.Id = Convert.ToInt32(insertFacturaCommand.ExecuteScalar());

                    // Actualizar el monto final en la factura
                    string updateFacturaQuery = "UPDATE Factura SET Monto_final = ROUND(@precio * @cantidad, 2) WHERE Numero_Factura = @numeroFactura";

                    using (SqlCommand updateFacturaCommand = new SqlCommand(updateFacturaQuery, connection))
                    {
                        try
                        {
                            // Validar que la cantidad y el precio no sean negativos
                            if (venta.Cantidad < 0 || venta.Precio < 0)
                            {
                                throw new ArgumentException("La cantidad y el precio deben ser valores no negativos.");
                            }

                            updateFacturaCommand.Parameters.AddWithValue("@precio", Math.Round(venta.Precio, 2));
                            updateFacturaCommand.Parameters.AddWithValue("@cantidad", venta.Cantidad);
                            updateFacturaCommand.Parameters.AddWithValue("@numeroFactura", venta.Id);

                            updateFacturaCommand.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            // Manejar el error, ya sea registrándolo, lanzando una excepción diferente o manejándolo de otra manera
                            throw new Exception("Error al actualizar el monto final en la factura.", ex);
                        }
                    }

                    // Insertar detalle de factura
                    string insertDetalleQuery = "INSERT INTO Detalle_Factura (Numero_Factura, Producto_Id, Cantidad, Precio, Total_ventas) VALUES (@numeroFactura, @productoId, @cantidad, @precio, @precio * @cantidad)";
                    SqlCommand insertDetalleCommand = new SqlCommand(insertDetalleQuery, connection);
                    insertDetalleCommand.Parameters.AddWithValue("@numeroFactura", venta.Id);
                    insertDetalleCommand.Parameters.AddWithValue("@productoId", productoId);
                    insertDetalleCommand.Parameters.AddWithValue("@cantidad", venta.Cantidad);
                    insertDetalleCommand.Parameters.AddWithValue("@precio", Math.Round(venta.Precio, 2));

                    insertDetalleCommand.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Manejar excepciones de SQL Server
                throw new Exception("Error al insertar la venta.", ex);
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                throw new Exception("Error inesperado al insertar la venta.", ex);
            }
        }

    }
}
