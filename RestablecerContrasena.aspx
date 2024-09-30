<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestablecerContrasena.aspx.cs" Inherits="ProyectoFinal.RestablecerContrasena" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Restablecer Contraseña</title>
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
            width: 300px;
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
            background-color: #007bff;
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
            <h2>Restablecer Contraseña</h2>
            <asp:Panel ID="PanelMensaje" runat="server" Visible="false" CssClass="message-panel">
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </asp:Panel>
            <asp:Panel ID="PanelCambioContrasena" runat="server" Visible="false">
                <div class="form-group">
                    <asp:Label ID="lblNuevaContrasena" runat="server" Text="Nueva Contraseña"></asp:Label>
                    <asp:TextBox ID="txtNuevaContrasena" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblConfirmarContrasena" runat="server" Text="Confirmar Contraseña"></asp:Label>
                    <asp:TextBox ID="txtConfirmarContrasena" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Button ID="btnCambiarContrasena" runat="server" Text="Cambiar Contraseña" OnClick="btnCambiarContrasena_Click" CssClass="btn btn-view" />
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

