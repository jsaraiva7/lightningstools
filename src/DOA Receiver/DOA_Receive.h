/*
 Name:		DOA_Receiver.h
 Created:	28/02/2019 17:40:03
 Author:	jsara
 Editor:	http://www.visualmicro.com
*/

#ifndef DOARECEIVE_H_
#define DOARECEIVE_H_

#if defined(ARDUINO) && ARDUINO >= 100
	#include "arduino.h"
#else
 
#endif
/*
DoaReceive.h

Created: 25/02/2019 17:36:19
Author: jsara
*/


#define uint8_t unsigned char


class DoaReceive
{

public:
	friend void INT0_vect(void);
	enum State : unsigned char
	{
		Undefined = 0,
		DataAvail = 1,
		DataProcessed = 2,
	};
	volatile State DoaState;
	volatile uint8_t DOA_ADDRESS;
	volatile uint8_t SubAddress;
	volatile uint8_t Data;
	volatile bool DataAvailable;
	DoaReceive(uint8_t clockpin, uint8_t datapin);

	DoaReceive();
	void DoaRecv();
	static DoaReceive* pReceiver;
	void Reset();
private:

	void software_Reset();
	static void ISR_Fnc();
	void SetupDoa(uint8_t clockpin, uint8_t datapin);
	volatile uint8_t DOA_state;
	volatile uint8_t bitcounter;
	volatile uint8_t DOA_data;
	volatile uint8_t DOA_subaddr;
	volatile uint8_t DOA_addr;
	volatile uint8_t validatorBitCounter;
	uint8_t dataPin;
	uint8_t clockPin;

};




#endif /* DOARECEIVE_H_ */

 

