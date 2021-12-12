using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto2.Models
{
    public partial class Comida
    {
        public Comida()
        {
            SolicitudProductos = new HashSet<SolicitudProducto>();
            VentaComida = new HashSet<VentaComidum>();
        }

        public int IdComida { get; set; }
        public string NomComida { get; set; }
        public string Descripcion { get; set; }
        public byte[] Fotografia { get; set; }
        public decimal? Precio { get; set; }
        public string Estado { get; set; }
        public int? IdComercio { get; set; }

        public virtual AfiliacionComercio IdComercioNavigation { get; set; }
        public virtual ICollection<SolicitudProducto> SolicitudProductos { get; set; }
        public virtual ICollection<VentaComidum> VentaComida { get; set; }
    }
}
