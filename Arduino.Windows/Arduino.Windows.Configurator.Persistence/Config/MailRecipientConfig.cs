using Arduino.Windows.Configurator.Persistence.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Persistence.EntityFramework.Core;

namespace Arduino.Windows.Configurator.Persistence.Config
{
    public class MailRecipientConfig : EntityConfig<MailRecipient>
    {
        public MailRecipientConfig()
        {
        }

        public override void Configure(EntityTypeBuilder<MailRecipient> builder)
        {
            base.Configure(builder);


        }
    }
}
