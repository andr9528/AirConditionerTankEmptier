using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Persistence.Core;

namespace Arduino.Windows.Configurator.Persistence.Models
{
    public class Profile : IEntity
    {
        public int Id { get; set; }
        public byte[] Version { get; set; }
        public ICollection<SettingsEntity> Settings { get; set; }
        public ICollection<SecretsEntity> Secrets { get; set; }
        public string FunctionsFilePath { get; set; }
        public string INOFilePath { get; set; }
        public string Name { get; set; }
    }
}
