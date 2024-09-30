<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambiarContrasena.aspx.cs" Inherits="ProyectoFinal.CambiarContrasena" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cambiar Contraseña</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: url('images/Fondo colores.jpg') no-repeat center center fixed;
            background-size: cover;
            color: #333;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }
        .container {
            background-color: #fff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            width: 340px;
            text-align: center;
        }
        h2 {
            color: #007bff;
        }
        .message-panel {
            margin-bottom: 15px;
            color: red;
        }
        .form-group {
            margin-bottom: 15px;
            text-align: left;
        }
        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }
        .form-group input {
            width: 100%;
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }
        .form-group button {
            width: 100%;
            padding: 10px;
            background-color: #07CAB8;
            border: none;
            color: #fff;
            font-size: 16px;
            border-radius: 4px;
            cursor: pointer;
        }
        .form-group button:hover {
            background-color: #0056b3;
        }
        .btn {
            padding: 10px;
            margin: 5px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
        .btn-view {
            background-color: #4CAF50;
            color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Cambiar Contraseña</h2>
            <asp:Panel ID="PanelCambioContrasena" runat="server" Visible="true" Width="331px">
                <div class="form-group">
                    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                    <br />
                    <asp:Label ID="lblUsuario" runat="server" Text="Usuario:"></asp:Label>
                    <br />
                    <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="txtContrasena" runat="server" Text="Contraseña:"></asp:Label>
                    <br />
                    <asp:TextBox ID="TextBox1" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="lblNuevaContrasena" runat="server" Text="Nueva Contraseña"></asp:Label>
                    <asp:TextBox ID="txtNuevaContrasena" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblConfirmarContrasena" runat="server" Text="Confirmar Contraseña"></asp:Label>
                    <asp:TextBox ID="txtConfirmarContrasena" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnCambiarContrasena" runat="server" Text="Cambiar Contraseña" OnClick="btnCambiarContrasena_Click" CssClass="btn btn-view" AutoPostBack="true" />
                    <br />
                    <br />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Return</asp:HyperLink>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

