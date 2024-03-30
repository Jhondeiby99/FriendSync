<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FriendSyncForms.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Inicia Sesion</title>
    <link rel="icon" href="../Media/LogoFriendSync.png" type="image/x-icon">
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"
        integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T"
        crossorigin="anonymous" />
    
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"
        integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.6/dist/umd/popper.min.js"
        integrity="sha384-oBqDVmMz9ATKxIep9tiCxS/Z9fNfEXOlGJV5g5fCpGVXR+6lGvR/6CZ9gHgT5pdF"
        crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"
        integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjUEfvqsFZD6J6eVHzI6pPbAR5JN1Hb"
        crossorigin="anonymous"></script>
    <link rel="stylesheet" href="../CSS/EstilosLogin.css"/>


</head>
<body>
    <div>
        <form id="form1" runat="server">
            <div class="container mt-5">
                <div class="row justify-content-center ">
                    <div class="col-md-6 divform">
                        <div>
                        <div class="tituloLogin">
                            
                         <b> <h1 class="text-center mb-4">FriendSync</h1></b>
                            
                        </div>
                            </div>

                        <div class="tboxLogin">
                        <div class="form-group">
                            <label for="TextBox1">Nombre de usuario o Email:</label>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control usuarioTextbox"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label for="TextBox2">Contraseña:</label>
                            <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" CssClass="form-control passTextbox"></asp:TextBox>
                        </div>

                        <div class="text-center ">
                            <asp:Button ID="Button1" runat="server" Text="Iniciar Sesión" 
                                CssClass="btn btn-primary botonLogin" OnClick="Button1_Click" />
                        </div><div class="text-center ">
                            <asp:Button ID="Button2" runat="server" Text="Registrarse" 
                                CssClass="btn btn-primary botonRegistrarse" OnClick="Button2_Click" />
                        </div>
                            <div class="divlabel">
                                 <asp:Label ID="Label1" runat="server"></asp:Label>
                            </div>
                            <div class="divOlviContra">
                                 <a class="botonOlvidasteC" href="RecuperarContrasena.aspx">¿Olvidaste tu contraseña?</a>
                            </div>
                           
                    </div>
                        
                </div>
                    </div>
            </div>
        </form>
    </div>
    
</body>

</html>

