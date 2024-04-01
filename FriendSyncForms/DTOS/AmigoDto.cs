using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendSyncForms.DTOS
{
    public class AmigosDto
    {
        public string NombreUsuario { get; set; }
        public byte[] FotoPerfil { get; set; }
        public int AmigoId { get; set; }
    }
}