<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaginaRegistro.aspx.cs"
    Inherits="FriendSyncForms.PaginaRegistro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Regístrate en FriendSync</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
        rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH"
        crossorigin="anonymous" />
    <link rel="stylesheet" href="../CSS/EstilosRegistro.css" />
</head>
<body>
    <div class="ContenedorGeneral">
        <div class="divTitulito">
            <h5><b>Ingresa los siguientes datos para crear tu usuario y poder ingresar a FriendSync</b>
            </h5>
        </div>

        <form id="form1" runat="server" class="formulario">
            <div class="cajasdetexto">
                
                <div class="form-group camposRegistro">
                    <asp:Label ID="Label2" runat="server" Text="NombreUsuario" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group camposRegistro">
                    <asp:Label ID="Label3" runat="server" Text="Email" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group camposRegistro">
                    <asp:Label ID="Label4" runat="server" Text="Contraseña" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group camposRegistro">
                    <asp:Label ID="Label5" runat="server" Text="NombreCompleto" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group camposRegistro">
                    <asp:Label ID="Label6" runat="server" Text="Fecha de Nacimiento" CssClass="control-label"></asp:Label>
                    <asp:TextBox ID="TextBox6" runat="server" Type="date" pattern="\d{2}-\d{2}-\d{4}"
                        CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group camposRegistro">
                    <asp:Label ID="Label7" runat="server" Text="Foto de perfil" CssClass="control-label"></asp:Label>
                    <asp:FileUpload ID="FileUpload1" runat="server" accept="image/*" CssClass="form-control" />
                    <div>
                        <img id="imagenPerfil" src="#" alt="Vista previa de la imagen" style="max-width: 100px;
                            display: none; margin: auto;" />
                    </div>
                </div>

                <script>
                    document.getElementById('<%= FileUpload1.ClientID %>').addEventListener('change', function () {
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
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Crear Usuario"
                CssClass="btn btn-primary botoncrearusuario" />
            <div class="form-group camposRegistro">
                <asp:Label ID="Label10" runat="server" CssClass="control-label"></asp:Label>
            </div>
        </form>
    </div>
</body>
</html>
