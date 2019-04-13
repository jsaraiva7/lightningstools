#ifndef StepperCommand_h
#define StepperCommand_h

#include <Arduino.h>
#include "switecX12.h"
 



class StepperCommand {
public:
	StepperCommand();
	StepperCommand(DoaReceive rec, SwitecX12 motors[4], DoaProcessor proc);
	void ResetMotors();
	void UpdateSteppers();
	void PositionCommand(unsigned char motorNum, unsigned int pos);
	void Setup();
	void BlinkLed();


private:

	volatile byte DEVADDR;
	DoaReceive receiver;
	DoaProcessor processor;

	void CheckBlinkLed();
	///reset the Arduino Code
	void software_Reset();
	int STEPS;
	const byte _dataPin;
	const byte _clockPin;
	byte RESET;
	bool StepperReset;
	SwitecX12 motor[4];
	byte motorCount;
	//LED stuff
	unsigned int led_1_blink_time;
	long led_1_last_blink;
	bool shouldBlink;
};
#endif

