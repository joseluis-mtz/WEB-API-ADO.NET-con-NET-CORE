using System;
using System.Collections.Generic;
using System.Text;
using ServiciosWebElPunto.Modelos;
using System.Threading.Tasks;

namespace ServiciosWebElPunto.Interfaces
{
    public interface IPedido:IGenerica<PedidoModel>
    {
        Task<PedidoModel> ObtenerPedidoID(int Id);

        Task<List<PedidoModel>> ObtenerListadoPedidos();

        Task<int> GuardarPedido(PedidoModel objPedido);
    }
}
