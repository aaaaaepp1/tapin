#include "Arduino.h"
#include "TapinServoManager.h"
#include "TapinServo.h"

TapinServoManager::TapinServoManager () {

  for(int i = 0; i < tapinServosLength; i++) {
    tapinServos[i].attach (i+2);
  }

}

void TapinServoManager::update (float baseAngle_, float boomAngle_, float armAngle_, float wristAngle_, float fingerAngle_) {

  for(int i = 0; i < tapinServosLength; i++) {
    tapinServos[i].turn (baseAngle_);
    tapinServos[i].update ();
  }


  // ===== LOG =====
  // Serial.println(tapinServos[0].getCurrentAngle ());
  // ===== LOG =====
  
}

void TapinServoManager::setBaseAngle (float angle_) {
  tapinServos[0].turn (angle_);
}

void TapinServoManager::setBoomAngle (float angle_) {
  tapinServos[1].turn (angle_);
  tapinServos[2].turn (180.0-angle_);
}

void TapinServoManager::setArmAngle (float angle_) {
  tapinServos[3].turn (angle_);
  tapinServos[4].turn (180.0-angle_);
}

void TapinServoManager::setWristAngle (float angle_) {
  tapinServos[5].turn (angle_);
}

void TapinServoManager::setFingerAngle (float angle_) {
  tapinServos[6].turn (angle_);
  tapinServos[7].turn (180.0-angle_);
}
