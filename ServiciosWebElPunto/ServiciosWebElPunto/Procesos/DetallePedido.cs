using ServiciosWebElPunto.Interfaces;
using ServiciosWebElPunto.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWebElPunto.Procesos
{
    class DetallePedido : IDetallePedido
    {
        private readonly string _cadenaConexion;
        public DetallePedido(string CadenaConexion)
        {
            _cadenaConexion = CadenaConexion;
        }
        public async Task<bool> Eliminar(DetallePedidoModel ObjDetallePedido)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {

                SqlCommand cmd = new SqlCommand("SP_Eliminar_PedidoDetalle", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Pedido", ObjDetallePedido.PedidoID);
                cmd.Parameters.AddWithValue("@Id_Producto", ObjDetallePedido.ProductoID);

                try
                {
                    await con.OpenAsync();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return true;
                }
                catch (Exception)
                {
                    con.Close();
                    return false;
                }
            }
        }

        public Task<bool> Eliminar(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Guardar(DetallePedidoModel ObjModelo)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {

                SqlCommand cmd = new SqlCommand("SP_Agregar_PedidoDetalle", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Pedido", ObjModelo.PedidoID);
                cmd.Parameters.AddWithValue("@Id_Producto", ObjModelo.ProductoID);
                cmd.Parameters.AddWithValue("@Cant_Prod", ObjModelo.CantUnit);
                cmd.Parameters.AddWithValue("@VlrUni_Prod", ObjModelo.VlrUnitProd);
                try
                {
                    await con.OpenAsync();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    return true;
                }
                catch (Exception)
                {
                    con.Close();
                    return false;
                }
            }
        }

        public async Task<DetallePedidoModel> ObtenerDetallePedidoID(int IdPedido, int IdProducto)
        {
            DetallePedidoModel GetDetallePedidoID = new DetallePedidoModel();

            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("SP_Obtener_PedidoDetalle", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Pedido", IdPedido);
                cmd.Parameters.AddWithValue("@Id_Producto", IdProducto);

                try
                {
                    await con.OpenAsync();

                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();

                    while (sdr.Read())
                    {
                        GetDetallePedidoID.PedidoID = Convert.ToInt32(sdr["PedidoID"]);
                        GetDetallePedidoID.ProductoID = Convert.ToInt32(sdr["ProductoID"]);
                        GetDetallePedidoID.CantUnit = Convert.ToInt32(sdr["Cantidad"]);
                        GetDetallePedidoID.VlrUnitProd = Convert.ToInt32(sdr["ValorUnitario"]);
                        GetDetallePedidoID.VlrTotalProd = Convert.ToInt32(sdr["ValorTotal"]);
                    }

                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                }
            }
            return GetDetallePedidoID;
        }

        public async Task<List<DetallePedidoModel>> ObtenerListadoDetallePedidos(int IdPedido)
        {
            List<DetallePedidoModel> GetDetalleList = new List<DetallePedidoModel>();

            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("SP_Obtener_PedidoDetalle", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Pedido", IdPedido);

                try
                {
                    await con.OpenAsync();

                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();

                    while (sdr.Read())
                    {
                        GetDetalleList.Add(new DetallePedidoModel
                        {
                            PedidoID = Convert.ToInt32(sdr["PedidoID"]),
                            ProductoID = Convert.ToInt32(sdr["ProductoID"]),
                            CantUnit = Convert.ToInt32(sdr["Cantidad"]),
                            VlrUnitProd = Convert.ToInt32(sdr["ValorUnitario"]),
                            VlrTotalProd = Convert.ToInt32(sdr["ValorTotal"])
                        });
                    }

                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                }
            }
            return GetDetalleList;
        }
    }
}
