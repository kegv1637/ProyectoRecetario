using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace ProyectoFinal
{
    // Definición de la clase parcial RecuperacionContrasena que hereda de System.Web.UI.Page
    public partial class RecuperacionContrasena : System.Web.UI.Page
    {
        // Método que se ejecuta cuando se carga la página
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Evento que se ejecuta cuando se hace clic en el botón de enviar
        protected void ButtonEnviar_Click(object sender, EventArgs e)
        {
            // Obtiene el valor del control de texto de correo electrónico
            string email = TextBoxEmail.Text.Trim();

            // Verifica si el correo electrónico está vacío
            if (string.IsNullOrEmpty(email))
            {
                LabelMensaje.Text = "Por favor, introduzca su correo electrónico.";
                return;
            }

            // Verifica si el correo electrónico está registrado
            if (VerificarCorreoElectronico(email))
            {
                // Genera un token
                string token = GenerarToken();
                // Almacena el token en la base de datos
                if (AlmacenarToken(email, token))
                {
                    // Genera un enlace de restablecimiento de contraseña
                    string enlaceRestablecimiento = GenerarEnlaceRestablecimiento(token);
                    // Envía un correo electrónico con el enlace de restablecimiento
                    if (EnviarCorreoRestablecimiento(email, enlaceRestablecimiento))
                    {
                        LabelMensaje.ForeColor = System.Drawing.Color.Green;
                        LabelMensaje.Text = "Si el correo está registrado, recibirá un correo con las instrucciones para restablecer su contraseña.";
                    }
                    else
                    {
                        LabelMensaje.Text = "Hubo un error al enviar el correo. Por favor, intente nuevamente.";
                    }
                }
                else
                {
                    LabelMensaje.Text = "Hubo un error al generar el enlace de restablecimiento. Por favor, intente nuevamente.";
                }
            }
            else
            {
                LabelMensaje.Text = "El correo electrónico no está registrado.";
            }
        }

        // Método que verifica si el correo electrónico está registrado en la base de datos
        private bool VerificarCorreoElectronico(string email)
        {
            // Crea una conexión a la base de datos usando la cadena de conexión del archivo de configuración
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString))
            {
                string query = "SELECT COUNT(*) FROM Usuarios WHERE Email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }

        // Método que genera un token único
        private string GenerarToken()
        {
            return Guid.NewGuid().ToString();
        }

        // Método que almacena el token en la base de datos
        private bool AlmacenarToken(string email, string token)
        {
            // Crea una conexión a la base de datos usando la cadena de conexión del archivo de configuración
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["RecetarioDB1ConnectionString"].ConnectionString))
            {
                string query = "INSERT INTO RestablecimientoContrasena (Email, Token, FechaExpiracion) VALUES (@Email, @Token, @FechaExpiracion)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Token", token);
                command.Parameters.AddWithValue("@FechaExpiracion", DateTime.Now.AddHours(1)); // El token expira en 1 hora

                connection.Open();
                int result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        // Método que genera un enlace de restablecimiento de contraseña
        private string GenerarEnlaceRestablecimiento(string token)
        {
            string urlBase = Request.Url.GetLeftPart(UriPartial.Authority);
            return $"{urlBase}/RestablecerContrasena.aspx?token={token}";
        }

        // Método que envía un correo electrónico con el enlace de restablecimiento de contraseña
        private bool EnviarCorreoRestablecimiento(string email, string enlaceRestablecimiento)
        {
            try
            {
                // Configura los detalles del correo electrónico
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("CORREO", "Nombre Usu"); // Remitente
                mail.To.Add(email); // Destinatario
                mail.Subject = "Restablecer contraseña";
                mail.Body = $"Haga clic en el siguiente enlace para restablecer su contraseña: <a href='{enlaceRestablecimiento}'>Restablecer contraseña</a>";
                mail.IsBodyHtml = true;

                // Configuración del cliente SMTP
                SmtpClient smtp = new SmtpClient("smtp-mail.outlook.com", 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("correo", "contraseña"); // Cambiar por tu contraseña
                smtp.EnableSsl = true;
                smtp.Timeout = 15000; // Tiempo de espera en milisegundos

                // Envía el correo electrónico
                smtp.Send(mail);

                return true;
            }
            catch (Exception ex)
            {
                // Maneja el error
                LabelMensaje.Text = $"Hubo un error al enviar el correo: {ex.Message}";
                return false;
            }
        }
    }
}
