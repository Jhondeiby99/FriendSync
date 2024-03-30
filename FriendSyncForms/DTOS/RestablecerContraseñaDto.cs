using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FriendSyncForms.DTOS
{
    public class RestablecerContraseñaDto
    {
        public string CorreoElectronico { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Contrasena { get; set; }
    }
}