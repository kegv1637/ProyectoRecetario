using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ProyectoFinal
{
    // Definición de la clase parcial WebForm2 que hereda de System.Web.UI.Page
    public partial class WebForm2 : System.Web.UI.Page
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

        // Evento que se ejecuta cuando se selecciona una fila en el GridView
        protected void GridViewRecetas_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtiene la fila seleccionada del GridView
            GridViewRow row = GridViewRecetas.SelectedRow;
            // Convierte el texto de la primera celda (ID de la receta) a entero
            int idReceta = Convert.ToInt32(row.Cells[0].Text);
            // Llama al método MostrarDetallesReceta con el ID de la receta
            MostrarDetallesReceta(idReceta);
        }

        // Método para mostrar los detalles de una receta específica
        private void MostrarDetallesReceta(int idReceta)
        {
            // Crea una conexión a la base de datos usando la cadena de conexión del archivo de configuración
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString))
            {
                // Define la consulta SQL para obtener los detalles de una receta específica
                string query = "SELECT titulo, descripcion, ingredientes, instrucciones FROM recetas WHERE id = @idReceta";
                // Crea un comando SQL con la consulta y la conexión
                SqlCommand command = new SqlCommand(query, con);
                // Agrega el parámetro @idReceta al comando
                command.Parameters.AddWithValue("@idReceta", idReceta);

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

                    // Muestra el panel de detalles
                    PanelDetalles.Visible = true;
                }

                // Cierra el lector de datos
                reader.Close();
            }
        }

        // Evento que se ejecuta cuando se hace clic en el ImageButton para redirigir a la página de login
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            // Redirige a la página Login.aspx
            Response.Redirect("Login.aspx");
        }

        // Evento que se ejecuta cuando se hace clic en el botón de buscar
        protected void ButtonBuscar_Click(object sender, EventArgs e)
        {
            // Obtiene el texto de búsqueda del TextBox y elimina espacios en blanco al principio y al final
            string textoBusqueda = TextBoxBuscar.Text.Trim();
            // Crea una conexión a la base de datos usando la cadena de conexión del archivo de configuración
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString))
            {
                // Define la consulta SQL para buscar recetas que coincidan con el texto de búsqueda
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

        // Evento que se ejecuta cuando se hace clic en el botón para mostrar todas las recetas
        protected void ButtonMostrarTodo_Click(object sender, EventArgs e)
        {
            // Llama al método CargarRecetas para cargar y mostrar todas las recetas
            CargarRecetas();
        }
    }
}
