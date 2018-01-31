#include "Arduino.h"
#include "TapinServo.h"
#include <Servo.h>

TapinServo::TapinServo () {

	currentSmoothing = new Smoothing ();
	currentSmoothing->setLength (35);
	currentSmoothing->setValue (90);
	mainServo = new Servo();

}

void TapinServo::attach (int pin_) {

	pinNumber = pin_;
	mainServo->attach (pinNumber);

}

void TapinServo::update () {

	currentAngle = currentSmoothing->smooth ();
	// write (currentAngle);

}

void TapinServo::turn (double angle_) {

	Smoothing *newSmoothing = new Smoothing();
	newSmoothing->setValue (angle_);
	newSmoothing->setNext (currentSmoothing);
	currentSmoothing = newSmoothing;

}

void TapinServo::write (int angle_) {
	int angle = angle_ < 0 ? 0 : angle_ > 180 ? 180 : angle_;
	Serial.print(pinNumber);
	Serial.print(", ");
	Serial.println(angle);

	mainServo->write (angle);
}

int TapinServo::getCurrentAngle () {
	return currentAngle;
}
