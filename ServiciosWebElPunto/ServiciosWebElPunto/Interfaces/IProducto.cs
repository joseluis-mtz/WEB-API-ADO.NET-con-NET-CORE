using System;
using System.Collections.Generic;
using System.Text;
using ServiciosWebElPunto.Modelos;
using System.Threading.Tasks;

namespace ServiciosWebElPunto.Interfaces
{
    public interface IProducto: IGenerica<ProductoModel>
    {
        Task<ProductoModel> ObtenerProductoID(int Id);
        Task<List<ProductoModel>> ObtenerListadoProductos();
    }
}
