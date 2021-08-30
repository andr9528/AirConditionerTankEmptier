// Mail Constants:
// Wheather or not to send a mail to receipients when the pump is activated.
bool sendMailOnPumpActivation = false;
// Message to send to all the receipients when the pump is activated, if option for it is true.
String pumpActivationMessage = "Water level of airconditioning tank have exceded 80% of capacity. Activating pump until empty.";
// The Warning message to send to all the receipients.
String warningMessage = "Water level of airconditioning tank have exceded 90% of capacity!";
// The email addresses of the recipients. Comma (,) seperated.
String recipients[] = { "mima@steenhoff.dk" };
// Ip of where the companion Api is being run.
String ip = "0.0.0.0";

// Logic Constants:
// The digital port number where the input from sensor 1 (0% or more) is.
int sensorOneInput = 1;
// The digital port number where the input from sensor 2 (80% or more) is.
int sensorTwoeInput = 2;
// The digital port number where the input from sensor 3 (90% or more) is.
int sensorTreeInput = 3;
// The digital port number where the output to the pump is.
int pumpOutput = 4;
// Delay in ms between checking if any of the sensors has changed their status. 1000 ms = 1 second.
int delayBetweenChecks = 60000;

// Static Constants - Should (In theory) never be changed.
// The Https method called on the Api for sending mails.
String httpMethod = "POST";
// The Http address called to call the Api
String httpAddress = ip + "/mail";

// Variables - Expected to change during code execution
// The current state of sensor one.
bool sensorOneState = false;
// The current state of sensor two.
bool sensorTwoState = false;
// The current state of sensor two.
bool sensorThreeState = false;
// Wheather or not the pump is currently active.
bool isPumpActive = false;

void mail(String message) {
  for (int i = 0; i < sizeof(recipients) - 1; i++ )
  {
    String input = "?input=" + recipients[i] + "|" + message;
    String requestAddress = httpAddress + input;

    // INSERT LOGIC TO SEND HTTP POST REQUEST TO API WITH ABOVE ADDRESS
  }
  return;
}

void setup() {
  // put your setup code here, to run once:

  // INSERT SETUP LOGIC FOR WIFI
}

void loop() {
  // put your main code here, to run repeatedly:

  // INSERT LOGIC TO SET STATE FROM SENSORS

  if (isPumpActive == true) // Is the pump Active?
  {
    if (sensorOneState == false) // Should it still be active? No? Then enter and Disable it.
    {
      // INSERT LOGIC TO DISABLE PUMP

      isPumpActive = false;
    }
  }

  if (sensorOneState == true && sensorTwoState == true) // Should the pump be active? Yes? Then enter and Enable it.
  {
    // INSERT LOGIC TO ENABLE PUMP

    isPumpActive = true;

    if (sendMailOnPumpActivation) // Should recipients be notified about the pump starting? Yes? Then enter and send a notification mail to recipients.
    {
      mail(pumpActivationMessage);
    }
  }
  
  if (sensorThreeState == true) // Is water not being removed? Yes? Then enter and send a warning mail to recipients.
  {
    mail(warningMessage);
  }

  delay(delayBetweenChecks);
}
