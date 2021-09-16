using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Arduino;
using Wolf.Utility.Core.Persistence.Core;

namespace Arduino.Windows.Configurator.Persistence.Models
{
    public class Settings : HeaderBuilder, IEntity
    {
        private const string FILENAME = "Settings";
        
        public Settings() : base(FILENAME)
        {
        }

        public int Id { get; set; }
        public byte[] Version { get; set; }

        public override void Build(string savePath)
        {
            BuildCheck(savePath);
            var builder = new StringBuilder();

            builder.AppendLine(HEADER());



            builder.AppendLine(FOOTER);

        }
    }
}
