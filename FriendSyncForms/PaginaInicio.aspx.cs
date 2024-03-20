using FriendSyncDB.ModeloFriendSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FriendSyncForms
{
    public partial class PaginaInicio : System.Web.UI.Page
    {
        FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities db = new FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verificar si se recibió un ID de usuario desde la página de inicio de sesión
            if (!IsPostBack && Request.QueryString["userId"] != null)
            {
                // Obtener el ID de usuario de la URL
                int userId = Convert.ToInt32(Request.QueryString["userId"]);

                // Cargar la información del usuario utilizando el ID
                CargarInformacionUsuario(userId);
            }
        }

        // Método para cargar la información del usuario
        private void CargarInformacionUsuario(int userId)
        {
            // Crear una instancia del contexto de la base de datos
            using (FriendSyncBDEntities db = new FriendSyncBDEntities())
            {
                // Obtener el usuario de la base de datos utilizando el ID proporcionado
                users usuario = db.users.Find(userId);

                if (usuario != null)
                {
                    // Mostrar el nombre del usuario en el título de la página
                    Page.Title = $"{usuario.nombreUsuario}'s Pagina de Inicio";

                    // Mostrar la foto de perfil del usuario
                    if (usuario.fotoPerfil != null)
                    {
                        // Convertir la foto de perfil a una cadena base64
                        string base64String = Convert.ToBase64String(usuario.fotoPerfil);
                        string imageUrl = $"data:image/jpeg;base64,{base64String}";

                        // Asignar la URL de la imagen al atributo src del elemento img en el HTML
                        navUsuarioFoto.Src = imageUrl;
                    }
                }
            }
        }

        

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect($"InfoUsuario.aspx?userId={Request.QueryString["userId"]}");
        }protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect($"SeccionAmigos.aspx?userId={Request.QueryString["userId"]}");
        }
    }
}