//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FriendSync.ModeloFriendSync
{
    using System;
    using System.Collections.Generic;
    
    public partial class users
    {
        public int IdUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public string email { get; set; }
        public string contraseña { get; set; }
        public string nombreCompleto { get; set; }
        public Nullable<System.DateTime> fechaNac { get; set; }
        public byte[] fotoPerfil { get; set; }
    }
}
