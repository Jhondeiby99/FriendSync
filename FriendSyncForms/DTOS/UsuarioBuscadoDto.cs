using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendSyncForms.DTOS
{
    public class UsuarioBuscadoDto
    {
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public byte[] FotoPerfil { get;  set; }
    }
}