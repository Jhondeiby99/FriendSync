<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaginaInicio.aspx.cs" Inherits="FriendSyncForms.PaginaInicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="icon" href="../Media/LogoFriendSync.png" type="image/x-icon">
    <link rel="stylesheet" href="../CSS/EstilosInicio.css" />
    <link rel="stylesheet" href="../CSS/EstilosNavbar.css" />
    <title>Inicio de FriendSync</title>
</head>
<body>
    <form id="form2" runat="server">
        <nav class="navbar navbar-expand-lg navbar-light bg-light barranav">
            <div class="container">
                <img class="logonav" src="../Media/LogoFriendSync.png" alt="Icono" />
                <a class="navbar-brand tituloNavInicio" href="">FriendSync</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav"
                    aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="listanav navbar-nav ml-auto">
                        <li class="libarranav">
                            <a class="nav-link" href="">
                                <svg viewBox="0 0 24 24" width="24" height="24" fill="currentColor" class="x19dipnz x1lliihq x1k90msu x2h7rmj x1qfuztq"
                                    style="color: #570ea5">
                                    <path d="M9.464 1.286C10.294.803 11.092.5 12 .5c.908 0 1.707.303 2.537.786.795.462 1.7 1.142 2.815 1.977l2.232 1.675c1.391 1.042 2.359 1.766 2.888 2.826.53 1.059.53 2.268.528 4.006v4.3c0 1.355 0 2.471-.119 3.355-.124.928-.396 1.747-1.052 2.403-.657.657-1.476.928-2.404 1.053-.884.119-2 .119-3.354.119H7.93c-1.354 0-2.471 0-3.355-.119-.928-.125-1.747-.396-2.403-1.053-.656-.656-.928-1.475-1.053-2.403C1 18.541 1 17.425 1 16.07v-4.3c0-1.738-.002-2.947.528-4.006.53-1.06 1.497-1.784 2.888-2.826L6.65 3.263c1.114-.835 2.02-1.515 2.815-1.977zM10.5 13A1.5 1.5 0 0 0 9 14.5V21h6v-6.5a1.5 1.5 0 0 0-1.5-1.5h-3z">
                                    </path>
                                </svg>
                            </a>
                        </li>
                        <li class="libarranav">
                            <button class="nav-link" id="Button2" runat="server" onserverclick="Button2_Click">
                                <svg viewBox="0 0 24 24" width="24" height="24" fill="currentColor" class="x19dipnz x1lliihq x1k90msu x2h7rmj x1qfuztq"
                                    style="color: #570ea5">
                                    <path d="M8 2.5a4.5 4.5 0 1 0 0 9 4.5 4.5 0 0 0 0-9zM5.5 7a2.5 2.5 0 1 1 5 0 2.5 2.5 0 0 1-5 0zm-.25 6A4.75 4.75 0 0 0 .5 17.75 3.25 3.25 0 0 0 3.75 21h8.5a3.25 3.25 0 0 0 3.25-3.25A4.75 4.75 0 0 0 10.75 13h-5.5zM2.5 17.75A2.75 2.75 0 0 1 5.25 15h5.5a2.75 2.75 0 0 1 2.75 2.75c0 .69-.56 1.25-1.25 1.25h-8.5c-.69 0-1.25-.56-1.25-1.25zM14 9.5a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0zM17.5 8a1.5 1.5 0 1 0 0 3 1.5 1.5 0 0 0 0-3zm0 6.5a1 1 0 1 0 0 2h2.3a1.7 1.7 0 0 1 1.7 1.7.8.8 0 0 1-.8.8h-3.2a1 1 0 1 0 0 2h3.2a2.8 2.8 0 0 0 2.8-2.8 3.7 3.7 0 0 0-3.7-3.7h-2.3z">
                                    </path>
                                </svg>
                            </button>
                        </li>
                        <li class="libarranav">
                            <button id="Button1" runat="server" class="botonfoto" onserverclick="Button1_Click">
                                <img id="navUsuarioFoto" runat="server" class="foto-usuario" />
                            </button>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <div class="divcards">
            <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false"
                tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h1 class="modal-title fs-5" id="staticBackdropLabel">Crear Publicación</h1>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="input-group flex-nowrap">
                                <span class="input-group-text" id="addon-wrapping">Descripción</span>
                                <asp:TextBox ID="txtDescripcion" type="text" runat="server" class="form-control" aria-label="Descripcion"
                                    aria-describedby="addon-wrapping"></asp:TextBox>
                            </div>
                            <div class="form-group camposRegistro detail">
                                <asp:FileUpload ID="FileUploadPubli" runat="server" accept="image/*" CssClass="form-control" />
                                <div class="divImagenPubli">
                                    <img id="imagenPerfil" src="#" alt="Vista previa de la imagen" style="max-width: 100%;
                                        display: none; margin: auto; padding-top: 5%;" />
                                </div>
                            </div>
                            <script>
                                document.getElementById('<%= FileUploadPubli.ClientID %>').addEventListener('change', function () {
                                    var archivo = this.files[0];
                                    if (archivo) {
                                        var reader = new FileReader();
                                        reader.onload = function (e) {
                                            var imagenPerfil = document.getElementById('imagenPerfil');
                                            imagenPerfil.src = e.target.result;
                                            imagenPerfil.style.display = 'inline-block';
                                        }
                                        reader.readAsDataURL(archivo);
                                    }
                                });
                            </script>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                            <asp:Button ID="btnCrearPublicacion" runat="server" Text="Crear Publicación" CssClass="btn btn-success" OnClick="btnCrearPublicacion_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="card mb-3">
                <button type="button" class="btn btn-primary botonmodal" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                    Crear nueva publicación
                </button>
            </div>
            
            <div id="contenedorPublicaciones" runat="server" class="container mb-3">
                
            </div>
            
        </div>
    </form>
</body>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
    rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH"
    crossorigin="anonymous" />
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"
    integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz"
    crossorigin="anonymous"></script>
</html>
