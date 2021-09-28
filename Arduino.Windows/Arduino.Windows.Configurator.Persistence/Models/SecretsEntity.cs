using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Persistence.Core;

namespace Arduino.Windows.Configurator.Persistence.Models
{
    public class SecretsEntity : IEntity
    {
        public int Id { get; set; }
        public byte[] Version { get; set; }
        public Secrets FileBuilder { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Profile Profile { get; set; }
        public int ProfileId { get; set; }
    }
}
