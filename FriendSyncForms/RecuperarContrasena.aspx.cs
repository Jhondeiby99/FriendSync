using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FriendSyncForms.DTOS;

namespace FriendSyncForms
{
    public partial class RecuperarContrasena : System.Web.UI.Page
    {
        FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities db = new FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            restablecer.Visible = false;
            Textboxestablecer.Visible = false;
            Label2.Visible = false;
            TextboxConfirmar.Visible = false;


        }

        protected void BtnRecuperarContraseña_Click(object sender, EventArgs e)

        {



            string correoElectronico = txtEmail.Text;

            DateTime fechaNacimiento = DateTime.ParseExact(TextBox6.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);



            RestablecerContraseñaDto usuario = (from u in db.users
                                                where u.email == correoElectronico && u.fechaNac == fechaNacimiento
                                                select new RestablecerContraseñaDto
                                                {
                                                    CorreoElectronico = u.email,
                                                    FechaNacimiento = (DateTime)u.fechaNac,
                                                    Contrasena = u.contraseña
                                                }).FirstOrDefault();
            if (usuario != null && usuario.FechaNacimiento.Date == fechaNacimiento.Date)
            {
                Textboxestablecer.Visible = true;
                TextboxConfirmar.Visible = true;
                restablecer.Visible = true;
            }
            else
            {

                Label2.Text = "No se encontró el usuario o la fecha de nacimiento no coincide";
            }




        }
        protected void RstablecerContraseña(object sender, EventArgs e)
        {
            string correoElectronico = txtEmail.Text;
            string nuevaContraseña = Textboxestablecer.Text;
            string confirmarContraseña = TextboxConfirmar.Text;

            // Verificar si las contraseñas coinciden
            if (nuevaContraseña != confirmarContraseña)
            {
                Label2.Visible = true;
                Label2.Text = "Las contraseñas no coinciden.";
                return;
            }

            DateTime fechaNacimiento = DateTime.ParseExact(TextBox6.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var usuario = (from u in db.users
                           where u.email == correoElectronico && u.fechaNac == fechaNacimiento
                           select u).FirstOrDefault();

            if (usuario != null)
            {
                usuario.contraseña = nuevaContraseña;
                db.SaveChanges();
            }

            Response.Redirect($"login.aspx");

        }
    }
}