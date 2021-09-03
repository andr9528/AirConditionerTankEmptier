#ifndef _Settings_H
#define _Settings_H

// Mail Constants:
// Wheather or not to send a mail to receipients when the pump is activated.
const bool sendMailOnPumpActivation = false;
// Message to send to all the receipients when the pump is activated, if option for it is true.
const String pumpActivationMessage = "Water level of airconditioning tank have exceded 80% of capacity. Activating pump until empty.";
// The Warning message to send to all the receipients.
const String warningMessage = "Water level of airconditioning tank have exceded 90% of capacity!";
// The email addresses of the recipients. Comma (,) seperated.
const String recipients[] = { "andre@steenhoff.dk" };
// Email of the arduino
const String arduinoMail = "arduino@steenhoff.dk";

// Logic Constants:
// The digital port number where the input from sensor 1 (0% or more) is.
const int sensorOneInput = 2;
// The digital port number where the input from sensor 2 (80% or more) is.
const int sensorTwoeInput = 3;
// The digital port number where the input from sensor 3 (90% or more) is.
const int sensorTreeInput = 4;
// The digital port number where the output to the pump is.
const int pumpOutput = 5;
// The digital port number where output to the warning diode is.
const int warningOutput = 6;
// Delay in ms between checking if any of the sensors has changed their status. 1000 ms = 1 second.
const int delayBetweenChecks = 10000;
// Delay in ms after sending an warning mail. 1000 ms = 1 second.
const int delayAfterWarning = 30000; // 3.600.000 = 1 Hour;
// Delay in ms between blinks while in warning mode after sending an warning mail. 1000 ms = 1 second.
const int delayBetweenWarningBlinks = 1000;

#endif
