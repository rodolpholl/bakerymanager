using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;


namespace BakeryManager.Infraestrutura.Helpers
{
    public static class EmailHelper
    {
        /// <summary>
        /// Enviar um e-mail para.
        /// </summary>
        /// <param name="host">Host do servidor SMPT.</param>
        /// <param name="port">Porta do servidor SMTP.</param>
        /// <param name="from">Remetente da mensagem.</param>
        /// <param name="to">Destinatários da mensagem. Separar lista por vírgula(,).</param>
        /// <param name="cc">Destinatários em cópia. Separar lista por vírgula(,).</param>
        /// <param name="cco">Destinatários em cópia oculta. Separar lista por vírgula(,).</param>
        /// <param name="subject">Assunto do E-mail.</param>
        /// <param name="message">Corpo da Mensagem.</param>
        /// <param name="userName">Login do Usuário no serviço de SMTP</param>
        /// <param name="password">Password do Usuário no serviço de SMTP</param>
        /// <param name="useSSL">Indica se o protocolo SSL será utilizado no envido e-mail</param>
        /// <param name="ConteudoHTML">Indica se o conteúdo da mensagem usará texto HTML</param>
        /// <param name="ReplyTo">Indica um email para qual uma eventual resposta deverá ser encaminhada</param>
        public static void EnviarEmail(string host, int port, string from, string to, string cc, string cco, string subject, string message, bool AuthenticationRequested, string userName, string password, bool useSSL, bool ConteudoHTML,  IEnumerable<MailAddress> ReplyTo = null)
        {

            if (!IsValidEmail(from))
                throw new Exception("Email do remetente inválido! Verifique e tente novamente.");
            
            if (!IsValidEmail(to))
                throw new Exception("Email do destinatário principal inválido! Verifique e tente novamente.");


            var mensagem = new MailMessage(from, to, subject, message) { IsBodyHtml = ConteudoHTML };

            if (ReplyTo != null)
                foreach (var rp in ReplyTo)
                    mensagem.ReplyToList.Add(rp);

            if (!string.IsNullOrWhiteSpace(cc))
            {
                foreach (var copia in cc.Split(new char[] {','}))
                {
                    if (!IsValidEmail(copia))
                        throw new Exception(
                            string.Format(
                                "O email {0}, contido na lista de de cópia, é inválido! Altere e tente novamente.",
                                copia));

                    mensagem.CC.Add(new MailAddress(copia));
                }
            }

            if (!string.IsNullOrWhiteSpace(cco))
            {
                foreach (var copia in cco.Split(new char[] { ',' }))
                {
                    if (!IsValidEmail(copia))
                        throw new Exception(
                            string.Format(
                                "O email {0}, contido na lista de de cópia oculta, é inválido! Altere e tente novamente.",
                                copia));

                    mensagem.Bcc.Add(new MailAddress(copia));
                }
            }

            var smtp = new SmtpClient
                           {
                               Host = host,
                               EnableSsl = useSSL,
                               UseDefaultCredentials = !AuthenticationRequested
                               
                           };

            if (port > 0)
                smtp.Port = port;

            if (AuthenticationRequested)
            {
                if (string.IsNullOrWhiteSpace(userName))
                    throw new Exception("Informe o usuário para autenticação do e-mail");

                else if(string.IsNullOrWhiteSpace(password))

                    throw new Exception("Informe a senha do usuário para autenticação do e-mail");

                else
                    smtp.Credentials = new NetworkCredential(userName, password);
            }
           
         
                           

            smtp.Send(mensagem);

            smtp.Dispose();
            mensagem.Dispose();
        }

        /// <summary>
        /// Enviar um e-mail utilizanod uma conta do gmail.
        /// </summary>
        /// <param name="from">Remetente da mensagem.</param>
        /// <param name="to">Destinatários da mensagem. Separar lista por vírgula(,).</param>
        /// <param name="cc">Destinatários em cópia. Separar lista por vírgula(,).</param>
        /// <param name="cco">Destinatários em cópia oculta. Separar lista por vírgula(,).</param>
        /// <param name="subject">Assunto do E-mail.</param>
        /// <param name="message">Corpo da Mensagem.</param>
        /// <param name="password">Password do Usuário no serviço de SMTP</param>
        /// <param name="ConteudoHTML">Indica se o conteúdo da mensagem usará texto HTML</param>
        /// /// <param name="ReplyTo">Indica um email para qual uma eventual resposta deverá ser encaminhada</param>
        public static void EnviarEmailGmail(string from, string to, string cc, string cco, string subject, string message, string password, bool ConteudoHTML, IEnumerable<MailAddress> ReplyTo = null )
        {

            if (!from.Contains("@gmail.com"))
                throw new Exception("E-mail do remetente inválido! Utilize um e-amil do domínimo @gmail.com ou utilize o método Enviar Email");


            EnviarEmail("smtp.gmail.com", 587, from, to, cc, cco, subject, message, true,from, password, true, ConteudoHTML, ReplyTo);
        }

        public static bool IsValidEmail(string Email)
        {
            
            // Returna true se "Email" estiver num formato correto. 
            try {
                return Regex.IsMatch(Email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException) {
                return false;
            }
        }
    }
}
