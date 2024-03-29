using FriendSyncDB.ModeloFriendSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FriendSyncForms
{
    public partial class PaginaRegistro : System.Web.UI.Page
    {
        FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities db = new FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            FriendSyncDB.ModeloFriendSync.users usuario = new FriendSyncDB.ModeloFriendSync.users();

            usuario.nombreUsuario = TextBox2.Text;
            usuario.email = TextBox3.Text;
            usuario.contraseña = TextBox4.Text;
            usuario.nombreCompleto = TextBox5.Text;
            try
            {
                DateTime fechaNacimiento = DateTime.ParseExact(TextBox6.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                string fechaFormateada = fechaNacimiento.ToString("dd/mm/yyyy");

                usuario.fechaNac = DateTime.ParseExact(fechaFormateada, "dd/mm/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                
            }
            catch (FormatException ex)
            {
                Console.WriteLine("formato de fecha mal: " + ex.Message);
            }
            if (FileUpload1.HasFile)
            {
                try
                {
                    HttpPostedFile archivo = FileUpload1.PostedFile;
                    int longitud = archivo.ContentLength;
                    byte[] bytesImagen = new byte[longitud];
                    archivo.InputStream.Read(bytesImagen, 0, longitud);

                    usuario.fotoPerfil = bytesImagen;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al procesar el archivo cargado: " + ex.Message);
                }
            }


            try
            {
                db.users.Add(usuario);
                db.SaveChanges();
                Label10.Text = "El usuario se creó satisfactoriamente.";
                Response.Redirect($"login.aspx");
            }
            catch (Exception ex)
            {
                Label10.Text = "Error al crear el usuario: ";
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    Label10.Text += innerException.Message;
                    innerException = innerException.InnerException;
                    if (innerException != null)
                    {
                        Label10.Text += " -> ";
                    }
                }
            }


        }
    }
}