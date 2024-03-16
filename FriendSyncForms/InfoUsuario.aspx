<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InfoUsuario.aspx.cs" Inherits="FriendSyncForms.InfoUsuario" %>

<!DOCTYPE html>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!-- Enlaces a los estilos de Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
          rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH"
          crossorigin="anonymous">
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container">
                <div class="botonVolverinicio">
                    <asp:Button ID="Button1" runat="server" Text="Volver a inicio" 
                        CssClass="btn btn-primary" OnClick="Button1_Click" />
                </div>
                <h1 class="mt-5">Perfil de Usuario
                </h1>
                <div class="user-details">
                    <img id="userImage" runat="server" class="img-fluid rounded mx-auto d-block" />
                    <div class="detail">
                        <label for="txtIdUsuario">ID:</label>
                        <input id="txtIdUsuario" runat="server" class="form-control" readonly />
                    </div>
                    <div class="detail">
                        <label for="txtNombreUsuario">Nombre de Usuario:</label>
                        <input id="txtNombreUsuario" runat="server" class="form-control" readonly />
                    </div>
                    <div class="detail">
                        <label for="txtEmail">Email:</label>
                        <input id="txtEmail" runat="server" class="form-control" readonly />
                    </div>
                    <div class="detail">
                        <label for="txtNombreCompleto">Nombre Completo:</label>
                        <input id="txtNombreCompleto" runat="server" class="form-control" readonly />
                    </div>
                    <div class="detail">
                        <label for="txtFechaNacimiento">Fecha de Nacimiento:</label>
                        <input id="txtFechaNacimiento" runat="server" class="form-control" readonly />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

