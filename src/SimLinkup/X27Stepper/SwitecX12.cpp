/*
 Name:		X27Stepper.cpp
 Created:	28/02/2019 17:50:51
 Author:	jsara
 Editor:	http://www.visualmicro.com
*/

#include "SwitecX12.h"



// This table defines the acceleration curve.
// 1st value is the speed step, 2nd value is delay in microseconds
// 1st value in each row must be > 1st value in subsequent row
// 1st value in last row should be == maxVel, must be <= maxVel
static unsigned short defaultAccelTable[][2] = {
	{ 20, 800 },
{ 50, 500 },
{ 80, 400 },
{ 120, 300 },
{ 140, 200 },
{ 170, 150 },
{ 200, 120 }
};
const int stepPulseMicrosec = 1;
const int resetStepMicrosec = 300;
#define DEFAULT_ACCEL_TABLE_SIZE (sizeof(defaultAccelTable)/sizeof(*defaultAccelTable))

SwitecX12::SwitecX12() {

}

SwitecX12::SwitecX12(unsigned int steps, unsigned char pinStep, unsigned char pinDir)
{
	this->steps = steps;
	this->pinStep = pinStep;
	this->pinDir = pinDir;
	pinMode(pinStep, OUTPUT);
	pinMode(pinDir, OUTPUT);
	digitalWrite(pinStep, LOW);
	digitalWrite(pinDir, LOW);


	dir = 0;
	vel = 0;
	stopped = false;
	currentStep = 0;
	targetStep = 0;

	accelTable = defaultAccelTable;
	maxVel = defaultAccelTable[DEFAULT_ACCEL_TABLE_SIZE - 1][0]; // last value in table.
}

void SwitecX12::step(int dir)
{
	digitalWrite(pinDir, dir > 0 ? LOW : HIGH);

	digitalWrite(pinStep, HIGH);
	delayMicroseconds(stepPulseMicrosec);
	digitalWrite(pinStep, LOW);
	currentStep += dir;
}

void SwitecX12::stepTo(int position)
{
	int count;
	int dir;
	if (position > currentStep) {
		dir = 1;
		count = position - currentStep;
	}
	else {
		dir = -1;
		count = currentStep - position;
	}
	for (int i = 0; i < count; i++) {
		step(dir);
		delayMicroseconds(resetStepMicrosec);
	}
}

void SwitecX12::zero()
{
	currentStep = steps - 1;
	stepTo(0);
	targetStep = 0;
	vel = 0;
	dir = 0;
}

void SwitecX12::advance()
{
	// detect stopped state
	if (currentStep == targetStep && vel == 0) {
		stopped = true;
		dir = 0;
		time0 = micros();
		return;
	}

	// if stopped, determine direction
	if (vel == 0) {
		dir = currentStep < targetStep ? 1 : -1;
		// do not set to 0 or it could go negative in case 2 below
		vel = 1;
	}

	step(dir);

	// determine delta, number of steps in current direction to target.
	// may be negative if we are headed away from target
	int delta = dir > 0 ? targetStep - currentStep : currentStep - targetStep;

	if (delta > 0) {
		// case 1 : moving towards target (maybe under accel or decel)
		if (delta < vel) {
			// time to declerate
			vel--;
		}
		else if (vel < maxVel) {
			// accelerating
			vel++;
		}
		else {
			// at full speed - stay there
		}
	}
	else {
		// case 2 : at or moving away from target (slow down!)
		vel--;
	}

	// vel now defines delay
	unsigned char i = 0;
	// this is why vel must not be greater than the last vel in the table.
	while (accelTable[i][0] < vel) {
		i++;
	}
	microDelay = accelTable[i][1];
	time0 = micros();
}

void SwitecX12::setPosition(unsigned int pos)
{
	// pos is unsigned so don't need to check for <0
	if (pos >= steps) pos = steps - 1;
	targetStep = pos;
	if (stopped) {
		// reset the timer to avoid possible time overflow giving spurious deltas
		stopped = false;
		time0 = micros();
		microDelay = 0;
	}
}

void SwitecX12::update()
{
	if (!stopped) {
		unsigned long delta = micros() - time0;
		if (delta >= microDelay) {
			advance();
		}
	}
}
//This updateMethod is blocking, it will give you smoother movements, but your application will wait for it to finish
void SwitecX12::updateBlocking()
{
	while (!stopped) {
		unsigned long delta = micros() - time0;
		if (delta >= microDelay) {
			advance();
		}
	}
}
