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
        private const string SECTIONMAIL = "// Mail Constants:";
        private const string SECTIONLOGIC = "// Logic Constants:";
        private const string SECTIONPORT = "// Port Constants:";
        private const string SECTIONOTHER = "// Other Logic:";

        public Settings() : base(FILENAME)
        {
        }

        public int Id { get; set; }
        public byte[] Version { get; set; }
        public SettingsEntity Entity { get; set; }
        public int EntityId { get; set; }

        public const string SENDMAILONPUMPACTIVATIONDESC = "// Wheather or not to send a mail to receipients when the pump is activated.";
        private const string SENDMAILONPUMPACTIVATIONTYPE = "const bool sendMailOnPumpActivation = ";
        public bool SendMailOnPumpActivation { get; set; }

        public const string PUMPACTIVATIONMESSAGEDESC = "// Message to send to all the receipients when the pump is activated, if option for it is true.";
        private const string PUMPACTIVATIONMESSAGETYPE = "const String pumpActivationMessage = ";
        public string PumpActivationMessage { get; set; }

        public const string WARNINGMESSAGEDESC = "// The Warning message to send to all the receipients.";
        private const string WARNINGMESSAGETYPE = "const String warningMessage = ";
        public string WarningMessage { get; set; }

        public const string SENSORONEINPUTDESC = "// The digital port number where the input from sensor 1 (0% or more) is.";
        private const string SENSORONEINPUTTYPE = "const int sensorOneInput = ";
        public int SensorOneInput { get; set; }

        public const string SENSORTWOINPUTDESC = "// The digital port number where the input from sensor 2 (80% or more) is.";
        private const string SENSORTWOINPUTTYPE = "const int sensorTwoInput = ";
        public int SensorTwoInput { get; set; }

        public const string SENSORTHREEINPUTDESC = "// The digital port number where the input from sensor 3 (90% or more) is.";
        private const string SENSORTHREEINPUTTYPE = "const int sensorThreeInput = ";
        public int SensorThreeInput { get; set; }

        public const string PUMPOUTPUTDESC = "// The digital port number where the output to the pump is.";
        private const string PUMPOUTPUTTYPE = "const int pumpOutput = ";
        public int PumpOutput { get; set; }

        public const string WARNINGDIODEOUTPUTDESC = "// The digital port number where output to the warning diode is.";
        private const string WARNINGDIODEOUTPUTTYPE = "const int warningDiodeOutput = ";
        public int WarningDiodeOutput { get; set; }

        public const string SENSORONEDIODEOUTPUTDESC = "// The digital port number where the state of input input from sensor 1 is shown via a diode.";
        private const string SENSORONEDIODEOUTPUTTYPE = "const int sensorOneDiodeOutput = ";
        public int SensorOneDiodeOutput { get; set; }

        public const string SENSORTWODIODEOUTPUTDESC = "// The digital port number where the state of input input from sensor 2 is shown via a diode.";
        private const string SENSORTWODIODEOUTPUTTYPE = "const int sensorTwoDiodeOutput = ";
        public int SensorTwoDiodeOutput { get; set; }

        public const string SENSORTHREEDIODEOUTPUTDESC = "// The digital port number where the state of input input from sensor 3 is shown via a diode.";
        private const string SENSORTHREEDIODEOUTPUTTYPE = "const int sensorThreeDiodeOutput = ";
        public int SensorThreeDiodeOutput { get; set; }

        public const string WARNINGRESETBUTTONINPUTDESC = "// The digital port number where the warning reset button is connected..";
        private const string WARNINGRESETBUTTONINPUTTYPE = "const int warningResetButtonInput = ";
        public int WarningResetButtonInput { get; set; }

        public const string WARNINGSPEAKEROUTPUTDESC = "// The digital port number where the warning speaker is connected.";
        private const string WARNINGSPEAKEROUTPUTTYPE = "const int warningSpeakerOutput = ";
        public int WarningSpeakerOutput { get; set; }

        public const string DELAYBETWEENCHECKSDESC = "// Delay in ms between checking if any of the sensors has changed their status. 1000 ms = 1 second.";
        private const string DELAYBETWEENCHECKSTYPE = "const int delayBetweenChecks = ";
        public int DelayBetweenChecks { get; set; }

        public const string DELAYAFTERWARNINGDESC = "// Delay in ms after sending an warning mail. 1000 ms = 1 second.";
        private const string DELAYAFTERWARNINGTYPE = "const int delayAfterWarning = ";
        public int DelayAfterWarning { get; set; }

        public const string DELAYBETWEENWARNINGBLINKSDESC = "// Delay in ms between blinks while in warning mode after sending an warning mail. 1000 ms = 1 second.";
        private const string DELAYBETWEENWARNINGBLINKSTYPE = "const int delayBetweenWarningBlinks = ";
        public int DelayBetweenWarningBlinks { get; set; }

        public const string WIFICONNECTIONATTEMPTSDESC = "// Attempts to make when trying to connect to wifi. -1 means infinite.";
        private const string WIFICONNECTIONATTEMPTSTYPE = "const int wifiConnectionAttempts = ";
        public int WifiConnectionAttempts { get; set; }

        public override void Build(string savePath)
        {
            BuildCheck(savePath);
            var builder = new StringBuilder();

            builder.AppendLine(HEADER());

            builder.AppendLine(SECTIONMAIL);

            builder.AppendLine(SENDMAILONPUMPACTIVATIONDESC);
            builder.AppendLine($"{SENDMAILONPUMPACTIVATIONTYPE}{SendMailOnPumpActivation};");

            builder.AppendLine(PUMPACTIVATIONMESSAGEDESC);
            builder.AppendLine($"{PUMPACTIVATIONMESSAGETYPE}\"{PumpActivationMessage}\";");

            builder.AppendLine(WARNINGMESSAGEDESC);
            builder.AppendLine($"{WARNINGMESSAGETYPE}\"{WarningMessage}\";");

            builder.AppendLine();
            builder.AppendLine(SECTIONLOGIC);
            builder.AppendLine(SECTIONPORT);

            builder.AppendLine(SENSORONEINPUTDESC);
            builder.AppendLine($"{SENSORONEINPUTTYPE}{SensorOneInput};");

            builder.AppendLine(SENSORTWOINPUTDESC);
            builder.AppendLine($"{SENSORTWOINPUTTYPE}{SensorTwoInput};");

            builder.AppendLine(SENSORTHREEINPUTDESC);
            builder.AppendLine($"{SENSORTHREEINPUTTYPE}{SensorThreeInput};");

            builder.AppendLine(PUMPOUTPUTDESC);
            builder.AppendLine($"{PUMPOUTPUTTYPE}{PumpOutput};");

            builder.AppendLine(WARNINGDIODEOUTPUTDESC);
            builder.AppendLine($"{WARNINGDIODEOUTPUTTYPE}{WarningDiodeOutput};");

            builder.AppendLine(SENSORONEDIODEOUTPUTDESC);
            builder.AppendLine($"{SENSORONEDIODEOUTPUTTYPE}{SensorOneDiodeOutput};");

            builder.AppendLine(SENSORTWODIODEOUTPUTDESC);
            builder.AppendLine($"{SENSORTWODIODEOUTPUTTYPE}{SensorTwoDiodeOutput};");

            builder.AppendLine(SENSORTHREEDIODEOUTPUTDESC);
            builder.AppendLine($"{SENSORTHREEDIODEOUTPUTTYPE}{SensorThreeDiodeOutput};");

            builder.AppendLine(WARNINGRESETBUTTONINPUTDESC);
            builder.AppendLine($"{WARNINGRESETBUTTONINPUTTYPE}{WarningResetButtonInput};");

            builder.AppendLine(WARNINGSPEAKEROUTPUTDESC);
            builder.AppendLine($"{WARNINGSPEAKEROUTPUTTYPE}{WarningSpeakerOutput};");

            builder.AppendLine();
            builder.AppendLine(SECTIONOTHER);

            builder.AppendLine(DELAYBETWEENCHECKSDESC);
            builder.AppendLine($"{DELAYBETWEENCHECKSTYPE}{DelayBetweenChecks};");

            builder.AppendLine(DELAYAFTERWARNINGDESC);
            builder.AppendLine($"{DELAYAFTERWARNINGTYPE}{DelayAfterWarning};");

            builder.AppendLine(DELAYBETWEENWARNINGBLINKSDESC);
            builder.AppendLine($"{DELAYBETWEENWARNINGBLINKSTYPE}{DelayBetweenWarningBlinks};");

            builder.AppendLine(WIFICONNECTIONATTEMPTSDESC);
            builder.AppendLine($"{WIFICONNECTIONATTEMPTSTYPE}{WifiConnectionAttempts};");

            builder.AppendLine();
            builder.AppendLine(FOOTER);

        }
    }
}
