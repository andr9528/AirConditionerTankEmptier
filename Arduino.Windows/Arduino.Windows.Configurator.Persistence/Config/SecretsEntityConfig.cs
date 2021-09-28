using Arduino.Windows.Configurator.Persistence.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Persistence.EntityFramework.Core;

namespace Arduino.Windows.Configurator.Persistence.Config
{
    public class SecretsEntityConfig : EntityConfig<SecretsEntity>
    {
        public SecretsEntityConfig()
        {
        }

        public override void Configure(EntityTypeBuilder<SecretsEntity> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => x.Name, "UniqueNameIndex").IsUnique();

            builder.HasOne(x => x.FileBuilder).WithOne(x => x.Entity).HasForeignKey<Secrets>(x => x.EntityId);
        }
    }
}
