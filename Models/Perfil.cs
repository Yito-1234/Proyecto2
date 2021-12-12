using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto2.Models
{
    public partial class Perfil
    {
        public Perfil()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdPerfil { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
