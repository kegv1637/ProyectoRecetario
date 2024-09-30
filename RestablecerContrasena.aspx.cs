using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ProyectoFinal
{
    public partial class RestablecerContrasena : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // Verifica si la página se está cargando por primera vez
            {
                string token = Request.QueryString["token"]; // Obtiene el token de la URL
                if (!string.IsNullOrEmpty(token)) // Verifica si el token no está vacío
                {
                    if (ValidarToken(token)) // Valida el token
                    {
                        // Token válido, mostrar formulario para cambiar la contraseña
                        PanelCambioContrasena.Visible = true; // Muestra el panel para cambiar la contraseña
                        Session["Token"] = token; // Almacenar el token en un campo oculto
                    }
                    else
                    {
                        // Token no válido, redirigir a una página de error
                        Response.Redirect("Error.aspx"); // Redirige a la página de error
                    }
                }
                else
                {
                    // No se proporcionó un token válido, redirigir a una página de error
                    Response.Redirect("Error.aspx"); // Redirige a la página de error
                }
            }
        }

        protected void btnCambiarContrasena_Click(object sender, EventArgs e)
        {
            string token = Session["Token"] as string; // Obtener el token del campo oculto
            string nuevaContrasena = txtNuevaContrasena.Text.Trim(); // Obtiene y limpia el campo de nueva contraseña
            string confirmarContrasena = txtConfirmarContrasena.Text.Trim(); // Obtiene y limpia el campo de confirmar contraseña

            if (!string.IsNullOrEmpty(token) && !string.IsNullOrEmpty(nuevaContrasena) && !string.IsNullOrEmpty(confirmarContrasena)) // Verifica que los campos no estén vacíos
            {
                if (nuevaContrasena == confirmarContrasena) // Verifica que las contraseñas coincidan
                {
                    if (ActualizarContrasena(token, nuevaContrasena)) // Intenta actualizar la contraseña
                    {
                        // Contraseña actualizada correctamente, mostrar mensaje de éxito
                        PanelMensaje.Visible = true; // Muestra el panel de mensaje
                        lblMensaje.Text = "Contraseña actualizada correctamente."; // Muestra mensaje de éxito
                        lblMensaje.ForeColor = System.Drawing.Color.Green; // Cambia el color del mensaje a verde
                        PanelCambioContrasena.Visible = false; // Ocultar formulario después de cambiar la contraseña
                    }
                    else
                    {
                        // Error al actualizar la contraseña
                        lblMensaje.Text = "Hubo un error al cambiar la contraseña. Por favor, intente nuevamente."; // Muestra mensaje de error
                    }
                }
                else
                {
                    // Las contraseñas no coinciden
                    lblMensaje.Text = "Las contraseñas no coinciden."; // Muestra mensaje de contraseñas no coinciden
                }
            }
            else
            {
                // No se proporcionaron todos los datos necesarios
                lblMensaje.Text = "Por favor, complete todos los campos."; // Muestra mensaje de campos incompletos
            }
        }

        private bool ValidarToken(string token) // Método para validar el token
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString)) // Crea conexión con la base de datos
            {
                string query = "SELECT COUNT(*) FROM RestablecimientoContrasena WHERE Token = @Token AND FechaExpiracion > GETDATE()"; // Consulta para verificar el token
                SqlCommand command = new SqlCommand(query, connection); // Crea el comando SQL
                command.Parameters.AddWithValue("@Token", token); // Agrega el parámetro token

                connection.Open(); // Abre la conexión
                int count = (int)command.ExecuteScalar(); // Ejecuta la consulta y obtiene el resultado
                return count > 0; // Devuelve true si el token es válido
            }
        }

        private bool ActualizarContrasena(string token, string nuevaContrasena) // Método para actualizar la contraseña
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString)) // Crea conexión con la base de datos
            {
                string query = "UPDATE Usuarios SET Contrasena = @Contrasena WHERE Email IN (SELECT Email FROM RestablecimientoContrasena WHERE Token = @Token AND FechaExpiracion > GETDATE())"; // Consulta para actualizar la contraseña
                SqlCommand command = new SqlCommand(query, connection); // Crea el comando SQL
                command.Parameters.AddWithValue("@Contrasena", nuevaContrasena); // Agrega el parámetro nueva contraseña
                command.Parameters.AddWithValue("@Token", token); // Agrega el parámetro token

                connection.Open(); // Abre la conexión
                int result = command.ExecuteNonQuery(); // Ejecuta la consulta y obtiene el resultado
                return result > 0; // Devuelve true si la actualización fue exitosa
            }
        }
    }
}
