using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto2.Models
{
    public partial class AfiliacionComercio
    {
        public AfiliacionComercio()
        {
            ComercioUsuarios = new HashSet<ComercioUsuario>();
            Comida = new HashSet<Comida>();
            Productos = new HashSet<Producto>();
        }

        public int IdComercio { get; set; }
        public string Nombre { get; set; }
        public string Decripcion { get; set; }
        public string Telefono { get; set; }

        public virtual ICollection<ComercioUsuario> ComercioUsuarios { get; set; }
        public virtual ICollection<Comida> Comida { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
