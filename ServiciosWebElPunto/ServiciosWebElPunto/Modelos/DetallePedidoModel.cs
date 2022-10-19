using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciosWebElPunto.Modelos
{
    public class DetallePedidoModel
    {
        public int PedidoID { get; set; }
        public int ProductoID { get; set; }
        public int CantUnit { get; set; }
        public decimal VlrUnitProd { get; set; }
        public decimal VlrTotalProd { get; set; }
    }
}
