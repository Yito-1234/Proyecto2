using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto2.Models
{
    public partial class Producto
    {
        public Producto()
        {
            SolicitudProductos = new HashSet<SolicitudProducto>();
        }

        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte[] Fotografia { get; set; }
        public decimal? Precio { get; set; }
        public int? IdComercio { get; set; }
        public int? Cantidad { get; set; }

        public virtual AfiliacionComercio IdComercioNavigation { get; set; }
        public virtual ICollection<SolicitudProducto> SolicitudProductos { get; set; }
    }
}
