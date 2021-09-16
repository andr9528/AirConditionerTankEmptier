using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wolf.Utility.Core.Startup.Assist;

namespace Arduino.Windows.Api.Mail.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : ControllerBase
    {
        private ILoggerManager _logger;

        public MailController(ILoggerManager logger = null)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.SetCaller(GetType().FullName);
        }

        [HttpGet]
        public string Get() 
        {
            _logger.LogInfo($"Testing Logger 123");

            return GetClientIp();
        }

        /// <summary>
        /// Implemented Following https://blog.elmah.io/how-to-send-emails-from-csharp-net-the-definitive-tutorial/
        /// Expects 1 string containing a | between the receiver and message.
        /// Expects a @ in the receiver.
        /// </summary>
        /// <returns></returns>
        [HttpPost]        
        public IActionResult Post([FromQuery] string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                _logger.LogError($"Input was null or empty");
                return new BadRequestResult();
            }
            if (!input.Contains('|'))
            {
                _logger.LogError($"Input was missing divider");
                return new BadRequestResult();
            }

            // Write the Ip of caller.
            _logger.LogInfo($"{DateTime.Now} - Ip of caller => {GetClientIp()}");

            var divide = input.Split('|');

            string receiver = divide[0].Trim();
            string message = divide[1].Trim();

            if (!receiver.Contains('@'))
            {
                _logger.LogError($"Receiver was missing '@' sign");
                return new BadRequestResult();
            }

            // Writes the receiver and message to the ILogger.
            _logger.LogInfo($"Mail recepient => {receiver}; Message => {message}");

            var sender = "arduino@steenhoff.dk";
            var mail = new MailMessage()
            {
                From = new MailAddress(sender, $"Sensor Arduino"),
                Subject = $"Air Conditioner Tank Overflowing",
                To = { new MailAddress(receiver, $"Handler") },
                Body = message,
                Priority = MailPriority.High
            };

            using var smtp = new SmtpClient();
            smtp.Send(mail);

            _logger.LogInfo($"Mail Sent to {receiver}.");

            return Ok();
        }

        private string GetClientIp(HttpRequest request = null)
        {
            request ??= Request;

            return request.HttpContext.Connection.RemoteIpAddress.ToString();
        }

    }
}
