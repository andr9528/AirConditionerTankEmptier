#include <SPI.h>
#include <WiFiNINA.h>

#include "Functions.h"
#include "Settings.h"
#include "Secrets.h"

// Variables - Expected to change during code execution
// The current state of sensor one.
bool sensorOneState = false;
// The current state of sensor two.
bool sensorTwoState = false;
// The current state of sensor two.
bool sensorThreeState = false;
// Wheather or not the pump is currently active.
bool isPumpActive = false;
// simple count for amount of loops it has been throught, for testing.
int loops = 0;
// time in ms left for current warning mode.
int warningModeTimeLeft = 0;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);

  Serial.println("Setting up Pins...");

  pinMode(sensorOneInput, INPUT);
  pinMode(sensorTwoeInput, INPUT);
  pinMode(sensorTreeInput, INPUT);
  pinMode(pumpOutput, OUTPUT);
  pinMode(warningOutput, OUTPUT);

  Serial.println("Completed pin setup...");
  
  // INSERT SETUP LOGIC FOR WIFI
}

void loop() {
  // put your main code here, to run repeatedly:

  Serial.println("Loops: " + String(loops));

  sensorOneState = digitalRead(sensorOneInput);
  sensorTwoState = digitalRead(sensorTwoeInput);
  sensorThreeState = digitalRead(sensorTreeInput);

  // Write States to Serial, for confirmation.
  Serial.println("Sensor 1: " + String(sensorOneState));
  Serial.println("Sensor 2: " + String(sensorTwoState));
  Serial.println("Sensor 3: " + String(sensorThreeState));

  if (isPumpActive == true) // Is the pump Active?
  {
    if (sensorOneState == false) // Should it still be active? No? Then enter and Disable it.
    {
      Serial.println("Disableing Pump...");
      // Disables Pump
      digitalWrite(pumpOutput, LOW);

      isPumpActive = false;
    }
  }

  if (sensorOneState == true && sensorTwoState == true) // Should the pump be active? Yes? Then enter and Enable it.
  {
    Serial.println("Enableing Pump...");
    // Enables Pump
    digitalWrite(pumpOutput, HIGH);

    isPumpActive = true;

    if (sendMailOnPumpActivation) // Should recipients be notified about the pump starting? Yes? Then enter and send a notification mail to recipients.
    {
      Serial.println("Notifying recipients about Pump activation...");
      mail(pumpActivationMessage);
    }
  }

  if (sensorThreeState == true) // Is water not being removed? Yes? Then enter and send a warning mail to recipients.
  {
    Serial.println("Warning recipients about Tank overflow...");
    
    mail(warningMessage);

    warningModeTimeLeft = delayAfterWarning;

    while (warningModeTimeLeft > 0) 
      {
        digitalWrite(warningOutput, HIGH);
        delay(delayBetweenWarningBlinks);

        digitalWrite(warningOutput, LOW);
        delay(delayBetweenWarningBlinks);

        warningModeTimeLeft = warningModeTimeLeft - (2 * delayBetweenWarningBlinks);
      }
  }

  delay(delayBetweenChecks);
  loops++;
  Serial.println();
}
