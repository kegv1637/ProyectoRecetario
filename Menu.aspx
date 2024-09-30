<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="ProyectoFinal.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Recetario</title>
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap" rel="stylesheet"/>
    <style>
        body {
            font-family: 'Roboto', sans-serif;
            margin: 0;
            padding: 0;
            background: url('images/Fondo comidaBN.jpeg') no-repeat center center fixed;
            background-size: cover;
            color: #333;
        }

        #header {
            background-color: #050200;
            padding: 20px;
            display: flex;
            align-items: center;
            justify-content: space-between;
            text-align: center;
            margin-bottom: 20px;
        }

        #header h1 {
            font-size: 36px;
            margin: 0;
            color: #fff;
            flex-grow: 1;
            text-align: center;
        }

        #header-right {
            display: flex;
            flex-direction: column;
            align-items: flex-end;
        }

        .adminImage {
            width: 50px;
            height: 50px;
            margin-bottom: 5px;
            border-radius: 5px;
        }

        #container {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 20px;
            padding: 20px;
        }

        #content {
            background-color: #fff;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            padding: 20px;
        }

        #GridViewRecetas {
            border: 1px solid #ccc;
            border-collapse: collapse;
            width: 100%;
        }

        #GridViewRecetas th, #GridViewRecetas td {
            padding: 10px;
            border: 1px solid #ccc;
            text-align: left;
        }

        #PanelDetalles {
            background-color: #f9f9f9;
            padding: 20px;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .header-image {
            width: 50px;
            height: 50px;
        }
        .multiline-label {
            white-space: pre-line;
        }
        .btn {
            padding: 10px;
            margin: 5px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
        .btn-view { background-color: royalblue; color: white; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="header">
            <h1>Bienvenido a mi página web de recetas</h1>
            <div id="header-right">
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/sesion admin 2.png" CssClass="adminImage" OnClick="ImageButton1_Click" />
                <asp:Label ID="Label2" runat="server" Text="Lic. Laura Camacho" ForeColor="#fff"></asp:Label>
            </div>
        </div>
        <div id="container">
            <div id="content">
                <asp:TextBox ID="TextBoxBuscar" runat="server"  placeholder="Buscar receta..." Width="316px" Height="21px"></asp:TextBox>
                <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" CssClass="btn btn-view" OnClick="ButtonBuscar_Click" Height="36px" Width="82px" />
                <asp:Button ID="ButtonMostrartodo" runat="server" Text="Mostrar Todo" CssClass="btn btn-view" OnClick="ButtonMostrarTodo_Click" Height="36px" Width="95px" />
                <br />
                <asp:GridView ID="GridViewRecetas" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridViewRecetas_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                        <asp:BoundField DataField="titulo" HeaderText="Título Receta" SortExpression="titulo" />
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
            <div id="PanelDetalles" runat="server" Visible="False">
                <asp:Label ID="LabelTitulo" runat="server" CssClass="multiline-label"></asp:Label><br />
                <asp:Label ID="LabelDescripcion" runat="server" CssClass="multiline-label"></asp:Label><br />
                <asp:Label ID="LabelIngredientes" runat="server" CssClass="multiline-label"></asp:Label><br />
                <asp:Label ID="LabelInstrucciones" runat="server" CssClass="multiline-label"></asp:Label><br />
            </div>
        </div>
    </form>
</body>
</html>

