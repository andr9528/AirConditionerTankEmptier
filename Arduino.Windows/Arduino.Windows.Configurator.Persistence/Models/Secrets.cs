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

        private const string RECIPIENTDESC = "// The email addresses of the recipients. Comma (,) seperated.";
        private const string RECIPIENTTYPE = "const String recipients[] = ";
        public ICollection<MailRecipient> Recipients { get; set; }

        private const string ARDUINOMAILDESC = "// Email of the arduino.";
        private const string ARDUINOMAILTYPE = "const String arduinoMail = ";
        public string ArduinoMail { get; set; }

        private const string MAILSERVERDESC = "// Name of the mail server to connect to on port 25.";
        private const string MAILSERVERTYPE = "char const mailServer[] = ";
        public string MailServer { get; set; }

        private const string WIFINAMEDESC = "// Name of the wifi network to connect to.";
        private const string WIFINAMETYPE = "char const wifiName[] = ";
        public string WifiName { get; set; }

        public Secrets() : base(FILENAME)
        {

        }

        public override void Build(string savePath)
        {
            BuildCheck(savePath);
            var builder = new StringBuilder();

            builder.AppendLine(HEADER());



            builder.AppendLine(FOOTER);

        }
    }
}
