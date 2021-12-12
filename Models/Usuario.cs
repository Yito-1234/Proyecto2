using System;
using System.Collections.Generic;

#nullable disable

namespace Proyecto2.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            ComercioUsuarios = new HashSet<ComercioUsuario>();
        }

        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string UserName { get; set; }
        public string Clave { get; set; }
        public int? IdPerfil { get; set; }

        public virtual Perfil IdPerfilNavigation { get; set; }
        public virtual ICollection<ComercioUsuario> ComercioUsuarios { get; set; }
    }
}
