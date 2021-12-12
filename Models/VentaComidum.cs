using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto2.Models
{
    public partial class VentaComidum
    {
        public int IdVenta { get; set; }
        public int? IdComida { get; set; }
        public int? Cantidad { get; set; }
        public DateTime? Fecha { get; set; }

        public virtual Comida IdComidaNavigation { get; set; }
    }
}
