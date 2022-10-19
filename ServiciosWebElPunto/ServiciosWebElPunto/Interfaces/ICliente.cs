using System;
using System.Collections.Generic;
using System.Text;
using ServiciosWebElPunto.Modelos;
using System.Threading.Tasks;

namespace ServiciosWebElPunto.Interfaces
{
    public interface ICliente:IGenerica<ClienteModel>
    {
        Task<ClienteModel> ObtenerClienteID(int Id);
        Task<List<ClienteModel>> ObtenerListadoClientes();
    }
}
