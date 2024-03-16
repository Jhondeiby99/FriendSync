using FriendSync.ModeloFriendSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FriendSyncForms
{
    public partial class InfoUsuario : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["userId"] != null)
                {
                    int userId = Convert.ToInt32(Request.QueryString["userId"]);

                    using (var db = new FriendSync.ModeloFriendSync.FriendSyncBDEntities())
                    {
                        var usuario = db.users.FirstOrDefault(u => u.IdUsuario == userId);

                        if (usuario != null)
                        {
                            txtIdUsuario.Value = usuario.IdUsuario.ToString();
                            txtNombreUsuario.Value = usuario.nombreUsuario;
                            txtEmail.Value = usuario.email;
                            txtNombreCompleto.Value = usuario.nombreCompleto;
                            txtFechaNacimiento.Value = usuario.fechaNac?.ToString("dd/MM/yyyy");

                            string fotoPerfilBase64 = Convert.ToBase64String(usuario.fotoPerfil);
                            userImage.Src = $"data:image/jpeg;base64,{fotoPerfilBase64}";
                        }
                        else
                        {
                            Response.Write("Usuario no encontrado");
                        }
                    }
                }
                else
                {
                    Response.Write("Falta el parámetro userId en la URL");
                }

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect($"PaginaInicio.aspx?userId={Request.QueryString["userId"]}");
        }



    }
}