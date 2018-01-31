#include "TapinServoManager.h"

//TapinServoManager tapinServoManager;
int variableResister = 0;
bool serialWave = false;
int serialWaveLed = 10;
int resultIntArray[] = {90, 90, 90, 90, 90};

// ====== TEMPORAL ========
Servo baseServo;
Servo boomLServo;
Servo boomRServo;
Servo armLServo;
Servo armRServo;
Servo wristServo;
Servo fingerLServo;
Servo fingerRServo;
TapinServo baseTServo;
TapinServo boomTServo;
TapinServo armTServo;
TapinServo wristTServo;
TapinServo fingerTServo;
// ====== END TEMPORAL ========

void setup() {
  // put your setup code here, to run once:
  Serial.begin(19200);
  Serial.println("====== PROGRAM START ======");

  // ====== TEMPORAL ========
  baseServo.attach(2);
  boomLServo.attach(3);
  boomRServo.attach(4);
  armLServo.attach(5);
  armRServo.attach(6);
  wristServo.attach(7);
  fingerLServo.attach(8);
  fingerRServo.attach(9);
  // ====== END TEMPORAL ========

  pinMode(serialWaveLed, OUTPUT);
}

void loop() {
  
  if(Serial.available () > 0) {
    Serial.print(serialWave);
    Serial.print(" | ");
    String tmpLine = readLineWithNumber(resultIntArray);
    Serial.println(resultIntArray[3]); //<<<<<<<<<<ずっと90（初期値）が出力される！
    serialWave = !serialWave;
  }
  
//  if(resultIntArray[3] > 85 && resultIntArray[3] < 95) {
//    digitalWrite(serialWaveLed, HIGH);
//  } else {
//    digitalWrite(serialWaveLed, LOW);
//  }
  
//  double variableResisterValue = sg90map(analogRead(variableResister));
//  tapinServoManager.update(180.0*variableResisterValue, 0.0, 0.0, 0.0, 0.0);
  
  // ====== TEMPORAL ========
//  int eachAngle = 180.0*variableResisterValue;
  turn(resultIntArray[0], resultIntArray[1], resultIntArray[2], resultIntArray[3], resultIntArray[4]);
  // ====== END TEMPORAL ========
  
  digitalWrite(serialWaveLed, serialWave);

  delay(10);
}

// ====== TEMPORAL ========

void turn (int baseAngle_, int boomAngle_, int armAngle_, int wristAngle_, int fingerAngle_) {
  setBaseAngle (baseAngle_);
  setBoomAngle (boomAngle_);
  setArmAngle (armAngle_);
  setWristAngle (wristAngle_);
  setFingerAngle (fingerAngle_);
}

void setBaseAngle (int angle_) {
  baseTServo.turn (angle_);
  baseTServo.update();
  int nextAngle = baseTServo.getCurrentAngle();
  nextAngle = nextAngle < 0 ? 0 : nextAngle > 180? 180 : nextAngle;
//  Serial.println(nextAngle);
  baseServo.write (180-nextAngle);
}

void setBoomAngle (int angle_) {
  boomTServo.turn (angle_);
  boomTServo.update();
  int nextAngle = boomTServo.getCurrentAngle();
  nextAngle = nextAngle < 0 ? 0 : nextAngle > 180? 180 : nextAngle;
  boomLServo.write (nextAngle);
  boomRServo.write (180-nextAngle);
}

void setArmAngle (int angle_) {
  armTServo.turn (angle_);
  armTServo.update();
  int nextAngle = armTServo.getCurrentAngle();
  nextAngle = nextAngle < 0 ? 0 : nextAngle > 180? 180 : nextAngle;
  armLServo.write (180-nextAngle);
  armRServo.write (nextAngle);
}

void setWristAngle (int angle_) {
  wristTServo.turn (angle_);
  wristTServo.update();
  int nextAngle = wristTServo.getCurrentAngle();
  nextAngle = nextAngle < 0 ? 0 : nextAngle > 180? 180 : nextAngle;
  wristServo.write (180-nextAngle);
}

void setFingerAngle (int angle_) {
  fingerTServo.turn (angle_);
  fingerTServo.update();
  int nextAngle = fingerTServo.getCurrentAngle();
  nextAngle = nextAngle < 45 ? 45 : nextAngle > 135? 135 : nextAngle;
  fingerLServo.write (180-nextAngle);
  fingerRServo.write (nextAngle);
}
// ====== END TEMPORAL ========

double sg90map(int num) {
  double result = num / 790.0;
  return result > 1.0 ? 1.0 : result;
}

String readLineWithNumber(int *resultIntArray) {
  String result = "";
  String eachNumberString = "";
  int resultIntArrayCounter = 0;
  int loopCount = 0;
  int currentReceivedChar = 0;
  while(loopCount < 20 || currentReceivedChar != 59 || resultIntArrayCounter < 5) {
    currentReceivedChar = Serial.read();
    if(currentReceivedChar == 59){
      resultIntArray[resultIntArrayCounter] = eachNumberString.toInt();
      break;
    } else if(currentReceivedChar == 44) {
      resultIntArray[resultIntArrayCounter] = eachNumberString.toInt();
      resultIntArrayCounter++;
      eachNumberString = "";
    } else {
      eachNumberString.concat(char(currentReceivedChar));
    }
    
    result.concat(char(currentReceivedChar));
    loopCount++;
    delay(1);
    
  }

  return result;
}
