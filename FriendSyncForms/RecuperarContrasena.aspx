<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarContrasena.aspx.cs"
    Inherits="FriendSyncForms.RecuperarContrasena" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Recupera tu contraseña</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"
        integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T"
        crossorigin="anonymous" />

    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"
        integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo"
        crossorigin="anonymous"></script>
    <link rel="icon" href="../Media/LogoFriendSync.png" type="image/x-icon" />
    <link rel="stylesheet" href="../CSS/EstilosRecuperarContrasena.css" />
    <link rel="icon" href="../Media/LogoFriendSync.png" type="image/x-icon" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Recuperación de Contraseña</h2>
            <div class="form-group">
                <asp:Label ID="lblEmail" runat="server" Text="Correo Electrónico:" AssociatedControlID="txtEmail"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control recuperarTextBox"
                    TextMode="Email"
                    Required="true"></asp:TextBox>

            </div>

            <div class="form-group camposRegistro">
                <asp:Label ID="Label6" runat="server" Text="Fecha de Nacimiento" CssClass="control-label"></asp:Label>
                <asp:TextBox ID="TextBox6" runat="server" Type="date"
                    CssClass="form-control recuperarTextBox"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Button ID="btnRecuperarContraseña" runat="server" Text="Recuperar Contraseña"
                    CssClass="btn btn-primary botonRecuperar" OnClick="BtnRecuperarContraseña_Click" />
                <br />
                <br />
                <asp:Label ID="Label2" runat="server" Text="" CssClass="control-label"></asp:Label>
                <asp:TextBox ID="Textboxestablecer" runat="server" placeholder="Contraseña Nueva"
                    CssClass="form-control recuperarTextBox"></asp:TextBox>
                <br />
                <br />
                <asp:TextBox ID="TextboxConfirmar" runat="server" placeholder="Confirmar Contraseña Nueva"
                    CssClass="form-control recuperarTextBox"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="restablecer" runat="server" Text="Restablecer Contraseña" CssClass="btn btn-primary botonRecuperar"
                    OnClick="RstablecerContraseña" />
            </div>
        </div>
    </form>
</body>
</html>
