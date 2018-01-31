#ifndef TapinServo_h
#define TapinServo_h
#include "Arduino.h"
#include "Smoothing.h"
#include <Servo.h>

class TapinServo {
  public:
    TapinServo ();
    void attach(int pin_);
    void update ();
    void setPwm (int min_, int max_);
    void turn (double angle_);
    void write (int angle_);
    int getCurrentAngle ();

  private:
  	Servo *mainServo;
    int currentAngle = 90;
    int pinNumber = -1;
    Smoothing *currentSmoothing;
    
};

#endif