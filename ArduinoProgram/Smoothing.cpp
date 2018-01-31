#include "Arduino.h"
#include "Smoothing.h"

Smoothing::Smoothing () { }

void Smoothing::setNext(Smoothing *smoothing_) {
  next = smoothing_;
  length = smoothing_->getLength ();
}

void Smoothing::setLength (int length_) {
  length = length_;
}

void Smoothing::setValue (int value_) {
  value = value_;
}

int Smoothing::smooth () {
  if (next != NULL)
    return next->smooth (1, value);

  return -1;
}

int Smoothing::smooth (int currentLength_, int currentValue_) {

  if (next != NULL) {
    if (currentLength_ >= length) {
      delete next;
      next = NULL;
    } else {
      return next->smooth (++currentLength_, currentValue_ + value);
    }
  }
  return currentValue_ / currentLength_;
}

int Smoothing::getLength () {
  return length;
}