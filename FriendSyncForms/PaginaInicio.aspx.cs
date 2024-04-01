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
        protected void Page_Load(object sender, EventArgs e)
        {
            MostrarPublicacionesUsuario();
            // Verificar si se recibió un ID de usuario desde la página de inicio de sesión
            if (!IsPostBack && Request.QueryString["userId"] != null)
            {
                // Obtener el ID de usuario de la URL
                int userId = Convert.ToInt32(Request.QueryString["userId"]);

                // Cargar la información del usuario utilizando el ID
                CargarInformacionUsuario(userId);

                // Mostrar las publicaciones del usuario
                

            }

        }
        private void MostrarPublicacionesUsuario()
        {
            int userId = Convert.ToInt32(Request.QueryString["userId"]);

            // Aquí obtienes las publicaciones del usuario utilizando Entity Framework
            using (FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities contexto = new FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities())
            {
                var publicaciones = contexto.Publicaciones.ToList();

                // Obtener la lista de amigos del usuario de la sesión actual
                var amigosUsuario = contexto.Amigos.Where(a => a.UsuarioId1 == userId || a.UsuarioId2 == userId).ToList();

                // Iterar sobre las publicaciones y crear elementos HTML para mostrarlas
                foreach (var publicacion in publicaciones)
                {
                    // Crear un div para cada publicación con sus detalles
                    var divPublicacion = new HtmlGenericControl("div");
                    divPublicacion.Attributes.Add("class", "card mb-3 publicard");

                    // Obtener información del usuario específico de la publicación
                    var usuario = contexto.users.FirstOrDefault(u => u.IdUsuario == publicacion.IdUsuario);

                    // Crear un div para contener la foto de perfil y el nombre de usuario
                    var divUsuario = new HtmlGenericControl("div");
                    divUsuario.Attributes.Add("class", "usuario divusuario");

                    // Crear una imagen para la foto de perfil del usuario
                    var fotoPerfilUsuario = new HtmlGenericControl("img");
                    fotoPerfilUsuario.Attributes.Add("class", "foto-perfil-usuario fotoperfilpubli");
                    fotoPerfilUsuario.Attributes.Add("src", "data:image/jpeg;base64," + Convert.ToBase64String(usuario.fotoPerfil));

                    // Crear un párrafo para el nombre de usuario
                    var nombreUsuario = new HtmlGenericControl("p");
                    nombreUsuario.Attributes.Add("class", "nombre-usuario nombreusuario");
                    nombreUsuario.InnerText = usuario.nombreUsuario;

                    // Agregar la foto de perfil y el nombre de usuario al div del usuario
                    divUsuario.Controls.Add(fotoPerfilUsuario);
                    divUsuario.Controls.Add(nombreUsuario);

                    // Agregar el div del usuario al div de la publicación
                    divPublicacion.Controls.Add(divUsuario);

                    // Crear una imagen para la foto de la publicación
                    var fotoPublicacion = new HtmlGenericControl("img");
                    fotoPublicacion.Attributes.Add("class", "foto-publicacion imagenpubli");
                    fotoPublicacion.Attributes.Add("src", "data:image/jpeg;base64," + Convert.ToBase64String(publicacion.Imagen));

                    // Crear un párrafo para el contenido de la publicación
                    var contenido = new HtmlGenericControl("p");
                    contenido.Attributes.Add("class", "card-text");
                    contenido.InnerText = publicacion.Contenido;

                    // Crear un párrafo para la fecha de publicación
                    var fecha = new HtmlGenericControl("p");
                    fecha.Attributes.Add("class", "card-text nombreusuario");
                    fecha.InnerHtml = "<small class='text-muted'>" + publicacion.FechaPublicacion.ToString() + "</small>";

                    // Agregar la foto de la publicación, el contenido y la fecha al div de la publicación
                    divPublicacion.Controls.Add(fotoPublicacion);
                    divPublicacion.Controls.Add(contenido);
                    divPublicacion.Controls.Add(fecha);

                    // Verificar si el usuario que realizó la publicación es amigo del usuario de la sesión actual
                    bool sonAmigos = amigosUsuario.Any(a => a.UsuarioId1 == publicacion.IdUsuario || a.UsuarioId2 == publicacion.IdUsuario);

                    // Crear la caja de comentarios
                    var divComentarios = new HtmlGenericControl("div");
                    var tituloComentarios = new HtmlGenericControl("h4");
                    tituloComentarios.InnerText = "Comentarios";
                    divComentarios.Attributes.Add("class", "comentarios");
                    divComentarios.Controls.Add(tituloComentarios);

                    // Obtener los comentarios de la publicación
                    var comentarios = contexto.Comentarios.Where(c => c.IdPublicacion == publicacion.IdPublicacion).ToList();

                    // Iterar sobre los comentarios y agregarlos a la caja de comentarios
                    foreach (var comentario in comentarios)
                    {
                        // Crear un div para cada comentario
                        var divComentario = new HtmlGenericControl("div");
                        divComentario.Attributes.Add("class", "comentario");

                        // Obtener información del usuario que hizo el comentario
                        var usuarioComentario = contexto.users.FirstOrDefault(u => u.IdUsuario == comentario.IdUsuario);

                        // Crear una imagen para la foto de perfil del usuario que hizo el comentario
                        var fotoPerfilComentario = new HtmlGenericControl("img");
                        fotoPerfilComentario.Attributes.Add("class", "foto-perfil-comentario");
                        fotoPerfilComentario.Attributes.Add("src", "data:image/jpeg;base64," + Convert.ToBase64String(usuarioComentario.fotoPerfil));

                        // Crear un párrafo para el nombre de usuario que hizo el comentario
                        var nombreUsuarioComentario = new HtmlGenericControl("p");
                        nombreUsuarioComentario.Attributes.Add("class", "nombre-usuario-comentario");
                        nombreUsuarioComentario.InnerText = usuarioComentario.nombreUsuario;

                        // Crear un párrafo para la fecha de comentario
                        var fechaComentario = new HtmlGenericControl("p");
                        fechaComentario.Attributes.Add("class", "fecha-comentario");
                        fechaComentario.InnerHtml = "<small class='text-muted'>" + comentario.FechaComentario.ToString() + "</small>";

                        // Crear un párrafo para el contenido del comentario
                        var contenidoComentario = new HtmlGenericControl("p");
                        contenidoComentario.Attributes.Add("class", "contenido-comentario");
                        contenidoComentario.InnerText = comentario.Contenido;

                        // Agregar la foto de perfil, el nombre de usuario, la fecha y el contenido al div del comentario
                        divComentario.Controls.Add(fotoPerfilComentario);
                        divComentario.Controls.Add(nombreUsuarioComentario);
                        divComentario.Controls.Add(fechaComentario);
                        divComentario.Controls.Add(contenidoComentario);

                        // Agregar el div del comentario a la caja de comentarios
                        divComentarios.Controls.Add(divComentario);

                    }
                    var divTextBoxEnviar = new HtmlGenericControl("div");
                    divTextBoxEnviar.Attributes.Add("class", "div-textbox-enviar");
                    var txtComentario = new TextBox();
                    // Si no son amigos, inhabilitar el cuadro de texto para comentarios
                    if (!sonAmigos)
                    {
                        txtComentario.Enabled = false;
                    }

                    // Crear un textbox para ingresar comentarios
                    
                    txtComentario.ID = "txtComentario_" + publicacion.IdPublicacion; // Asignar un ID único para identificarlo posteriormente
                    txtComentario.CssClass = "form-control inputcomentario";
                    txtComentario.Attributes.Add("placeholder", "Escribe un comentario...");
                    txtComentario.Enabled = sonAmigos; // Habilitar el textbox solo si son amigos

                    // Crear un botón para enviar el comentario
                    var btnEnviarComentario = new HtmlButton();
                    btnEnviarComentario.ID = "btnEnviarComentario_" + publicacion.IdPublicacion; // Asignar un ID único para identificarlo posteriormente
                    btnEnviarComentario.InnerText = "Enviar";
                    btnEnviarComentario.Attributes["class"] = "btn btn-primary";
                    btnEnviarComentario.ServerClick += EnviarComentario_Click; // Asignar el evento Click para manejar el envío del comentario
                    btnEnviarComentario.Disabled = !sonAmigos; // Inhabilitar el botón si no son amigos

                    // Agregar el textbox y el botón al div de comentarios
                    divTextBoxEnviar.Controls.Add(txtComentario);
                    divTextBoxEnviar.Controls.Add(btnEnviarComentario);

                    // Agregar la caja de comentarios al div de la publicación
                    divPublicacion.Controls.Add(divComentarios);
                    divPublicacion.Controls.Add(divTextBoxEnviar);

                    // Agregar el div de la publicación al contenedor de publicaciones
                    contenedorPublicaciones.Controls.Add(divPublicacion);
                }
            }
        }



        protected void EnviarComentario_Click(object sender, EventArgs e)
        {
            HtmlButton btnEnviarComentario = (HtmlButton)sender;
            int publicacionId = int.Parse(btnEnviarComentario.ID.Split('_')[1]); // Obtener el ID de la publicación desde el ID del botón

            // Obtener el ID del usuario de la sesión actual
            int userId = Convert.ToInt32(Request.QueryString["userId"]);

            // Obtener el textbox correspondiente al comentario
            TextBox txtComentario = (TextBox)contenedorPublicaciones.FindControl("txtComentario_" + publicacionId);

            if (txtComentario != null)
            {
                // Obtener el contenido del comentario
                string contenidoComentario = txtComentario.Text.Trim();

                if (!string.IsNullOrEmpty(contenidoComentario))
                {
                    // Crear una nueva instancia de comentario
                    Comentarios nuevoComentario = new Comentarios
                    {
                        IdPublicacion = publicacionId,
                        IdUsuario = userId,
                        Contenido = contenidoComentario,
                        FechaComentario = DateTime.Now
                    };

                    // Guardar el comentario en la base de datos
                    using (FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities contexto = new FriendSyncDB.ModeloFriendSync.FriendSyncBDEntities())
                    {
                        contexto.Comentarios.Add(nuevoComentario);
                        contexto.SaveChanges();
                    }

                    // Limpiar el contenido del textbox después de enviar el comentario
                    txtComentario.Text = "";

                    // Puedes agregar aquí cualquier lógica adicional, como mostrar un mensaje de éxito o recargar la página para actualizar los comentarios, etc.
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    // El contenido del comentario está vacío
                    // Puedes mostrar un mensaje de error al usuario
                    // o realizar alguna otra acción apropiada, como resaltar el textbox o deshabilitar el botón de enviar.

                    // Por ejemplo, puedes mostrar un mensaje de error en una etiqueta de texto:
                    Label lblMensajeError = new Label();
                    lblMensajeError.ID = "lblMensajeError";
                    lblMensajeError.Text = "Por favor, escribe un comentario antes de enviar.";
                    lblMensajeError.CssClass = "mensaje-error"; // Clase CSS opcional para dar estilo al mensaje de error

                    // Agregar el control Label al contenedor adecuado (por ejemplo, el formulario o un panel)
                    // En este ejemplo, se agrega al panel pnlMensajeError
                    contenedorPublicaciones.Controls.Add(lblMensajeError);

                    // También puedes enfocar el textbox para que el usuario pueda ingresar el comentario directamente
                    txtComentario.Focus();
                }
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
            Response.Redirect($"InfoUsuario.aspx?userId={Request.QueryString["userId"]}");
        }protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect($"SeccionAmigos.aspx?userId={Request.QueryString["userId"]}");
        }
        protected void btnCrearPublicacion_Click(object sender, EventArgs e)
        {
            string descripcion = txtDescripcion.Text;
            HttpPostedFile archivo = FileUploadPubli.PostedFile;
            if (archivo != null && archivo.ContentLength > 0)
            {
                int longitud = archivo.ContentLength;
                byte[] bytesImagenPubli = new byte[longitud];
                archivo.InputStream.Read(bytesImagenPubli, 0, longitud);

                int userId;
                if (Request.QueryString["userId"] != null && int.TryParse(Request.QueryString["userId"], out userId))
                {
                    using (var db = new FriendSyncBDEntities())
                    {
                        Publicaciones nuevaPublicacion = new Publicaciones
                        {
                            IdUsuario = userId,
                            Contenido = descripcion,
                            Imagen = bytesImagenPubli,
                            FechaPublicacion = DateTime.Now
                        };

                        db.Publicaciones.Add(nuevaPublicacion);
                        db.SaveChanges();
                    }
                }
                else
                {
                    Response.Write("ID de usuario no válido");
                }
            }
            txtDescripcion.Text = "";
            Response.Redirect(Request.RawUrl);
        }

    }
}