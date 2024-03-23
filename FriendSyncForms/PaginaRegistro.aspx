<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaginaRegistro.aspx.cs"
    Inherits="FriendSyncForms.PaginaRegistro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Regístrate en FriendSync</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
    rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH"
    crossorigin="anonymous"/>
     <link rel="stylesheet" href="../CSS/EstilosRegistro.css" />
</head>
<body>
    <div class="ContenedorGeneral">
    <h5> Ingresa los siguientes datos para crear tu usuario y poder ingresar a FriendSync</h5>
    <form id="form1" runat="server">
        <div class="form-group camposRegistro camposRegistro">
            <asp:Label ID="Label1" runat="server" Text="Identificación" CssClass="control-label"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group camposRegistro">
            <asp:Label ID="Label2" runat="server" Text="Nombre" CssClass="control-label"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group camposRegistro">
            <asp:Label ID="Label3" runat="server" Text="Apellido" CssClass="control-label"></asp:Label>
            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group camposRegistro">
            <asp:Label ID="Label4" runat="server" Text="Dirección" CssClass="control-label"></asp:Label>
            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group camposRegistro">
            <asp:Label ID="Label5" runat="server" Text="Email" CssClass="control-label"></asp:Label>
            <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group camposRegistro">
            <asp:Label ID="Label6" runat="server" Text="Celular" CssClass="control-label"></asp:Label>
            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group camposRegistro">
            <asp:Label ID="Label7" runat="server" Text="Carrera" CssClass="control-label"></asp:Label>
            <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group camposRegistro">
            <asp:Label ID="Label8" runat="server" Text="Ciudad" CssClass="control-label"></asp:Label>
            <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group camposRegistro">
            <asp:Label ID="Label9" runat="server" Text="Edad" CssClass="control-label"></asp:Label>
            <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Crear Usuario"
            CssClass="btn btn-primary" />
        <div class="form-group camposRegistro">
            <asp:Label ID="Label10" runat="server" CssClass="control-label"></asp:Label>
        </div>
    </form>
        </div>
</body>
</html>
