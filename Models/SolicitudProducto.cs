using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto2.Models
{
    public partial class SolicitudProducto
    {
        public int IdReseta { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public int? IdComida { get; set; }

        public virtual Comida IdComidaNavigation { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
