<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProyectoFinal.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: url('images/Fondo colores.jpg') no-repeat center center fixed;
            background-size: cover;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        .login-container {
            background-color: #BBEBFC;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            width: 382px;
            text-align: center;
            height: 420px;
        }
        .login-container h2 {
            margin-bottom: 20px;
        }
        .login-container input[type="text"],
        .login-container input[type="password"] {
            padding: 10px;
            margin: 10px 0;
            border: 1px solid #ccc;
            border-radius: 3px;
        }
        .login-container input[type="submit"] {
            background-color: #F7E0AC;
            color: #fff;
            border: none;
            padding: 10px;
            border-radius: 3px;
            cursor: pointer;
        }
        .login-container input[type="submit"]:hover {
            background-color: #F3D592;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>Iniciar Sesión</h2>
            <asp:Image ID="Image1" runat="server" Height="87px" ImageUrl="~/images/logologin.jpeg" Width="94px" />
            <br />
            <asp:TextBox ID="txtNombreUsuario" runat="server" Placeholder="Username" Width="131px"></asp:TextBox><br />
            <asp:TextBox ID="txtContrasena" runat="server" TextMode="Password" Placeholder="Password" Width="130px"></asp:TextBox>
            <br />
            <asp:Label ID="lblMensajeError" runat="server" Font-Size="Small"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="BtnLogin_Click" ForeColor="Black" />
            <br />
            <br />
            <asp:HyperLink ID="HyperLink1" runat="server" Font-Size="Small" NavigateUrl="~/Confirmacion.aspx">Olvide mi contraseña</asp:HyperLink>
            <br />
            <br />
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Menu.aspx">Return</asp:HyperLink>
        </div>
    </form>
</body>
</html>
