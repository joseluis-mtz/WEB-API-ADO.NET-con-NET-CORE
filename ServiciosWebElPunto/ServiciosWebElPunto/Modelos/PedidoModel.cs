using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ServiciosWebElPunto.Modelos
{
    public class PedidoModel
    {
        public int PedidoID { get; set; }
        public int ClienteID { get; set; }
        public string NombreCliente { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaPed { get; set; }
        public string EstadoPed { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal ValorTotalPed { get; set; }
    }
}
