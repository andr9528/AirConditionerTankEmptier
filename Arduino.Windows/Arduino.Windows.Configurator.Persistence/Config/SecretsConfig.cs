using Arduino.Windows.Configurator.Persistence.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Persistence.EntityFramework.Core;

namespace Arduino.Windows.Configurator.Persistence.Config
{
    public class SecretsConfig : EntityConfig<Secrets>
    {
        public SecretsConfig()
        {
        }

        public override void Configure(EntityTypeBuilder<Secrets> builder)
        {
            base.Configure(builder);

            builder.HasMany(x => x.Recipients).WithOne(x => x.Secrets).HasForeignKey(x => x.SecretsId);
            builder.HasIndex(x => new { x.Entity.ProfileId, x.MailServer, x.WifiName, x.ArduinoMail }, "UniqueConfigurationIndex").IsUnique();
        }
    }
}
