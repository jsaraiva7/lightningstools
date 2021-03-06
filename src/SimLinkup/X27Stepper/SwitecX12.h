/*
 Name:		X27Stepper.h
 Created:	28/02/2019 17:50:51
 Author:	jsara
 Editor:	http://www.visualmicro.com
*/

#ifndef SwitecX12_h
#define SwitecX12_h

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
	#include "WProgram.h"
#endif


class SwitecX12 {
public:
	unsigned char pinStep;
	unsigned char pinDir;
	unsigned int currentStep;      // step we are currently at
	unsigned int targetStep;       // target we are moving to
	unsigned int steps;            // total steps available
	unsigned long time0;           // time when we entered this state
	unsigned int microDelay;       // microsecs until next state
	unsigned short(*accelTable)[2]; // accel table can be modified.
	unsigned int maxVel;           // fastest vel allowed
	unsigned int vel;              // steps travelled under acceleration
	char dir;                      // direction -1,0,1
	boolean stopped;               // true if stopped
	SwitecX12(unsigned int steps, unsigned char pinStep, unsigned char pinDir);
	SwitecX12();
	//void stepUp();
	void step(int dir);
	void zero();
	void stepTo(int position);
	void advance();
	void update();
	void setPosition(unsigned int pos);
	void updateBlocking();
};

#endif


