#pragma once
#ifndef DoaProcess_h
#define DoaProcess_h

#define uint8_t unsigned char


#if defined(ARDUINO) && ARDUINO >= 100
#include "arduino.h"
#else
 
#endif
 
 



class DoaProcessor {
public:
	enum State : unsigned char
	{
		Undefined = 0,
		DataAvail = 1,
		DataProcessed = 2,
		ValidReset = 3,
	};
	DoaProcessor();
	uint8_t DoaAddress;
	uint8_t DoaSubAddress;
	unsigned int Data;
	bool DataAvailable;
	State DoaState;
	void CheckWordCommand(uint8_t addressToCheck, uint8_t doaAddress, uint8_t doaSubAddress, uint8_t doaData);
	void CheckAddressAndSubaddress(uint8_t addressToCheck, uint8_t subAddressToCheck, uint8_t doaAddress, uint8_t doaSubAddress, uint8_t doaData);
	void Setup();
	void Reset();
	void software_Reset();
};

#endif
