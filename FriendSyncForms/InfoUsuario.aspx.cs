using FriendSyncDB;
using FriendSyncDB.ModeloFriendSync;
using FriendSyncForms.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FriendSyncForms
{
    public partial class InfoUsuario : System.Web.UI.Page
    {

        FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities db = new FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities();   
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["userId"] != null)
                {
                    int userId = Convert.ToInt32(Request.QueryString["userId"]);

                    using (var db = new FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities())
                    {
                        var usuario = db.users.FirstOrDefault(u => u.IdUsuario == userId);

                        if (usuario != null)
                        {
                            var infousuarioDto = new InfoUsuarioDto //
                            {
                                IdUsuario = usuario.IdUsuario,
                                NombreUsuario = usuario.nombreUsuario,
                                Email = usuario.email,
                                Contraseña = usuario.contraseña,
                                NombreCompleto = usuario.nombreCompleto,
                                FechaNacimiento = usuario.fechaNac,
                                FotoPerfilBase64 = Convert.ToBase64String(usuario.fotoPerfil)
                            };

                            txtIdUsuario.Text = infousuarioDto.IdUsuario.ToString();
                            txtNombreUsuario.Text = infousuarioDto.NombreUsuario;
                            txtEmail.Text = infousuarioDto.Email;
                            txtContraseña.Text = infousuarioDto.Contraseña;
                            txtNombreCompleto.Text = infousuarioDto.NombreCompleto;
                            txtFechaNacimiento.Text = infousuarioDto.FechaNacimiento?.ToString("dd/MM/yyyy");
                            userImage.Src = $"data:image/jpeg;base64,{infousuarioDto.FotoPerfilBase64}";
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
                Response.Redirect($"PaginaInicio.aspx?userId={Request.QueryString["userId"]}");
            }
        protected void Button2_Click(object sender, EventArgs e)
            {
                Response.Redirect($"SeccionAmigos.aspx?userId={Request.QueryString["userId"]}");
            }
        protected void Button3_Click(object sender, EventArgs e)
            {
            var user = db.users.Find(int.Parse(txtIdUsuario.Text));
            if (user != null)
            {
                user.nombreUsuario = txtNombreUsuario.Text;
                user.email = txtEmail.Text ;
                user.nombreCompleto = txtNombreCompleto.Text;
                user.fechaNac = DateTime.Parse(txtFechaNacimiento.Text);
                db.SaveChanges();

            }
            else
            {
                Label1.Text = "No se pudo actualizar el usuario";
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
            {
            }
        protected void Button5_Click(object sender, EventArgs e)
            {
            Response.Redirect($"login.aspx");
        }

        



        }
}