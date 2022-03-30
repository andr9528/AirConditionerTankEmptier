#ifndef _Functions_H
#define _Functions_H

#include <SPI.h>
// https://github.com/khoih-prog/WiFiNINA_Generic
// #include <WiFiNINA_Generic.h>
// https://github.com/arduino-libraries/WiFiNINA
#include <WiFiNINA.h>

#include "Settings.h"
#include "Secrets.h"

// https://forum.arduino.cc/t/string-array-length/153692/6
// number of items in an array
#define NUMITEMS(arg) ((unsigned int) (sizeof (arg) / sizeof (arg [0])))

// Simple variable that saves whether there is a valid Wifi module or nor. Used to limit further Wifi activity, if non is present.
bool wifiModulePresent = false;
// Status of the wifi connection.
int wifiStatus = WL_IDLE_STATUS;
// Wifi client used to connect to Mail server.
WiFiClient client;

void printMacAddress(byte mac[]) 
{
  for (int i = 5; i >= 0; i--) 
  {
    if (mac[i] < 16)     
      Serial.print("0");    

    Serial.print(mac[i], HEX);

    if (i > 0)
      Serial.print(":");
  }
  Serial.println();
}

void setupWifi() {
  wifiStatus = WiFi.status();
  if(wifiStatus == WL_NO_MODULE) 
  {
    Serial.println("WARNING: Communication with WiFi module failed!");
    wifiModulePresent = false;
  }
  else wifiModulePresent = true;

  if(wifiModulePresent == true) 
  {
    String fv = WiFi.firmwareVersion();

    if (fv < WIFI_FIRMWARE_LATEST_VERSION)     
      Serial.println("NOTE: Please upgrade the Wifi firmware");

    byte numSsid = WiFi.scanNetworks();
    Serial.println("Number of available WiFi networks discovered: " + String(numSsid));

    bool infiniteAttempts = false;
    int attempts = 1;
    if (wifiConnectionAttempts == -1) infiniteAttempts = true;
    
    while (wifiStatus != WL_CONNECTED && (infiniteAttempts == true || infiniteAttempts == false && attempts <= wifiConnectionAttempts)) 
    {
      Serial.println("Attempting (#" + String(attempts) + ") to connect to open SSID: " + wifiName);
      wifiStatus = WiFi.begin(wifiName);

      delay(10000);
      attempts++;
    }

    if (infiniteAttempts == false && attempts > wifiConnectionAttempts) 
    {
      Serial.println("WARNING: Failed to connect to wifi in allowed attempts. (Allowed Attempts: " + String(wifiConnectionAttempts) + ")");
      digitalWrite(wifiConnectedOutput, LOW);
    }     
      
    else if (wifiStatus == WL_CONNECTED) 
    {
      Serial.println("INFORMATION: Successfully connected to Wifi.");
      digitalWrite(wifiConnectedOutput, HIGH);
    } 
  }
}


/*
 * Values returned by client.connect
 * 
 * SUCCESS 1
 * TIMED_OUT -1
 * INVALID_SERVER -2
 * TRUNCATED -3
 * INVALID_RESPONSE -4
 */

void mail(String message, bool isWarning) {
  if (wifiModulePresent == true && wifiStatus != WL_CONNECTED) setupWifi();
  
  String subject = "Notification";
  if(isWarning == true) subject = "WARNING!";
  
  int connection = client.connect(mailServer, 25);
  if (connection == 1) 
  {
    Serial.println("INFORMATION: Connected to Server: " + String(mailServer));
    int recipientsCount = NUMITEMS(recipients);
    Serial.println("Writing mail to: " + String(recipientsCount) + " recipients.");
    
    for (int i = 0; i <= recipientsCount - 1; i++ )
    {
      String recipient = recipients[i];
      Serial.println("Writing mail to: " + recipient);

      // https://www.reddit.com/r/arduino/comments/ep3dhi/send_an_email/
      Serial.println("Saying Helo to server: " + String(mailServer));
      
      client.println("helo " + String(mailServer));
      client.println("MAIL FROM:" + arduinoMail);
      client.println("RCPT TO:" + recipient);
      client.println("data");
      client.println("Subject:" + subject);
      client.println("");    
      client.println(message);
      client.println("."); 
    }

    client.println("QUIT");
    client.println();
  }
  else Serial.println("WARNING: Failed to connected to Server: " + String(mailServer) + "; Returned: " + String(connection));
}

void printIP() 
{
  if (wifiModulePresent == true && wifiStatus == WL_CONNECTED) 
  {
    IPAddress ip = WiFi.localIP();
    Serial.print("INFORMATION: IP Address: ");
    Serial.println(ip);
  }
}

void printSubnet() 
{
  if (wifiModulePresent == true && wifiStatus == WL_CONNECTED) 
  {
    IPAddress gateway = WiFi.gatewayIP();
    Serial.print("INFORMATION: Gateway: ");
    Serial.println(gateway);
  }
}

void printGateway() 
{
  if (wifiModulePresent == true && wifiStatus == WL_CONNECTED) 
  {
    IPAddress subnet = WiFi.subnetMask();
    Serial.print("INFORMATION: NetMask: ");
    Serial.println(subnet);
  }
}

void printMac()
{
  byte mac[6];
  WiFi.macAddress(mac);
  Serial.print("INFORMATION: MAC address: ");
  printMacAddress(mac);
}



#endif
