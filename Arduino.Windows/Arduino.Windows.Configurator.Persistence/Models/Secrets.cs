using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Arduino;
using Wolf.Utility.Core.Persistence.Core;

namespace Arduino.Windows.Configurator.Persistence.Models
{
    public class Secrets : HeaderBuilder, IEntity
    {
        private const string FILENAME = "Secrets";

        public int Id { get; set; }
        public byte[] Version { get; set; }
        public SecretsEntity Entity { get; set; }
        public int EntityId { get; set; }

        public const string RECIPIENTDESC = "// The email addresses of the recipients. Comma (,) seperated.";
        private const string RECIPIENTTYPE = "const String recipients[] = ";
        public ICollection<MailRecipient> Recipients { get; set; }

        public const string ARDUINOMAILDESC = "// Email of the arduino.";
        private const string ARDUINOMAILTYPE = "const String arduinoMail = ";
        public string ArduinoMail { get; set; }

        public const string MAILSERVERDESC = "// Name of the mail server to connect to on port 25.";
        private const string MAILSERVERTYPE = "char const mailServer[] = ";
        public string MailServer { get; set; }

        public const string WIFINAMEDESC = "// Name of the wifi network to connect to.";
        private const string WIFINAMETYPE = "char const wifiName[] = ";
        public string WifiName { get; set; }

        public Secrets() : base(FILENAME)
        {

        }

        public override void Build(string savePath)
        {
            BuildCheck(savePath);
            var recipientsBuilder = new StringBuilder();

            recipientsBuilder.Append("{ ");
            foreach (var recipient in Recipients)
            {
                recipientsBuilder.Append($"\"{recipient.Recipient}\", ");
            }
            recipientsBuilder.Length--;
            recipientsBuilder.Length--;
            recipientsBuilder.Append(" }");

            var builder = new StringBuilder();

            builder.AppendLine(HEADER());

            builder.AppendLine(RECIPIENTDESC);
            builder.AppendLine($"{RECIPIENTTYPE}{recipientsBuilder};");

            builder.AppendLine(ARDUINOMAILDESC);
            builder.AppendLine($"{ARDUINOMAILTYPE}{ArduinoMail};");

            builder.AppendLine(MAILSERVERDESC);
            builder.AppendLine($"{MAILSERVERTYPE}{MailServer};");

            builder.AppendLine(WIFINAMEDESC);
            builder.AppendLine($"{WIFINAMETYPE}{WifiName};");

            builder.AppendLine();
            builder.AppendLine(FOOTER);

        }
    }
}
