using FriendSyncForms.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FriendSyncForms
{
    public partial class SeccionAmigos : System.Web.UI.Page
    {
        FriendSync.ModeloFriendSync.FriendSyncBDEntities db = new FriendSync.ModeloFriendSync.FriendSyncBDEntities();
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string searchTerm = ((TextBox)form1.FindControl("TextBox1")).Text;

            var usuariosEncontrados = db.users
                .Where(user => user.nombreCompleto.Contains(searchTerm) || user.email.Contains(searchTerm))
                .Select(user => new UsuarioBuscadoDto
                {
                    NombreCompleto = user.nombreCompleto,
                    Email = user.email,
                    FotoPerfil = user.fotoPerfil
                })
                .ToList();

            phAmigos.Controls.Clear();

            foreach (var usuario in usuariosEncontrados)
            {
                HtmlGenericControl cardDiv = new HtmlGenericControl("div");
                cardDiv.Attributes["class"] = "card amigosCard";

                Image img = new Image();
                img.CssClass = "card-img-top";

                if (usuario.FotoPerfil != null && usuario.FotoPerfil.Length > 0)
                {
                    string base64String = "data:image/png;base64," + Convert.ToBase64String(usuario.FotoPerfil, 0, usuario.FotoPerfil.Length);
                    img.ImageUrl = base64String;
                }
                else
                {
                    img.ImageUrl = "../Media/imagen1.jpg";
                }

                HtmlGenericControl cardBody = new HtmlGenericControl("div");
                cardBody.Attributes["class"] = "card-body";

                Label lblTitle = new Label();
                lblTitle.Text = "<b>Usuario:</b>"+usuario.NombreCompleto+"<br></br>";
                lblTitle.CssClass = "card-title";

                Label lblText = new Label();
                lblText.Text = "<b>Email: </b>"+usuario.Email;
                lblText.CssClass = "card-text";

                cardBody.Controls.Add(img);
                cardBody.Controls.Add(lblTitle);
                cardBody.Controls.Add(lblText);

                cardDiv.Controls.Add(cardBody);

                phAmigos.Controls.Add(cardDiv);
            }


        }
    }
}