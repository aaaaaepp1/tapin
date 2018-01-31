#ifndef TapinServoManager_h
#define TapinServoManager_h
#include "Arduino.h"
#include "TapinServo.h"

class TapinServoManager {
  public:
    TapinServoManager ();
    void update (float baseAngle_, float boomAngle_, float armAngle_, float wristAngle_, float fingerAngle_);
    void setBaseAngle (float angle_);
    void setBoomAngle (float angle_);
    void setArmAngle (float angle_);
    void setWristAngle (float angle_);
    void setFingerAngle (float angle_);

  private:
    static const int tapinServosLength = 1;
    TapinServo tapinServos[tapinServosLength];
    
};

#endif