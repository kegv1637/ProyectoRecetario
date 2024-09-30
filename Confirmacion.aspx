<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Confirmacion.aspx.cs" Inherits="ProyectoFinal.RecuperacionContrasena" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Olvidé mi contraseña</title>
    <style>
        body {
            font-family: Arial, sans-serif;
             background: url('images/Fondo colores.jpg') no-repeat center center fixed;
            background-size: cover;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 272px;
            margin: 0;
            width: 593px;
        }
        /*.form-container {
            background-color: #fff;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            text-align: center;
        }*/
        .form-container h2 {
            margin-bottom: 20px;
        }
        .form-container input[type="text"], .form-container input[type="submit"] {
            padding: 10px;
            margin: 10px 0;
            border: 1px solid #ccc;
            border-radius: 5px;
        }
        .form-container input[type="submit"] {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 10px;
            border-radius: 3px;
            cursor: pointer;
        }
        .form-container input[type="submit"]:hover {
            background-color: #0056b3;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container">
            <h2>Olvidé mi contraseña</h2>
            <asp:TextBox ID="TextBoxEmail" runat="server" Placeholder="Correo electrónico" Width="458px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="ButtonEnviar" runat="server" Text="Enviar" OnClick="ButtonEnviar_Click"/><br />
            <asp:Label ID="LabelMensaje" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login.aspx">Return</asp:HyperLink>
        </div>
    </form>
</body>
</html>