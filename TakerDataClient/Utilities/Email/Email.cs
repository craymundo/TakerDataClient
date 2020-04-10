using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace WebTakerData.Utils.Email
{
    public static class Email
    {        
        public static void EnviarCorreo(EmailDto email)
        {
            var emailEnvio = email.EmailEnvio;
            var passWord = email.PassWord;
            var host = email.Host;
            var port = email.Port;
            
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = emailEnvio,
                    Password = passWord
                };
                    
                //client.UseDefaultCredentials = false;
                client.Credentials = credential;
                client.Host = host;
                client.Port = port;
                client.EnableSsl = true;

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(email.EmailDestino));

                    if (email.EmailDestinoCC != "" && email.EmailDestinoCC != null)
                    {
                        emailMessage.CC.Add(new MailAddress(email.EmailDestinoCC));
                    }

                    if (email.DisplayNameEnvio!= "" && email.DisplayNameEnvio != null)
                    {
                        emailMessage.From = new MailAddress(email.EmailEnvio, email.DisplayNameEnvio);
                    }
                    else
                    {
                        emailMessage.From = new MailAddress(email.EmailEnvio);
                    }
                    if (email.NomImgFirma != "")
                    {
                        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(email.Body, Encoding.UTF8, MediaTypeNames.Text.Html);

                        LinkedResource img = new LinkedResource(@"wwwroot/images/Layout/"+email.NomImgFirma, MediaTypeNames.Image.Jpeg);
                        img.ContentId = "NomImgFirma";

                        htmlView.LinkedResources.Add(img);

                        emailMessage.AlternateViews.Add(htmlView);
                    }
                    else
                    {
                        emailMessage.Body = email.Body;
                    }
                    emailMessage.Subject = email.Asunto;
                    emailMessage.IsBodyHtml = email.EsBodyHtml;
                    client.Send(emailMessage);
                }
            }
        }        
    }
}
