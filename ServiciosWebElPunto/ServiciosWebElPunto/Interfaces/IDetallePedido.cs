using System;
using System.Collections.Generic;
using System.Text;
using ServiciosWebElPunto.Modelos;
using System.Threading.Tasks;

namespace ServiciosWebElPunto.Interfaces
{
    public interface IDetallePedido:IGenerica<DetallePedidoModel>
    {
        Task<DetallePedidoModel> ObtenerDetallePedidoID(int IdPedido, int IdProducto);
        Task<List<DetallePedidoModel>> ObtenerListadoDetallePedidos(int IdPedido);

        Task<bool> Eliminar(DetallePedidoModel ObjDetallePedido);
    }
}
