#include <Wire.h>
#include <math.h>

#define BUTTON  2 //2 voor ard

bool lastButtonState = HIGH;

void setup() {
  Serial.begin(9600);

  Wire.begin();

  Wire.beginTransmission(0x68);
  Wire.write(0x6B);
  Wire.write(0);
  Wire.endTransmission();

  pinMode(BUTTON, INPUT_PULLUP);
}

void loop() {
  bool buttonState = digitalRead(BUTTON);

  if (lastButtonState == HIGH && buttonState == LOW) {

    Wire.beginTransmission(0x68);
    Wire.write(0x3B);
    Wire.endTransmission(false);

    Wire.requestFrom(0x68, 6, true);

    int16_t x = Wire.read() << 8 | Wire.read();
    int16_t y = Wire.read() << 8 | Wire.read();
    int16_t z = Wire.read() << 8 | Wire.read();

    float angle = atan2(y, z) * 180.0 / PI;
    
    Serial.print("test2|");
    Serial.println(angle);
  }

  lastButtonState = buttonState;

  delay(200);
}