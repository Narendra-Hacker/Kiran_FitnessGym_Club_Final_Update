using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

[ApiController]
[Route("api")]
public class EmailController : ControllerBase
{
    private readonly ILogger<EmailController> _logger;

    public EmailController(ILogger<EmailController> logger)
    {
        _logger = logger;
    }

    [HttpPost("sendEmail")]
    public async Task<IActionResult> SendEmail([FromForm] EmailRequest emailRequest)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("G Narendra Kumar", "padmanabhamkiran9@gmail.com"));
            message.To.Add(new MailboxAddress("Recipient's Name", emailRequest.To));
            message.Subject = "Receipt Details";

            //System.Console.WriteLine(message);
            var body = new TextPart("plain")
            {
                Text = "Please find your Receipt below."
            };

            var multipart = new Multipart("mixed");
            multipart.Add(body);

            if (emailRequest.Attachment != null && emailRequest.Attachment.Length > 0)
            {
                var attachment = new MimePart(emailRequest.Attachment.ContentType)
                {
                    Content = new MimeContent(emailRequest.Attachment.OpenReadStream(), ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = emailRequest.Attachment.FileName
                };

                multipart.Add(attachment);
            }


            else
            {
                return BadRequest("Attachment not found");
            }

            message.Body = multipart;
            // Console.WriteLine(message);
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("padmanabhamkiran9@gmail.com", "yxydgmgfpguwojgz");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                Console.WriteLine("HI");

            }

            Console.WriteLine("Email sent");
            _logger.LogInformation("Email sent successfully");
            return Ok("Email sent successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to send email: {ex.Message}");
            if (ex.InnerException != null)
            {
                _logger.LogError($"Inner Exception: {ex.InnerException.Message}");
            }
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }

    }
}

public class EmailRequest
{
    public string To { get; set; }
    public IFormFile Attachment { get; set; }
}
