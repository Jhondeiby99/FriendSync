using FriendSyncDB.ModeloFriendSync;
using FriendSyncForms.DTOS;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace FriendSyncForms
{
    public partial class SeccionAmigos : System.Web.UI.Page
    {
        FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities db = new FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarTarjetasUsuarios();
            MostrarSolicitudesNuevas();
            ObtenerAmigosUsuarioEnSesion();

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
            CargarTarjetasUsuarios();
        }

        private void CargarTarjetasUsuarios()
        {
            try
            {
                string searchTerm = ((TextBox)form1.FindControl("TextBox1")).Text;

                // Obtener el ID del usuario en sesión
                int userId = int.Parse(Request.QueryString["userId"]);

                // Filtrar los usuarios encontrados para excluir al usuario en sesión
                var usuariosEncontrados = db.users
                    .Where(user => (user.IdUsuario != userId) &&
                                   (user.nombreCompleto.Contains(searchTerm) || user.email.Contains(searchTerm)))
                    .ToList();

                phAmigos.Controls.Clear();

                foreach (var usuario in usuariosEncontrados)
                {
                    int userIdReceptor = usuario.IdUsuario;

                    HtmlGenericControl cardDiv = new HtmlGenericControl("div");
                    cardDiv.Attributes["class"] = "card amigosCard";

                    Image img = new Image();
                    img.CssClass = "card-img-top imgCardUser";
                    if (usuario.fotoPerfil != null && usuario.fotoPerfil.Length > 0)
                    {
                        string base64String = "data:image/png;base64," + Convert.ToBase64String(usuario.fotoPerfil, 0, usuario.fotoPerfil.Length);
                        img.ImageUrl = base64String;
                    }
                    else
                    {
                        img.ImageUrl = "../Media/imagen1.jpg";
                    }

                    HtmlGenericControl cardBody = new HtmlGenericControl("div");
                    cardBody.Attributes["class"] = "card-body";

                    Label lblTitle = new Label();
                    lblTitle.Text = "<b>Usuario: </b>" + usuario.nombreCompleto + "<br></br>";
                    lblTitle.CssClass = "card-title";

                    Label lblText = new Label();
                    lblText.Text = "<b>Email:  </b>" + usuario.email;
                    lblText.CssClass = "card-text";

                    HtmlButton btnEnviarSolicitud = new HtmlButton();
                    btnEnviarSolicitud.ID = "btnEnviarSolicitud_" + userIdReceptor;
                    btnEnviarSolicitud.InnerText = "Enviar Solicitud";
                    btnEnviarSolicitud.Attributes["class"] = "btn btn-primary btnEnviarSoli";
                    btnEnviarSolicitud.ServerClick += EnviarSolicitud_Click;
                    btnEnviarSolicitud.Attributes["onclick"] = "event.preventDefault();";
                    btnEnviarSolicitud.Attributes["data-userIdReceptor"] = userIdReceptor.ToString();

                    cardBody.Controls.Add(img);
                    cardBody.Controls.Add(lblTitle);
                    cardBody.Controls.Add(lblText);
                    cardBody.Controls.Add(btnEnviarSolicitud);

                    cardDiv.Controls.Add(cardBody);
                    phAmigos.Controls.Add(cardDiv);
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Error al cargar las tarjetas de usuarios: {ex.Message}");
            }
        }


        protected void EnviarSolicitud_Click(object sender, EventArgs e)
        {
            HtmlButton btnEnviarSolicitud = (HtmlButton)sender;
            int userIdReceptor = int.Parse(btnEnviarSolicitud.Attributes["data-userIdReceptor"]);
            int userId = int.Parse(Request.QueryString["userId"]);

            using (FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities contexto = new FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities())
            {
                try
                {
                    
                    var solicitudExistente = contexto.SolicitudesAmistad.FirstOrDefault(s =>
                        (s.IdUsuarioEmisor == userId && s.IdUsuarioReceptor == userIdReceptor) ||
                        (s.IdUsuarioEmisor == userIdReceptor && s.IdUsuarioReceptor == userId) &&
                        (s.Estado.Equals("Pendiente") || s.Estado.Equals("Aceptada")));

                    if (solicitudExistente == null)
                    {
                        
                        SolicitudesAmistad nuevaSolicitud = new SolicitudesAmistad
                        {
                            IdUsuarioEmisor = userId,
                            IdUsuarioReceptor = userIdReceptor,
                            FechaSolicitud = DateTime.Now,
                            Estado = "Pendiente" 
                        };

                        contexto.SolicitudesAmistad.Add(nuevaSolicitud);
                        contexto.SaveChanges();
                        respuestaSolicitud.Text = "Solicitud de amistad enviada correctamente.";
                        Response.Write("<script>alert('Solicitud de amistad enviada correctamente.');</script>");
                    }
                    else if (solicitudExistente.Estado.Equals("Rechazada"))
                    {
                        
                        solicitudExistente.Estado = "Pendiente";
                        contexto.SaveChanges();
                        respuestaSolicitud.Text = "Solicitud de amistad enviada correctamente.";
                        Response.Write("<script>alert('Solicitud de amistad enviada correctamente.');</script>");
                    }
                    else
                    {
                        respuestaSolicitud.Text = "Ya hay una solicitud de amistad pendiente o aceptada con este usuario.";
                        Response.Write("<script>alert('Ya hay una solicitud de amistad pendiente o aceptada con este usuario.');</script>");
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var error in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in error.ValidationErrors)
                        {
                            Response.Write($"Error de validación en la entidad {error.Entry.Entity.GetType().Name}: ");
                            Response.Write($"Propiedad: {validationError.PropertyName}, Error: {validationError.ErrorMessage}<br>");
                        }
                    }
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect($"InfoUsuario.aspx?userId={Request.QueryString["userId"]}");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect($"PaginaInicio.aspx?userId={Request.QueryString["userId"]}");
        }

        protected void MostrarSolicitudesNuevas()
        {
            int userId = Convert.ToInt32(Request.QueryString["userId"]);
            List<SolicitudesAmistad> solicitudes = new List<SolicitudesAmistad>();

            panelSolicitudesNuevas.Controls.Clear();

            using (var contexto = new FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities())
            {
                solicitudes = contexto.SolicitudesAmistad
                    .Where(s => s.IdUsuarioReceptor == userId && !s.Estado.Equals("Aceptada") && !s.Estado.Equals("Rechazada"))
                    .ToList();

                foreach (var solicitud in solicitudes)
                {
                    var usuarioEmisor = contexto.users.FirstOrDefault(u => u.IdUsuario == solicitud.IdUsuarioEmisor);

                    if (usuarioEmisor != null)
                    {
                        string nombreUsuarioEmisor = usuarioEmisor.nombreUsuario;

                        HtmlGenericControl divCard = new HtmlGenericControl("div");
                        divCard.Attributes["class"] = "card";

                        HtmlGenericControl divCardBody = new HtmlGenericControl("div");
                        divCardBody.Attributes["class"] = "card-body";

                        Image imgFotoPerfil = new Image();
                        imgFotoPerfil.CssClass = "card-img-top imgSolicituA";
                        if (usuarioEmisor.fotoPerfil != null && usuarioEmisor.fotoPerfil.Length > 0)
                        {
                            string base64String = "data:image/png;base64," + Convert.ToBase64String(usuarioEmisor.fotoPerfil, 0, usuarioEmisor.fotoPerfil.Length);
                            imgFotoPerfil.ImageUrl = base64String;
                        }
                        else
                        {
                            imgFotoPerfil.ImageUrl = "../Media/imagen1.jpg"; 
                        }
                        divCardBody.Controls.Add(imgFotoPerfil);

                        Label lblTitulo = new Label();
                        lblTitulo.CssClass = "card-title pSoli";
                        lblTitulo.Text = $"Solicitud de {nombreUsuarioEmisor}";
                        divCardBody.Controls.Add(lblTitulo);

                        Label lblFecha = new Label();
                        lblFecha.CssClass = "card-text";
                        lblFecha.Text = solicitud.FechaSolicitud.ToString();
                        divCardBody.Controls.Add(lblFecha);

                        HtmlButton btnAceptar = new HtmlButton();
                        btnAceptar.Attributes["class"] = "btn btn-primary botonesSoli";
                        btnAceptar.InnerText = "Aceptar";
                        btnAceptar.ServerClick += (sender, e) => { AceptarSolicitud(solicitud.Id); };
                        divCardBody.Controls.Add(btnAceptar);

                        HtmlButton btnRechazar = new HtmlButton();
                        btnRechazar.Attributes["class"] = "btn btn-danger botonesSoli";
                        btnRechazar.InnerText = "Rechazar";
                        btnRechazar.ServerClick += (sender, e) => { AceptarSolicitud(solicitud.Id); };
                        divCardBody.Controls.Add(btnRechazar);

                        divCard.Controls.Add(divCardBody);
                        panelSolicitudesNuevas.Controls.Add(divCard);
                    }
                }
            }
        }

        protected void AceptarSolicitud(int solicitudId)
        {
            using (var contexto = new FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities())
            {
                try
                {
                    var solicitud = contexto.SolicitudesAmistad.FirstOrDefault(s => s.Id == solicitudId);

                    if (solicitud != null)
                    {
                        
                        solicitud.Estado = "Aceptada";
                        solicitud.FechaRespuesta = DateTime.Now;

                        
                        Amigos nuevoAmigo = new Amigos
                        {
                            UsuarioId1 = solicitud.IdUsuarioEmisor,
                            UsuarioId2 = solicitud.IdUsuarioReceptor,
                            Estado = "Activo",
                            FechaAceptacion = DateTime.Now 
                        };

                        
                        contexto.Amigos.Add(nuevoAmigo);

                       
                        contexto.SaveChanges();

                        Response.Redirect(Request.RawUrl);
                        Response.Write("<script>alert('La solicitud de amistad ha sido aceptada.');</script>");
                    }
                }
                catch (Exception ex)
                {
                    
                    Response.Write($"<script>alert('Error al aceptar la solicitud de amistad: {ex.Message}');</script>");
                }
            }
        }


        protected void RechazarSolicitud(int solicitudId)
        {
            try
            {
                using (var contexto = new FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities())
                {
                    
                    var solicitud = contexto.SolicitudesAmistad.FirstOrDefault(s => s.Id == solicitudId);

                    if (solicitud != null)
                    {
                        
                            solicitud.Estado = "Rechazada";
                            solicitud.FechaRespuesta = DateTime.Now;

                       
                        contexto.SaveChanges();
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
            catch (Exception ex)
            {
               
                Response.Write($"Error al rechazar la solicitud de amistad: {ex.Message}");
            }
        }

        private void ObtenerAmigosUsuarioEnSesion()
        {
            using (var contexto = new FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities())
            {
                int userId = Convert.ToInt32(Request.QueryString["userId"]);

                var amigos = (from amigo in contexto.Amigos
                              join usuarioEmisor in contexto.users on amigo.UsuarioId1 equals usuarioEmisor.IdUsuario
                              join usuarioReceptor in contexto.users on amigo.UsuarioId2 equals usuarioReceptor.IdUsuario
                              where usuarioEmisor.IdUsuario == userId || usuarioReceptor.IdUsuario == userId
                              select new AmigosDto
                              {
                                  NombreUsuario = usuarioEmisor.IdUsuario == userId ? usuarioReceptor.nombreUsuario : usuarioEmisor.nombreUsuario,
                                  FotoPerfil = usuarioEmisor.IdUsuario == userId ? usuarioReceptor.fotoPerfil : usuarioEmisor.fotoPerfil,
                                  AmigoId = (int)(usuarioEmisor.IdUsuario == userId ? amigo.UsuarioId2 : amigo.UsuarioId1)
                              }).ToList();


                divListaAmigos.Controls.Clear();

                
                foreach (var amigo in amigos)
                {
                    HtmlGenericControl amigoDiv = new HtmlGenericControl("div");
                    amigoDiv.Attributes["class"] = "amigo";

                    // Agregar la imagen de perfil
                    Image img = new Image();
                    img.CssClass = "foto-perfil fotoperfilAmigo";
                    if (amigo.FotoPerfil != null && amigo.FotoPerfil.Length > 0)
                    {
                        string base64String = "data:image/png;base64," + Convert.ToBase64String(amigo.FotoPerfil, 0, amigo.FotoPerfil.Length);
                        img.ImageUrl = base64String;
                    }
                    else
                    {
                        img.ImageUrl = "../Media/imagen1.jpg";
                    }

                    // Agregar el nombre de usuario
                    HtmlGenericControl nombreUsuarioSpan = new HtmlGenericControl("span");
                    nombreUsuarioSpan.InnerHtml = amigo.NombreUsuario;
                    nombreUsuarioSpan.Attributes["class"]="pNombreUAmigo";

                    // Agregar el botón de eliminar amigo
                    HtmlButton btnEliminarAmigo = new HtmlButton();
                    btnEliminarAmigo.Attributes["class"] = "btn btn-danger botonEliminarAmigo";
                    btnEliminarAmigo.InnerText = "Eliminar amigo";
                    btnEliminarAmigo.Attributes["data-amigoId"] = amigo.AmigoId.ToString();
                    btnEliminarAmigo.ServerClick += BtnEliminarAmigo_Click;

                    // Agregar los controles al div del amigo
                    amigoDiv.Controls.Add(img);
                    amigoDiv.Controls.Add(nombreUsuarioSpan);
                    amigoDiv.Controls.Add(btnEliminarAmigo);

                    // Agregar el div del amigo al div contenedor
                    divListaAmigos.Controls.Add(amigoDiv);
                }
            }
        }
        protected void BtnEliminarAmigo_Click(object sender, EventArgs e)
        {
            var btnEliminarAmigo = (HtmlButton)sender;
            int userId = int.Parse(Request.QueryString["userId"]);
            int amigoId = int.Parse(btnEliminarAmigo.Attributes["data-amigoId"]);

            using (var contexto = new FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities())
            {
                try
                {
                    // Buscar la solicitud de amistad tanto para (userId, amigoId) como para (amigoId, userId)
                    var solicitud1 = contexto.SolicitudesAmistad.FirstOrDefault(s =>
                        s.IdUsuarioEmisor == userId && s.IdUsuarioReceptor == amigoId);

                    var solicitud2 = contexto.SolicitudesAmistad.FirstOrDefault(s =>
                        s.IdUsuarioEmisor == amigoId && s.IdUsuarioReceptor == userId);

                    // Cambiar el estado de la primera solicitud de amistad
                    if (solicitud1 != null)
                    {
                        solicitud1.Estado = "Rechazada";
                    }

                    // Cambiar el estado de la segunda solicitud de amistad
                    if (solicitud2 != null)
                    {
                        solicitud2.Estado = "Rechazada";
                    }

                    // Eliminar la relación de amistad
                    var amigo = contexto.Amigos.FirstOrDefault(a =>
                        (a.UsuarioId1 == userId && a.UsuarioId2 == amigoId) ||
                        (a.UsuarioId1 == amigoId && a.UsuarioId2 == userId));

                    if (amigo != null)
                    {
                        contexto.Amigos.Remove(amigo);
                    }

                    // Obtener el valor máximo actual del identity
                    var maxId = contexto.Amigos.Max(a => a.Id);

                    // Reiniciar el identity al valor máximo anterior
                    contexto.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('Amigos', RESEED, {maxId - 1})");

                    // Guardar los cambios en la base de datos
                    contexto.SaveChanges();

                    // Redirigir a la misma página
                    Response.Redirect(Request.RawUrl);
                    respuestaSolicitud.Text = "Amigo eliminado correctamente.";
                }
                catch (Exception ex)
                {
                    respuestaSolicitud.Text = "Error al eliminar el amigo: " + ex.Message;
                }
            }
        }






    }
}
