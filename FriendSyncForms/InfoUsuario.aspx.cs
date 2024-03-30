﻿using FriendSyncDB;
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
                            var infousuarioDto = new InfoUsuarioDto 
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

            if (!IsPostBack && Request.QueryString["userId"] != null)
            {
                int userId = Convert.ToInt32(Request.QueryString["userId"]);

                CargarInformacionUsuario(userId);
            }
        }

        private void CargarInformacionUsuario(int userId)
        {
            using (FriendSyncBDEntities db = new FriendSyncBDEntities())
            {
                users usuario = db.users.Find(userId);

                if (usuario != null)
                {
                    Page.Title = $"{usuario.nombreUsuario}'s Pagina de Inicio";

                    if (usuario.fotoPerfil != null)
                    {
                        string base64String = Convert.ToBase64String(usuario.fotoPerfil);
                        string imageUrl = $"data:image/jpeg;base64,{base64String}";

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
                user.contraseña = txtContraseña.Text;
                user.fechaNac = DateTime.Parse(txtFechaNacimiento.Text);
                
                if (FileUpload1.HasFile)
                {
                    try
                    {
                        HttpPostedFile archivo = FileUpload1.PostedFile;
                        int longitud = archivo.ContentLength;
                        byte[] bytesImagen = new byte[longitud];
                        archivo.InputStream.Read(bytesImagen, 0, longitud);

                        user.fotoPerfil = bytesImagen;
                        Response.Write("<script>Alert(En el proximo inicio de sesion se veran reflejados los cambios);</script>");
                        string base64String = Convert.ToBase64String(user.fotoPerfil);
                        string imageUrl = $"data:image/jpeg;base64,{base64String}";
                        userImage.Src = imageUrl;
                        navUsuarioFoto.Src = imageUrl;
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>Alert(Error al procesar el archivo cargado: " + ex.Message+");</script>");
                    }
                }
                    db.SaveChanges();

            }
            else
            {
                Label1.Text = "No se pudo actualizar el usuario";
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
            {
            int idUsuario = int.Parse(txtIdUsuario.Text);

            var usuario = db.users.Find(idUsuario);

            if (usuario != null)
            {
                db.users.Remove(usuario);
                db.SaveChanges();
                    
                Response.Redirect($"login.aspx");
            }
            else
            {
                Response.Write("No se encontró ningún usuario con el ID especificado.");
            }
        }
        protected void Button5_Click(object sender, EventArgs e)
            {
            Response.Redirect($"login.aspx");
        }

        



        }
}