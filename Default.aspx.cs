using System;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ProyectoFinal
{
    // Definición de la clase parcial Default que hereda de System.Web.UI.Page
    public partial class Default : System.Web.UI.Page
    {
        // Método que se ejecuta cuando se carga la página
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica si no es una recarga de página
            if (!IsPostBack)
            {
                // Llama al método CargarRecetas
                CargarRecetas();
            }
        }

        // Evento que se ejecuta cuando se hace clic en el botón para crear una nueva receta
        protected void ButtonCreate_Click(object sender, EventArgs e)
        {
            // Redirige a la página CrearReceta.aspx
            Response.Redirect("~/CrearReceta.aspx");
        }

        // Método para cargar las recetas desde la base de datos
        private void CargarRecetas()
        {
            // Crea una conexión a la base de datos usando la cadena de conexión del archivo de configuración
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString))
            {
                // Define la consulta SQL para obtener las recetas
                string query = "SELECT id, titulo FROM recetas";
                // Crea un adaptador de datos SQL con la consulta y la conexión
                SqlDataAdapter adapter = new SqlDataAdapter(query, con);
                // Crea una tabla de datos para almacenar los resultados
                DataTable dataTable = new DataTable();
                // Llena la tabla de datos con los resultados de la consulta
                adapter.Fill(dataTable);

                // Asigna la tabla de datos como fuente de datos para el GridView y enlaza los datos
                GridViewRecetas.DataSource = dataTable;
                GridViewRecetas.DataBind();
            }
        }

        // Evento que se ejecuta cuando se inicia la edición de una fila en el GridView
        protected void GridViewRecetas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Establece el índice de la fila que se está editando
            GridViewRecetas.EditIndex = e.NewEditIndex;
            // Recarga las recetas para reflejar el cambio
            //CargarRecetas();
        }

        // Evento que se ejecuta cuando se elimina una fila en el GridView
        protected void GridViewRecetas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                // Obtiene el ID de la receta que se va a eliminar
                int recetaId = Convert.ToInt32(GridViewRecetas.DataKeys[e.RowIndex].Value.ToString());

                // Crea una conexión a la base de datos usando la cadena de conexión del archivo de configuración
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString))
                {
                    // Define la consulta SQL para eliminar la receta
                    string query = "DELETE FROM recetas WHERE id = @id";
                    // Crea un comando SQL con la consulta y la conexión
                    SqlCommand cmd = new SqlCommand(query, con);
                    // Agrega el parámetro @id al comando
                    cmd.Parameters.AddWithValue("@id", recetaId);

                    // Abre la conexión a la base de datos
                    con.Open();
                    // Ejecuta el comando de eliminación
                    cmd.ExecuteNonQuery();
                    // Cierra la conexión a la base de datos
                    con.Close();
                }

                // Recarga las recetas para reflejar los cambios
                CargarRecetas();
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción que ocurra
                // Puede registrar el error o mostrar un mensaje al usuario
                throw new ApplicationException("Error eliminando la receta: " + ex.Message);
            }
        }

        // Evento que se ejecuta cuando se hace clic en un comando de una fila en el GridView
        protected void GridViewRecetas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Obtiene el ID de la receta del argumento del comando
            int recetaId = Convert.ToInt32(e.CommandArgument);
            // Verifica el nombre del comando y realiza la acción correspondiente
            if (e.CommandName == "View")
            {
                MostrarReceta(recetaId);
            }
            else if (e.CommandName == "Edit")
            {
                EditarReceta(recetaId);
            }
            else if (e.CommandName == "Delete")
            {
                // La eliminación ya está manejada por GridViewRecetas_RowDeleting
            }
            // Recarga las recetas para reflejar los cambios
            CargarRecetas();
        }

        // Método para mostrar los detalles de una receta específica
        private void MostrarReceta(int recetaID)
        {
            // Crea una conexión a la base de datos usando la cadena de conexión del archivo de configuración
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString))
            {
                // Define la consulta SQL para obtener los detalles de una receta específica
                string query = "SELECT titulo, descripcion, ingredientes, instrucciones FROM recetas WHERE id = @idReceta";
                // Crea un comando SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(query, con);
                // Agrega el parámetro @idReceta al comando
                command.Parameters.AddWithValue("@idReceta", recetaID);

                // Abre la conexión a la base de datos
                con.Open();
                // Ejecuta el comando y obtiene un lector de datos
                SqlDataReader reader = command.ExecuteReader();

                // Si el lector puede leer una fila
                if (reader.Read())
                {
                    // Asigna los valores de los campos de la receta a las etiquetas correspondientes
                    LabelTitulo.Text = "Título: " + reader["titulo"].ToString();
                    LabelDescripcion.Text = "\nDescripción: \n" + reader["descripcion"].ToString().Replace("\n", "<br />");
                    LabelIngredientes.Text = "\nIngredientes: \n" + reader["ingredientes"].ToString().Replace("\n", "<br />");
                    LabelInstrucciones.Text = "\nInstrucciones: \n" + reader["instrucciones"].ToString().Replace("\n", "<br />");

                    // Muestra el panel de detalles y oculta el panel de modificación
                    PanelModificar.Visible = false;
                    PanelDetalles.Visible = true;
                }

                // Cierra el lector de datos
                reader.Close();
            }
        }

        // Método para editar una receta específica
        private void EditarReceta(int recetaID)
        {
            // Crea una conexión a la base de datos usando la cadena de conexión del archivo de configuración
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString))
            {
                // Define la consulta SQL para obtener los detalles de una receta específica
                string query = "SELECT titulo, descripcion, ingredientes, instrucciones FROM recetas WHERE id = @idReceta";
                // Crea un comando SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(query, con);
                // Agrega el parámetro @idReceta al comando
                command.Parameters.AddWithValue("@idReceta", recetaID);

                // Abre la conexión a la base de datos
                con.Open();
                // Ejecuta el comando y obtiene un lector de datos
                SqlDataReader reader = command.ExecuteReader();

                // Si el lector puede leer una fila
                if (reader.Read())
                {
                    // Guarda el ID de la receta en el ViewState
                    ViewState["RecetaID"] = recetaID;
                    // Asigna los valores de los campos de la receta a los controles de texto correspondientes
                    txtTitulo.Text = reader["titulo"].ToString();
                    txtDescripcion.Text = reader["descripcion"].ToString();
                    txtIngredientes.Text = reader["ingredientes"].ToString();
                    txtInstrucciones.Text = reader["instrucciones"].ToString();

                    // Muestra el panel de modificación y oculta el panel de detalles
                    PanelModificar.Visible = true;
                    PanelDetalles.Visible = false;
                }

                // Cierra el lector de datos
                reader.Close();
            }
        }

        // Evento que se ejecuta cuando se hace clic en el botón de guardar los cambios en una receta
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtiene el ID de la receta del ViewState
            int recetaID = (int)ViewState["RecetaID"];
            // Crea una conexión a la base de datos usando la cadena de conexión del archivo de configuración
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString))
            {
                // Define la consulta SQL para actualizar los detalles de una receta específica
                string query = "UPDATE recetas SET titulo = @titulo, descripcion = @descripcion, ingredientes = @ingredientes, instrucciones = @instrucciones WHERE id = @idReceta";
                // Crea un comando SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(query, con);
                // Agrega los parámetros al comando
                command.Parameters.AddWithValue("@idReceta", recetaID);
                command.Parameters.AddWithValue("@titulo", txtTitulo.Text);
                command.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                command.Parameters.AddWithValue("@ingredientes", txtIngredientes.Text);
                command.Parameters.AddWithValue("@instrucciones", txtInstrucciones.Text);

                // Abre la conexión a la base de datos
                con.Open();
                // Ejecuta el comando de actualización
                command.ExecuteNonQuery();
            }

            // Restablece el índice de edición del GridView
            GridViewRecetas.EditIndex = -1;
            // Recarga las recetas para reflejar los cambios
            CargarRecetas();
            // Oculta el panel de modificación
            PanelModificar.Visible = false;
        }

        // Evento que se ejecuta cuando se hace clic en el botón de cancelar la edición de una receta
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Restablece el índice de edición del GridView
            GridViewRecetas.EditIndex = -1;
            // Recarga las recetas
            CargarRecetas();
            // Oculta el panel de modificación y muestra el panel de detalles
            PanelModificar.Visible = false;
            PanelDetalles.Visible = true;
        }

        // Evento que se ejecuta cuando se hace clic en el botón de buscar recetas
        protected void ButtonBuscar_Click(object sender, EventArgs e)
        {
            // Obtiene el texto de búsqueda del control de texto
            string textoBusqueda = TextBoxBuscar.Text.Trim();
            // Crea una conexión a la base de datos usando la cadena de conexión del archivo de configuración
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString))
            {
                // Define la consulta SQL para buscar recetas cuyo título contenga el texto de búsqueda
                string query = "SELECT id, titulo FROM recetas WHERE titulo LIKE '%' + @textoBusqueda + '%'";
                // Crea un comando SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(query, con);
                // Agrega el parámetro @textoBusqueda al comando
                command.Parameters.AddWithValue("@textoBusqueda", textoBusqueda);

                // Crea un adaptador de datos SQL con el comando
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                // Crea una tabla de datos para almacenar los resultados
                DataTable dataTable = new DataTable();
                // Llena la tabla de datos con los resultados de la consulta
                adapter.Fill(dataTable);

                // Asigna la tabla de datos como fuente de datos para el GridView y enlaza los datos
                GridViewRecetas.DataSource = dataTable;
                GridViewRecetas.DataBind();
            }
        }

        // Evento que se ejecuta cuando se hace clic en el botón de mostrar todas las recetas
        protected void ButtonMostrarTodo_Click(object sender, EventArgs e)
        {
            // Llama al método CargarRecetas
            CargarRecetas();
        }
    }
}
