#ifndef _Functions_H
#define _Functions_H

#include <SPI.h>
#include <WiFiNINA.h>

#include "Settings.h"
#include "Secrets.h"

// Simple variable that saves whether there is a valid Wifi module or nor. Used to limit further Wifi activity, if non is present.
bool wifiModulePresent = false;
// Status of the wifi connection.
int wifiStatus = WL_IDLE_STATUS;

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

void mail(String message) {
  for (int i = 0; i < sizeof(recipients) - 1; i++ )
  {
    // INSERT LOGIC TO SEND SMTP MAIL
    // https://create.arduino.cc/projecthub/eani/diy-how-to-use-the-arduino-uno-to-send-an-email-or-sms-28ac4d
  }
}



void setupWifi() {
  if(WiFi.status() == WL_NO_MODULE) 
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

    bool infiteAttempts = false;
    int attempts = 1;
    if (wifiConnectionAttempts == -1) infiteAttempts = true;
    
    char ssid[] = { };    
    wifiName.toCharArray(ssid, sizeof(wifiName));
    
    while (wifiStatus != WL_CONNECTED && (infiteAttempts == true || infiteAttempts == false && attempts <= wifiConnectionAttempts)) 
    {
      Serial.println("Attempting (#" + String(attempts) + ") to connect to open SSID: " + wifiName);
      wifiStatus = WiFi.begin(ssid);

      delay(10000);
      attempts++;
    }

    if (infiteAttempts == false && attempts > wifiConnectionAttempts)     
      Serial.println("WARNING: Failed to connect to wifi in allowed attempts. (Allowed Attempts: " + String(wifiConnectionAttempts) + ")");
    else if (wifiStatus == WL_CONNECTED) Serial.println("INFORMATION: Successfully connected to Wifi.");
  }
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
