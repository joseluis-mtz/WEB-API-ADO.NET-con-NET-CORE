using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciosWebElPunto.Modelos
{
    public class ProductoModel
    {
        public int ProductoID { get; set; }
        public string NombreProd { get; set; }
        public decimal PrecioUnit { get; set; }
        public string EstadoProd { get; set; }
    }
}
