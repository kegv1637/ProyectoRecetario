using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ProyectoFinal
{
    // Definición de la clase parcial CrearReceta que hereda de System.Web.UI.Page
    public partial class CrearReceta : System.Web.UI.Page
    {
        // Método que se ejecuta cuando se carga la página
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta cuando se hace clic en el botón de guardar la nueva receta
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            // Obtiene el ID de la receta desde la cadena de consulta (query string) de la URL
            int recetaId = Convert.ToInt32(Request.QueryString["id"]);

            // Crea una conexión a la base de datos usando la cadena de conexión del archivo de configuración
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString))
            {
                // Crea un comando SQL para insertar una nueva receta en la base de datos
                using (SqlCommand cmd = new SqlCommand("INSERT INTO recetas (titulo, descripcion, ingredientes, instrucciones) VALUES (@titulo, @descripcion, @ingredientes, @instrucciones)", con))
                {
                    // Agrega los parámetros al comando con los valores de los controles de texto
                    cmd.Parameters.AddWithValue("@Id", recetaId);
                    cmd.Parameters.AddWithValue("@Titulo", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
                    cmd.Parameters.AddWithValue("@Ingredientes", txtIngredientes.Text);
                    cmd.Parameters.AddWithValue("@Instrucciones", txtInstrucciones.Text);

                    // Abre la conexión a la base de datos
                    con.Open();
                    // Ejecuta el comando de inserción
                    cmd.ExecuteNonQuery();
                }
            }

            // Redirige a la página Default.aspx
            Response.Redirect("~/Default.aspx");
        }

        // Evento que se ejecuta cuando se hace clic en el botón de cancelar la creación de la receta
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Redirige a la página Default.aspx
            Response.Redirect("~/Default.aspx");
        }
    }
}
