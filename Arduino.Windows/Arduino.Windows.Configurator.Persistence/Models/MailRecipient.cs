using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Persistence.Core;

namespace Arduino.Windows.Configurator.Persistence.Models
{
    public class MailRecipient : IEntity
    {
        public int Id { get; set; }
        public byte[] Version { get; set; }
        public string Recipient { get; set; }
    }
}
