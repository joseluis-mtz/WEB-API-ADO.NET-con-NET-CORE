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
    class Pedido : IPedido
    {

        private readonly string _cadenaConexion;
        public Pedido(string CadenaConexion)
        {
            _cadenaConexion = CadenaConexion;
        }
        public async Task<bool> Eliminar(int Id)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {

                SqlCommand cmd = new SqlCommand("SP_Eliminar_Pedido", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Pedido", Id);

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

        public Task<bool> Guardar(PedidoModel ObjModelo)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GuardarPedido(PedidoModel objPedido)
        {
            int result = 0;

            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {

                SqlCommand cmd = new SqlCommand("SP_Agregar_Pedido", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Pedido", objPedido.PedidoID);
                cmd.Parameters.AddWithValue("@Id_Cliente", objPedido.ClienteID);
                cmd.Parameters.AddWithValue("@Id_Estado", objPedido.EstadoPed);
                cmd.Parameters.AddWithValue("@Fec_Pedido", objPedido.FechaPed);

                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();

                    while (sdr.Read())
                    {
                        result = Convert.ToInt32(sdr["ID_SCOPE"]);
                    }

                    con.Close();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    con.Close();
                    return 0;
                }
                return result;
            }
        }

        public async Task<List<PedidoModel>> ObtenerListadoPedidos()
        {
            List<PedidoModel> GetPedidoList = new List<PedidoModel>();

            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Get_Pedido", con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();

                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();

                    while (sdr.Read())
                    {
                        GetPedidoList.Add(new PedidoModel
                        {
                            PedidoID = Convert.ToInt32(sdr["PedidoID"]),
                            ClienteID = Convert.ToInt32(sdr["ClienteID"]),
                            NombreCliente = sdr["NombreCliente"].ToString(),
                            EstadoPed = sdr["EstadoPedido"].ToString(),
                            FechaPed = Convert.ToDateTime(sdr["FechaPedido"]),
                            ValorTotalPed = Convert.ToDecimal(sdr["ValorTotal"])
                        });
                    }

                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                }
            }
            return GetPedidoList;
        }

        public async Task<PedidoModel> ObtenerPedidoID(int Id)
        {
            PedidoModel GetPedidoID = new PedidoModel();

            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Get_Pedido", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Pedido", Id);

                try
                {
                    await con.OpenAsync();

                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();

                    while (sdr.Read())
                    {
                        GetPedidoID.PedidoID = Convert.ToInt32(sdr["PedidoID"]);
                        GetPedidoID.ClienteID = Convert.ToInt32(sdr["ClienteID"]);
                        GetPedidoID.NombreCliente = sdr["NombreCliente"].ToString();
                        GetPedidoID.EstadoPed = sdr["EstadoPedido"].ToString();
                        GetPedidoID.FechaPed = Convert.ToDateTime(sdr["FechaPedido"]);
                        GetPedidoID.ValorTotalPed = Convert.ToDecimal(sdr["ValorTotal"]);
                    }

                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                }
            }
            return GetPedidoID;
        }
    }
}
