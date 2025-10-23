using Infrastruture.Worker.DTO;
using Infrastruture.Worker.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Infrastruture.Worker.Setting;

namespace Infrastruture.Worker.Services
{
    public class SendEmailService : ISendEmailService
    {
        private readonly ILogger<SendEmailService> _logger;
        private readonly EmailSetting _emailSettings;
        private readonly IGeneratorReportService _pdfGenerator;
        public SendEmailService(ILogger<SendEmailService> logger, IOptions<EmailSetting> emailSetting, IGeneratorReportService pdfGenerator)
        {
            _logger = logger;
            _emailSettings = emailSetting.Value;
            _pdfGenerator = pdfGenerator;
        }

        private string BodyEmail(EmailDTO dto)
        {
            var TextoEmail = $@"<!DOCTYPE html>
                                        <html lang=""pt"">

                                        <head>
                                            <meta charset=""UTF-8"">
                                            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                            <title>{dto.EmailSubject}</title>
                                            <style>
                                                body {{
                                                    font-family: Arial, sans-serif;
                                                    background-color: #ffffff;
                                                    padding: 20px;
                                                }}

                                                .container {{
                                                    max-width: 600px;
                                                    margin: 0 auto;
                                                    background-color: #ffffff;
                                                    padding: 20px;
                                                    border-radius: 5px;
                                                    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
                                                }}

                                                h1 {{
                                                    color: #000;
                                                }}

                                                p {{
                                                    color: #000;
                                                }}
                                            </style>
                                        </head>

                                        <body>
                                            <div class=""container"">
                                                {dto.EmailText}
                                            </div>
                                        </body>

                                        </html>";
            return TextoEmail;
        }
        public async Task<bool> SendEmailAsync(EmailDTO dto)
        {
            try
            {
                using var smtpClient = new SmtpClient();
                await smtpClient.ConnectAsync(_emailSettings.HostSMT, _emailSettings.Port, SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(_emailSettings.User, _emailSettings.PassWord);
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(_emailSettings.From, _emailSettings.From));
                emailMessage.To.Add(MailboxAddress.Parse(dto.To));
                emailMessage.Subject = dto.EmailSubject;
                var bodyBuilder = new BodyBuilder();
                // Gerar o relatorio em PDF
                var pdfBytes = await _pdfGenerator.GenerateReservaPdfAsync(dto);
                //Anexar o PDF
                bodyBuilder.Attachments.Add(dto.User+"reserva.pdf", pdfBytes, new ContentType("application", "pdf"));
                bodyBuilder.HtmlBody = BodyEmail(dto);
                emailMessage.Body = bodyBuilder.ToMessageBody();
                await smtpClient.SendAsync(emailMessage);
                await smtpClient.DisconnectAsync(true);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao enviar email para {dto.To}. Erro: {ex.Message}");
                return false;
            }
        }
    }
}
