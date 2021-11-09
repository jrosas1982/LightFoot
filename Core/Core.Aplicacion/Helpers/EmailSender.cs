using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Core.Aplicacion.Helpers
{
    public static class EmailSender
    {
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

        public static async Task<IEnumerable<EmailResult>> SendEmail(string asunto, string mensaje, params string[] destinatarios)
        {
            var emailResult = new List<EmailResult>();

            foreach (var destinatario in destinatarios)
            {
                emailResult.Add(new EmailResult(await SendEmail(asunto, mensaje, destinatario), destinatario));
            }
            return emailResult;
        }

        private static async Task<bool> SendEmail(string asunto, string mensaje, string destinatario)
        {

            using (MailMessage msg = new MailMessage())
            {
                msg.From = new MailAddress("TrifulcaLightFoot@gmail.com");
                msg.To.Add(destinatario);
                msg.Subject = asunto;
                msg.Body = mensaje;
                msg.IsBodyHtml = true;
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                try
                {
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("TrifulcaLightFoot@gmail.com", "LightFoot2021");
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(msg);
                    }
                    return true;
                }
                catch (Exception e)
                {
                    var errorms = e.Message;
                    return false;
                }
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
