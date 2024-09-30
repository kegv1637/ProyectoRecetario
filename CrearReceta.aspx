<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearReceta.aspx.cs" Inherits="ProyectoFinal.CrearReceta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Crear Receta</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: url('images/Fondo comidaBN.jpeg') no-repeat center center fixed;
            background-size: cover;
            margin: 0;
            padding: 0;
        }

        .container {
            width: 57%;
            margin: 50px auto;
            background: url('images/Fondo colores.jpg') no-repeat center center fixed;
            background-size: cover;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }

        h2 {
            text-align: center;
            color: #333;
            margin-bottom: 20px;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-group label {
            font-weight: bold;
        }

        .form-group input[type="text"],
        .form-group textarea {
            padding: 8px;
            border: 1px solid #ccc;
            border-radius: 4px;
            font-size: 14px;
        }

        .form-group textarea {
            margin-right: 0px;
        }

        .btn-guardar {
            background-color: #4CAF50;
            color: white;
            border: none;
            padding: 10px 20px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            border-radius: 4px;
            cursor: pointer;
        }

        .btn-guardar:hover {
            background-color: #45a049;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Crear Receta</h2>
            <div class="form-group">
                <label for="txtNombre">Nombre:</label>
                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Width="92%" placeholder="Ingrese el nombre de la receta"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtDescripcion">Descripción:</label>
                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" Width="90%" placeholder="Ingrese la descripción de la receta" Height="95px"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtIngredientes">Ingredientes:</label>
                <asp:TextBox ID="txtIngredientes" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Width="90%" placeholder="Ingrese los ingredientes de la receta, uno por línea" Height="95px"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtInstrucciones">Instrucciones:</label>
                <asp:TextBox ID="txtInstrucciones" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" Width="90%" placeholder="Ingrese las instrucciones de preparación de la receta" Height="95px"></asp:TextBox>
            </div>
            <div style="text-align: center;">
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn-guardar" Text="Guardar" OnClick="btnGuardar_Click" />
            &nbsp;<asp:Button ID="btnCancelar" runat="server"  CssClass="btn-guardar" Text="Cancelar" OnClick="btnCancelar_Click" />
            </div>
        </div>
    </form>
</body>
</html>
