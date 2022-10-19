using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiciosWebElPunto.Interfaces
{
    public interface IGenerica<T> where T : class
    {
        Task<bool> Guardar(T ObjModelo);

        Task<bool> Eliminar(int Id);
    }
}
