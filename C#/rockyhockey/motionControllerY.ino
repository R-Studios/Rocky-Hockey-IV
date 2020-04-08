#include <AccelStepper.h>

//pin setup
//tourque hold pin
#define ENABLE_PIN 11
//end-switch pin, to be configured as pullup-input!!
#define END_PIN 8

AccelStepper stepper(AccelStepper::DRIVER, 5, 6);

#define EN_DISABLE digitalWrite(ENABLE_PIN, HIGH);
#define EN_ENABLE digitalWrite(ENABLE_PIN, LOW);

//constants
long minPosition = -2700;

//used variables
long movement = 0;
int speedSetting = 0;
int enabled = 0;
float assumedPosition = 0;

void setup()
{  

  TCCR1B = (TCCR1B & 0b11111000) | 0x02;
  //TCCR1B = 0; TCCR1B |= (1 « CS10);
  
  pinMode(ENABLE_PIN, OUTPUT);
  pinMode(END_PIN, INPUT_PULLUP);
  stepper.setMinPulseWidth(60);
  stepper.setMaxSpeed(1500);
  stepper.setAcceleration(9999);
  stepper.setEnablePin(ENABLE_PIN);
  Serial.begin(9600);

  calibrate();
}

//calibrates position by moving to assumed null position +x till end switch toggled
void calibrate(){
    EN_ENABLE;
    //assumedPosition = stepper.currentPosition();
    //inverting assumed position and adding estimated deviation
    //stepper.move(assumedPosition * -1.0f + 100.0);
    stepper.move(2700);
    stepper.setSpeed(1400);
    while(stepper.distanceToGo() != 0 && digitalRead(END_PIN) == 1){
      stepper.runSpeedToPosition();
    }
    stepper.setCurrentPosition(0);
    EN_DISABLE;
}

//check if movement will cause serious harm or death
bool moveAllowed(){
  if(digitalRead(END_PIN) == 0){
    if(stepper.distanceToGo() < 0){
       return true;
    }
    else{
      stepper.stop();
      stepper.setCurrentPosition(0);
      return false;
    }
  }
  else if(stepper.targetPosition() < minPosition){
    stepper.stop();
    return false;
  }
  else{
    return true;
  }
}

void loop()
{  
    //just testing the switches
    //Serial.print(digitalRead(END_PIN));
    //delay(999);
    
    stepper.move(movement);
    stepper.setSpeed(speedSetting);
    if(stepper.distanceToGo() != 0){
      EN_ENABLE;
    }
    while(stepper.distanceToGo() != 0 && moveAllowed()){
    stepper.runSpeedToPosition();
    }
    movement = stepper.distanceToGo();
    if(stepper.distanceToGo() == 0){
      EN_DISABLE;
    }

if(Serial.available() > 0) {
    String movement_string  = Serial.readStringUntil(',');
    if(movement_string == "position\n"){
      Serial.print(stepper.currentPosition());
    }
    else if(movement_string == "calibrate\n"){
      calibrate();
    }
    else{
      movement = movement_string.toInt();   
    }
    Serial.read();
    String speed_string = Serial.readStringUntil(',');
    Serial.read();
    speedSetting =  speed_string.toInt();  
  }
}


