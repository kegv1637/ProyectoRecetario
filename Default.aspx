<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProyectoFinal.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin - Recetario</title>
    <link href="https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap" rel="stylesheet"/>
    <style>
        body {
            font-family: 'Roboto', sans-serif;
            margin: 0;
            padding: 0;
            background: url('images/Fondo colores.jpg') no-repeat center center fixed;
            background-size: cover;
            color: #333;
        }

        #header {
            background-color: #9933FF;
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

        #adminLink {
            font-size: 18px;
            color: #fff;
            text-decoration: none;
            border-bottom: 1px solid transparent;
            transition: border-color 0.3s ease;
            margin-bottom: 5px;
        }

        #adminLink:hover {
            border-color: #fff;
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
            }

        #GridViewRecetas th, #GridViewRecetas td {
            padding: 10px;
            border: 1px solid #ccc;
            text-align: left;
        }

        .button-container {
            display: flex;
            justify-content: space-around;
            margin-bottom: 20px;
            width: 141px;
            margin-left: 560px;
        }

        .btn {
            padding: 10px;
            margin: 5px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
        }
        .btn-create {
            padding: 10px;
            margin: 5px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            background-color: gold; 
            color: black; 
        }
        .btn-view { background-color: #4CAF50; color: white; }
        .btn-edit { background-color: #2196F3; color: white; }
        .btn-delete { background-color: #f44336; color: white; }
        .btn-search { background-color: #871783; color: white; }

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
        .header-hyperlink{
            text-decoration: none;
            color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="header">
            <h1>Administración de Recetas</h1>
            <div id="header-right">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Menu.aspx" class="header-hyperlink">Cerrar Sesion</asp:HyperLink>
                &nbsp;
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/CambiarContrasena.aspx" class="header-hyperlink">Cambiar Contraseña</asp:HyperLink>
            </div>
        </div>
        <div id="container">
            <div id="content">
                <div class="button-container">
                    <asp:Button ID="ButtonCreate" runat="server" Text="Crear Receta" CssClass="btn-create" OnClick="ButtonCreate_Click" />
                </div>
                <asp:TextBox ID="TextBoxBuscar" runat="server"  placeholder="Buscar receta..." Width="316px" Height="21px"></asp:TextBox>
                <asp:Button ID="ButtonBuscar" runat="server" Text="Buscar" CssClass="btn btn-search" OnClick="ButtonBuscar_Click" Height="36px" Width="82px"/>
                <asp:Button ID="ButtonMostrartodo" runat="server" Text="Mostrar Todo" CssClass="btn btn-search" OnClick="ButtonMostrarTodo_Click" Height="36px" Width="133px" />
                <br />
                <asp:GridView ID="GridViewRecetas" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewRecetas_RowCommand" OnRowDeleting="GridViewRecetas_RowDeleting" OnRowEditing="GridViewRecetas_RowEditing" DataKeyNames="Id" Width="694px">
                    <Columns>
                        <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="id" />
                        <asp:BoundField DataField="titulo" HeaderText="Recetas" SortExpression="titulo" />
                        <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnView" runat="server" Text="Ver" CommandName="View" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-view" />
                            <asp:Button ID="btnEdit" runat="server" Text="Modificar" CommandName="Edit" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-edit" />
                            <asp:Button ID="btnDelete" runat="server" Text="Eliminar" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-delete" OnClientClick="return confirm('¿Está seguro que desea eliminar esta receta?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div id="PanelDetalles" runat="server" Visible="False">
                <asp:Label ID="LabelTitulo" runat="server" Text="Título: "></asp:Label><br />
                <asp:Label ID="LabelDescripcion" runat="server" Text="Descripción: " CssClass="multiline-label"></asp:Label><br />
                <asp:Label ID="LabelIngredientes" runat="server" Text="Ingredientes: " CssClass="multiline-label"></asp:Label><br />
                <asp:Label ID="LabelInstrucciones" runat="server" Text="Instrucciones: " CssClass="multiline-label"></asp:Label><br />
           </div>
           <div id="PanelModificar" runat="server" Visible="False">
                <asp:Label ID="Label5" runat="server" Text="Titulo:"></asp:Label>
                <br />
                <asp:TextBox ID="txtTitulo" runat="server" Width="256px"></asp:TextBox>
                <br />
                <asp:Label ID="Label2" runat="server" Text="Descripción: "></asp:Label>
                <br />
                <asp:TextBox ID="txtDescripcion" runat="server" Height="45px" TextMode="MultiLine" Width="263px"></asp:TextBox>
                <br />
                <asp:Label ID="Label3" runat="server" Text="Ingredientes: "></asp:Label>
                <br />
                <asp:TextBox ID="txtIngredientes" runat="server" Height="43px" TextMode="MultiLine" Width="261px"></asp:TextBox>
                <br />
                <asp:Label ID="Label4" runat="server" Text="Instrucciones: "></asp:Label>
                <br />
                <asp:TextBox ID="txtInstrucciones" runat="server" Height="74px" TextMode="MultiLine" Width="259px"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnGuardar" runat="server" Text="GUARDAR" CssClass="btn btn-search" OnClick="btnGuardar_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancelar" runat="server"  CssClass="btn btn-search" OnClick="btnCancelar_Click" Text="CANCELAR" />
                <br />
                <br />
          </div>
        </div>
    </form>
</body>
</html>
