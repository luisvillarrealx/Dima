using Dima_Api.Entities;
using Dima_Api.ModeloBD;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace Dima_Api.Models
{
    public class LogsModel
    {

        public void RegistrarError(LogsEnt logs)
        {
            using (var conexion = new docmedEntities())
            {
                conexion.RegistrarError(logs.descripcion, logs.idUsuario);
            }
        }

        public void EnviarCorreo(string destinatario, string asunto, string body)
        {
            try
            {
                String userName = ConfigurationManager.AppSettings["userName"].ToString();
                String password = ConfigurationManager.AppSettings["password"].ToString();
                MailMessage msg = new MailMessage(userName, destinatario);
                msg.Subject = asunto;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(body);
                msg.Body = sb.ToString();
                SmtpClient SmtpClient = new SmtpClient();
                SmtpClient.Credentials = new System.Net.NetworkCredential(userName, password);
                SmtpClient.Host = "smtp.office365.com";
                SmtpClient.Port = 587;
                SmtpClient.EnableSsl = true;
                SmtpClient.Send(msg);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }

    }
}