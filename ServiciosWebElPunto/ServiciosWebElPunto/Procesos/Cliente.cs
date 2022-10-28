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
    class Cliente : ICliente
    {

        private readonly string _cadenaConexion;

        public Cliente(string CadenaConexion)
        {
            _cadenaConexion = CadenaConexion;
        }

        public async Task<bool> Eliminar(int Id)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {

                SqlCommand cmd = new SqlCommand("SP_Eliminar_Cliente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_cliente", Id);

                try
                {
                    await con.OpenAsync();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                    return false;
                }
                return true;
            }
        }

        public async Task<bool> Guardar(ClienteModel ObjModelo)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("SP_Agregar_Cliente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_cliente", ObjModelo.ClienteID);
                cmd.Parameters.AddWithValue("@Nom_Cliente", ObjModelo.NombreCli);
                cmd.Parameters.AddWithValue("@Ape_Cliente", ObjModelo.ApellidosCli);
                cmd.Parameters.AddWithValue("@Dir_cliente", ObjModelo.DireccionCli);
                cmd.Parameters.AddWithValue("@Tel_Cliente", ObjModelo.TelefonoCli);

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

        public async Task<ClienteModel> ObtenerClienteID(int Id)
        {
            ClienteModel GetClienteID = new ClienteModel();

            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("SP_Obtener_Cliente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_cliente", Id);

                try
                {
                    await con.OpenAsync();

                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();

                    while (sdr.Read())
                    {
                        GetClienteID.ClienteID = Convert.ToInt32(sdr["ClienteID"]);
                        GetClienteID.NombreCli = sdr["Nombre"].ToString();
                        GetClienteID.ApellidosCli = sdr["Apellidos"].ToString();
                        GetClienteID.DireccionCli = sdr["Direccion"].ToString();
                        GetClienteID.TelefonoCli = sdr["Telefono"].ToString();
                    }

                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                }
            }
            return GetClienteID;
        }

        public async Task<List<ClienteModel>> ObtenerListadoClientes()
        {
            List<ClienteModel> GetClienteList = new List<ClienteModel>();

            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("SP_Obtener_Cliente", con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();

                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();

                    while (sdr.Read())
                    {
                        GetClienteList.Add(new ClienteModel
                        {
                            ClienteID = Convert.ToInt32(sdr["ClienteID"]),
                            NombreCli = sdr["Nombre"].ToString(),
                            ApellidosCli = sdr["Apellidos"].ToString(),
                            DireccionCli = sdr["Direccion"].ToString(),
                            TelefonoCli = sdr["Telefono"].ToString()
                        });
                    }

                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                }
            }
            return GetClienteList;
        }
    }
}
