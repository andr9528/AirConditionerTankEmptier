using Arduino.Windows.Configurator.Persistence.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Wolf.Utility.Core.Persistence.EntityFramework.Core;

namespace Arduino.Windows.Configurator.Persistence.Config
{
    public class SettingsConfig : EntityConfig<Settings>
    {
        public SettingsConfig()
        {
        }

        public override void Configure(EntityTypeBuilder<Settings> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => new { x.Entity.ProfileId, x.DelayAfterWarning, x.DelayBetweenChecks,
                x.DelayBetweenWarningBlinks, x.PumpActivationMessage, x.PumpOutput,
                x.SendMailOnPumpActivation, x.SensorOneDiodeOutput, x.SensorOneInput,
                x.SensorThreeDiodeOutput, x.SensorThreeInput, x.SensorTwoDiodeOutput,
                x.SensorTwoInput, x.WarningDiodeOutput, x.WarningMessage, 
                x.WarningResetButtonInput, x.WarningSpeakerOutput, x.WifiConnectionAttempts }, "UniqueConfigurationIndex").IsUnique();
        }
    }
}
