#ifndef Smoothing_h
#define Smoothing_h
#include "Arduino.h"

class Smoothing {
  public:
    Smoothing ();
    void setNext (Smoothing *smoothing_);
    void setLength (int length_);
    void setValue (int value_);
    int smooth ();
    int smooth (int currentLength_, int currentValue_);
    int getLength ();
    Smoothing *next = NULL;

  private:
    int length = 0;
    int value = 0;
};

#endif