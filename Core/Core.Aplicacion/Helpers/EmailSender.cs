using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Aplicacion.Helpers
{
    public static class EmailSender
    {
        private static readonly System.Net.Mail.SmtpClient _cliente = new System.Net.Mail.SmtpClient()
        {
            Credentials = new System.Net.NetworkCredential("ligthfootteam@gmail.com", "LightFoot2021"),
            Port = 587,
            EnableSsl = true,
            Host = "smtp.gmail.com",
        };


        public static bool Sender(string to, string motivo)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            //header 
            msg.To.Add(to);
            // msg.To.Add("jrosas1982@gmail.com");
            msg.Subject = motivo;
            // msg.Subject = "Consulta";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            //msg.Bcc.Add("otro email");
            //body
            string Mensaje = "Bienvenido " + to + " gracias por su compra, cualquier duda o consulta puede realizarla a traves de nuesto email : ligthfootteam@gmail.com ";
            msg.Body = Mensaje;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.From = new System.Net.Mail.MailAddress("ligthfootteam@gmail.com");
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
            cliente.Credentials = new System.Net.NetworkCredential("ligthfootteam@gmail.com", "LightFoot2021");
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.Host = "smtp.gmail.com";

            try
            {
                cliente.Send(msg);

                return true;
            }
            catch (Exception e)
            {
                var errorms = e.Message;
                return false;
            }
        }

        public static async Task<IEnumerable<EmailResult>> SendBatchEmailsAsync(string motivo, string mensaje, params string[] destinatarios)
        {
            var emailResult = new List<EmailResult>();

            foreach (var destinatario in destinatarios)
            {
                emailResult.Add(new EmailResult(await SendEmailAsync(destinatario, mensaje, motivo), destinatario));
            }
            return emailResult;
        }

        private static async Task<bool> SendEmailAsync(string motivo, string mensaje, string destinatario)
        {

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            //header 
            msg.To.Add(destinatario);
            // msg.To.Add("jrosas1982@gmail.com");
            msg.Subject = motivo;
            // msg.Subject = "Consulta";
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            //msg.Bcc.Add("otro email");
            //body
            string Mensaje = "Bienvenido " + destinatario + " gracias por su compra, cualquier duda o consulta puede realizarla a traves de nuesto email : ligthfootteam@gmail.com ";
            msg.Body = Mensaje;
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.From = new System.Net.Mail.MailAddress("ligthfootteam@gmail.com");
            try
            {
                await _cliente.SendMailAsync(msg);
                return true;
            }
            catch (Exception e)
            {
                var errorms = e.Message;
                return false;
            }
        }

        public class EmailResult
        {
            public EmailResult(bool exito, string email)
            {
                Exito = exito;
                Email = email;
            }

            bool Exito { get; set; }
            string Email { get; set; }
        }
    }
}
