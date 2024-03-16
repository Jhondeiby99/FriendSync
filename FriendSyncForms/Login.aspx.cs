using FriendSync.ModeloFriendSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FriendSyncForms
{
    public partial class Login : System.Web.UI.Page
    {
        FriendSync.ModeloFriendSync.FriendSyncBDEntities db = new FriendSync.ModeloFriendSync.FriendSyncBDEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string credencial = TextBox1.Text;
            string contraseña = TextBox2.Text;

            int idUsuario = ObtenerIdUsuario(credencial);

            if (idUsuario != -1 && ValidarCredenciales(credencial, contraseña))
            {
                Session["UserId"] = "idUsuario";

                Response.Redirect($"PaginaInicio.aspx?userId={idUsuario}");
            }
            else
            {
                Label1.Text = "Credenciales Incorrectas";
            }
        }

        private int ObtenerIdUsuario(string credencialNombreEmail)
        {
            var usuario = db.users.FirstOrDefault(u => u.nombreUsuario == credencialNombreEmail || u.email == credencialNombreEmail);

            if (usuario != null)
            {
                return usuario.IdUsuario;
            }
            else
            {
                return -1;
            }
        }

        private bool ValidarCredenciales(string credencialNombreEmail, string contraseña)
        {
            var usuario = db.users.FirstOrDefault(u => (u.nombreUsuario == credencialNombreEmail || u.email == credencialNombreEmail) && u.contraseña == contraseña);

            return usuario != null;
        }
    }
}