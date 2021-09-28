using Arduino.Windows.Configurator.Persistence.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Persistence.EntityFramework.Core;

namespace Arduino.Windows.Configurator.Persistence.Config
{
    public class ProfileConfig : EntityConfig<Profile>
    {
        public ProfileConfig()
        {
        }

        public override void Configure(EntityTypeBuilder<Profile> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => x.Name, "UniqueNameIndex").IsUnique();

            builder.HasMany(x => x.Secrets).WithOne(x => x.Profile).HasForeignKey(x => x.ProfileId);
            builder.HasMany(x => x.Settings).WithOne(x => x.Profile).HasForeignKey(x => x.ProfileId);
        }
    }
}
