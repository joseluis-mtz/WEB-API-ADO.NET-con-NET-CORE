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
    class Producto : IProducto
    {
        private readonly string _cadenaConexion;
        public Producto(string CadenaConexion)
        {
            _cadenaConexion = CadenaConexion;
        }

        public async Task<bool> Eliminar(int Id)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {

                SqlCommand cmd = new SqlCommand("SP_Eliminar_Producto", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Producto", Id);
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

        public async Task<bool> Guardar(ProductoModel ObjModelo)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {

                SqlCommand cmd = new SqlCommand("SP_Agregar_Producto", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Producto", ObjModelo.ProductoID);
                cmd.Parameters.AddWithValue("@Nom_Producto", ObjModelo.NombreProd);
                cmd.Parameters.AddWithValue("@Pre_Producto", ObjModelo.PrecioUnit);
                cmd.Parameters.AddWithValue("@Est_Producto", ObjModelo.EstadoProd);
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

        public async Task<List<ProductoModel>> ObtenerListadoProductos()
        {
            List<ProductoModel> GetProductoList = new List<ProductoModel>();


            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("SP_Obtener_Producto", con);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    await con.OpenAsync();

                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();

                    while (sdr.Read())
                    {
                        GetProductoList.Add(new ProductoModel
                        {
                            ProductoID = Convert.ToInt32(sdr["ProductoID"]),
                            NombreProd = sdr["NombreProd"].ToString(),
                            PrecioUnit = Convert.ToDecimal(sdr["PrecioUnit"]),
                            EstadoProd = Convert.ToString(sdr["EstadoProd"])
                        });
                    }

                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                }
            }
            return GetProductoList;
        }

        public async Task<ProductoModel> ObtenerProductoID(int Id)
        {
            ProductoModel GetProductoID = new ProductoModel();


            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                SqlCommand cmd = new SqlCommand("sp_Get_Producto", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Producto", Id);

                try
                {
                    await con.OpenAsync();

                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();

                    while (sdr.Read())
                    {
                        GetProductoID.ProductoID = Convert.ToInt32(sdr["ProductoID"]);
                        GetProductoID.NombreProd = sdr["NombreProd"].ToString();
                        GetProductoID.PrecioUnit = Convert.ToDecimal(sdr["PrecioUnit"]);
                        GetProductoID.EstadoProd = Convert.ToString(sdr["EstadoProd"]);
                    }

                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                }
            }
            return GetProductoID;
        }
    }
}
