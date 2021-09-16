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
// the state of the warning reset last loop.
bool warningResetLast = false;
// the state of the warning reset current loop.
bool warningResetCurrent = false;
// wheather or not the warning speaker trigger.
bool triggerWarningSpeaker = true;

void setupPins()
{
  Serial.println("Setting up Pins...");

  pinMode(sensorOneInput, INPUT);
  pinMode(sensorTwoeInput, INPUT);
  pinMode(sensorTreeInput, INPUT);
  pinMode(pumpOutput, OUTPUT);
  pinMode(warningDiodeOutput, OUTPUT);
  
  pinMode(sensorOneDiodeOutput, OUTPUT);
  pinMode(sensorTwoDiodeOutput, OUTPUT);
  pinMode(sensorThreeDiodeOutput, OUTPUT);
  pinMode(warningResetButtonInput, INPUT);
  pinMode(warningSpeakerOutput, OUTPUT);

  Serial.println("Completed pin setup...");
}

void readSensors() 
{
  sensorOneState = digitalRead(sensorOneInput);
  sensorTwoState = digitalRead(sensorTwoeInput);
  sensorThreeState = digitalRead(sensorTreeInput);

  // Write States to Serial, for confirmation.
  Serial.println("Sensor 1: " + String(sensorOneState));
  Serial.println("Sensor 2: " + String(sensorTwoState));
  Serial.println("Sensor 3: " + String(sensorThreeState));

  digitalWrite(sensorOneDiodeOutput, sensorOneState);
  digitalWrite(sensorTwoDiodeOutput, sensorTwoState);
  digitalWrite(sensorThreeDiodeOutput, sensorThreeState);
}

void setup() {
  // put your setup code here, to run once:
  Serial.begin(9600);

  printMac();
  
  setupWifi();
  printIP();
  printSubnet();
  printGateway();

  setupPins();
}

void loop() {
  // put your main code here, to run repeatedly:

  Serial.println("Loops: " + String(loops));

  readSensors();

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
      mail(pumpActivationMessage, false);
    }
  }

  if (sensorThreeState == true) // Is water not being removed? Yes? Then enter and send a warning mail to recipients.
  {
    Serial.println("Warning recipients about Tank overflow...");    
    mail(warningMessage, true);

    warningModeTimeLeft = delayAfterWarning;
    triggerWarningSpeaker = true;
    warningResetCurrent = false;
    warningResetLast = false;

    while (warningModeTimeLeft > 0) 
      { 
        Serial.println("Warning time Remaining:" + String(warningModeTimeLeft));
             
        if(triggerWarningSpeaker == true) digitalWrite(warningSpeakerOutput, HIGH);    
        digitalWrite(warningDiodeOutput, HIGH);
        delay(delayBetweenWarningBlinks);

        if(triggerWarningSpeaker == true) digitalWrite(warningSpeakerOutput, LOW);
        digitalWrite(warningDiodeOutput, LOW);
        delay(delayBetweenWarningBlinks);

        warningModeTimeLeft = warningModeTimeLeft - (2 * delayBetweenWarningBlinks);

        // Holding the reset button for 1 cycle, ie 1 second, diables the speaker. Holding it for 2 cycles, ie 2 seconds, resets the warning completely.
        warningResetCurrent = digitalRead(warningResetButtonInput);
        Serial.println("Warning Reset Button Pressed Current: " + String(warningResetCurrent) + "; Warning Reset Button Pressed Last:" + String(warningResetLast));
        if(warningResetCurrent == true) triggerWarningSpeaker = false;
        if(warningResetCurrent == true && warningResetLast == true) warningModeTimeLeft = 0;
        warningResetLast = warningResetCurrent;
      }
  }

  delay(delayBetweenChecks);
  loops++;
  Serial.println();
}
