using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ProyectoFinal
{
    // Definición de la clase parcial WebForm1 que hereda de System.Web.UI.Page
    public partial class WebForm1 : System.Web.UI.Page
    {
        // Método que se ejecuta cuando se carga la página
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta cuando se hace clic en el botón de login
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            // Obtiene el nombre de usuario y la contraseña de los controles de texto
            string nombreUsuario = txtNombreUsuario.Text;
            string contrasena = txtContrasena.Text;

            // Crea una conexión a la base de datos usando la cadena de conexión del archivo de configuración
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString))
            {
                // Abre la conexión a la base de datos
                con.Open();
                // Define la consulta SQL para verificar si existe un usuario con el nombre de usuario y la contraseña proporcionados
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario=@NombreUsuario AND Contrasena=@Contrasena", con);
                // Agrega los parámetros @NombreUsuario y @Contrasena al comando
                cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                cmd.Parameters.AddWithValue("@Contrasena", contrasena);
                // Ejecuta la consulta y obtiene el número de coincidencias
                int count = (int)cmd.ExecuteScalar();

                // Verifica si se encontró al menos una coincidencia
                if (count > 0)
                {
                    // Usuario autenticado
                    // Almacena el nombre de usuario en la sesión
                    Session["NombreUsuario"] = nombreUsuario;
                    // Redirige a la página Default.aspx
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    // Autenticación fallida
                    // Muestra un mensaje de error
                    lblMensajeError.Text = "Usuario o contraseña incorrectos.";
                }
            }
        }
    }
}
