using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ProyectoFinal
{
    // Definición de la clase parcial CambiarContrasena que hereda de System.Web.UI.Page
    public partial class CambiarContrasena : System.Web.UI.Page
    {
        // Método que se ejecuta cuando se carga la página
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta cuando se hace clic en el botón de cambiar la contraseña
        protected void btnCambiarContrasena_Click(object sender, EventArgs e)
        {
            // Obtiene los valores de los controles de texto
            string usuario = txtUsuario.Text;
            string contrasenaActual = TextBox1.Text;
            string nuevaContrasena = txtNuevaContrasena.Text;
            string confirmarContrasena = txtConfirmarContrasena.Text;

            // Verifica la contraseña actual del usuario
            if (ValidarContrasena(usuario, contrasenaActual))
            {
                // Si la contraseña es válida, intenta cambiarla
                if (CambiarContrasenaUsuario(usuario, nuevaContrasena, confirmarContrasena))
                {
                    lblMensaje.Text = "Contraseña cambiada exitosamente.";
                }
                else
                {
                    lblMensaje.Text = "Error al cambiar la contraseña.";
                }
            }
            else
            {
                lblMensaje.Text = "Usuario o contraseña actual incorrectos.";
            }
        }

        // Método que valida la contraseña actual del usuario
        private bool ValidarContrasena(string usuario, string contrasenaActual)
        {
            try
            {
                // Crea una conexión a la base de datos usando la cadena de conexión del archivo de configuración
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString))
                {
                    con.Open();
                    // Crea un comando SQL para verificar la contraseña actual del usuario
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario=@NombreUsuario AND Contrasena=@Contrasena", con);
                    cmd.Parameters.AddWithValue("@NombreUsuario", usuario);
                    cmd.Parameters.AddWithValue("@Contrasena", contrasenaActual);

                    // Ejecuta el comando y obtiene el resultado
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                lblMensaje.Text = "Error de conexión. Intente más tarde.";
                return false;
            }
        }

        // Método que cambia la contraseña del usuario
        private bool CambiarContrasenaUsuario(string usuario, string nuevaContrasena, string confirmarContrasena)
        {
            // Verifica que las nuevas contraseñas coincidan
            if (nuevaContrasena != confirmarContrasena)
            {
                lblMensaje.Text = "Las contraseñas nuevas no coinciden.";
                return false;
            }

            // Verifica que la nueva contraseña no esté vacía
            if (string.IsNullOrEmpty(nuevaContrasena))
            {
                lblMensaje.Text = "La nueva contraseña no puede estar vacía.";
                return false;
            }

            try
            {
                // Crea una conexión a la base de datos usando la cadena de conexión del archivo de configuración
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString))
                {
                    con.Open();
                    // Crea un comando SQL para actualizar la contraseña del usuario
                    string query = "UPDATE Usuarios SET Contrasena = @Contrasena WHERE NombreUsuario = @NombreUsuario";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Contrasena", nuevaContrasena); // Encriptación recomendada
                    cmd.Parameters.AddWithValue("@NombreUsuario", usuario);

                    // Ejecuta el comando y obtiene el número de filas afectadas
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                lblMensaje.Text = "Error de conexión. Intente más tarde.";
                return false;
            }
        }
    }
}
