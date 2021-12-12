using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto2.Models
{
    public partial class ComercioUsuario
    {
        public int Id { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdComercio { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public bool? Estado { get; set; }

        public virtual AfiliacionComercio IdComercioNavigation { get; set; }
        public virtual Usuario IdUsuarioNavigation { get; set; }
    }
}
