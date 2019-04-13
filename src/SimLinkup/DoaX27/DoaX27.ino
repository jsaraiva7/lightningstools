/*
 Name:		DoaX27.ino
 Created:	28/02/2019 17:52:56
 Author:	jsara
*/

#include "StepperCommand.h"
//#include "DoaProcessor.h"
#include "SwitecX12.h"
 
#define byte unsigned char

const byte _dataPin = 3;
const byte _clockPin = 2;

//Motors
const byte A_STEP = 16;
const byte A_DIR = 17;
const byte B_STEP = 19;
const byte B_DIR = 18;
const byte C_STEP = 5;
const byte C_DIR = 4;
const byte D_STEP = 19;
const byte D_DIR = 18;
const byte RESET = 6;
const unsigned int STEPS = ((315 * 12) - 1);

//declaring a global DOA Receiver object Pin 2 for clock, pin 3 for data




DoaReceive receiver(_clockPin, _dataPin);
DoaProcessor processor;

SwitecX12  motor[4]{
	SwitecX12(STEPS, A_STEP, A_DIR),
	SwitecX12(STEPS, B_STEP, B_DIR),
	SwitecX12(STEPS, C_STEP, C_DIR),
	SwitecX12(STEPS, D_STEP, D_DIR)

};
//Declare a StepperCommand Object
StepperCommand motors(receiver, motor, processor);

void setup() {
	Serial.begin(57600);


	pinMode(13, OUTPUT);
	pinMode(RESET, OUTPUT);
	//  LED stuff



	pinMode(A_STEP, OUTPUT);
	pinMode(A_DIR, OUTPUT);
	pinMode(B_STEP, OUTPUT);
	pinMode(B_DIR, OUTPUT);
	pinMode(C_STEP, OUTPUT);
	pinMode(C_DIR, OUTPUT);
	pinMode(D_STEP, OUTPUT);
	pinMode(D_DIR, OUTPUT);

	//  digitalWrite(RESET, LOW);     // assert RESET input pin
	//  delay(10);                     // wait x microsec for clean pulse duration
	//  digitalWrite(RESET, HIGH);    // negate RESET pin

	Serial.println("Ready");
}

void loop() {
	// Serial.println("subaddress");
	// Serial.println("Ready");
	motors.UpdateSteppers();
	// motors.BlinkLed();


}
