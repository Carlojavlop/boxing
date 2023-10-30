using System.Net.Mail;
using System.Net;

namespace boxing.Data
{
    public class Email
    {
        public void Enviar(string correo, string token)
        {
            Correo(correo, token);
        }

        void Correo(string correo_receptor, string token)
        {
            string correo_emisor = "youremail@hotmail.com";
            string clave_emisor = "youraccesscode";

            MailAddress receptor = new(correo_receptor);
            MailAddress emisor = new(correo_emisor);

            MailMessage email = new MailMessage(emisor, receptor);
            email.Subject = "account validation";
            email.Body = "To activate your account, click the link below: https://localhost:7146/Cuenta/Token?valor=" + token;

            SmtpClient smtp = new();
            smtp.Host = "aloga.carlo.lopez@gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential(correo_emisor, clave_emisor);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(email);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}
