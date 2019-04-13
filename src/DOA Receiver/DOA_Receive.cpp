/*
 Name:		DOA_Receiver.cpp
 Created:	28/02/2019 17:40:03
 Author:	jsara
 Editor:	http://www.visualmicro.com
*/

#include "DOA_Receive.h"



 DoaReceive* DoaReceive::pReceiver = 0;

DoaReceive::DoaReceive()
{
}

DoaReceive::DoaReceive(byte clockpin, byte datapin)
{
	SetupDoa(clockpin, datapin);
}

/// function to setup DOA variables initial state.
/// should be called on initialization
void DoaReceive::SetupDoa(byte clockpin, byte datapin)
{
	pReceiver = this;
	dataPin = datapin;
	clockPin = clockpin;
	DOA_state = 0;
	DOA_addr = 0;
	DOA_subaddr = 0;
	DOA_data = 0;
	bitcounter = 7;
	DataAvailable = false;
	DOA_ADDRESS = 0x00;
	SubAddress = 0x00;
	Data = 0x00;
	validatorBitCounter = 0x00;
	attachInterrupt(digitalPinToInterrupt(clockPin), ISR_Fnc, FALLING);
}

///reset the Arduino Code
void DoaReceive::software_Reset() // Restarts program from beginning but does not reset the peripherals and registers
{
	__asm {
		jmp 0
	}
}



///////////////
// DOA Receive Function - called from ISR routine.


 void DoaReceive::ISR_Fnc() {
	Serial.println("isr");
	pReceiver->DoaRecv();
}

void DoaReceive::Reset()
{
	DataAvailable = false;
	DOA_ADDRESS = 0x00;
	SubAddress = 0x00;
	Data = 0x00;

}

void DoaReceive::DoaRecv()
{
	validatorBitCounter++;

	bool DOA_DATA = digitalRead(dataPin);
	//check validator for bitcount
	if (validatorBitCounter > 22)
	{
		//DOA is out of sync, delay and reset. (ISR does not like big delays, so we cannot wait for a full DOA packet (500Us per bit
		delayMicroseconds(500);
		//software reset (Safe as all classes implement the Setup Methos! One gotta love c# and the interfaces! life is a lot easier!)
		software_Reset();

	}
	switch (DOA_state)
	{

	case 0:
		if (DOA_DATA)
			DOA_addr += 128;
		if (bitcounter-- != 0)
			DOA_addr = DOA_addr >> 1;
		else
		{
			DOA_state = 1;
			bitcounter = 5;

		}
		break;
	case 1:
		if (DOA_DATA)
			DOA_subaddr += 128;
		if (bitcounter-- != 0)
			DOA_subaddr = DOA_subaddr >> 1;
		else
		{
			DOA_state = 2;
			DOA_subaddr = DOA_subaddr >> 2;
			bitcounter = 7;
		}
		break;
	case 2:
		if (DOA_DATA)
			DOA_data += 128;
		if (bitcounter-- != 0)
			DOA_data = DOA_data >> 1;
		else
		{
			//check if subAddress value is above 0x3F, if so, DOA is out of sync. Pause and soft reset!!
			if (DOA_subaddr > 0x3F)
			{
				DOA_state = 3;
				software_Reset();
			}

			DOA_state = 0;
			bitcounter = 7;
			DataAvailable = true;
			DOA_ADDRESS = DOA_addr;
			SubAddress = DOA_subaddr;
			Data = DOA_data;
			DOA_addr = 0;
			DOA_subaddr = 0;
			DOA_data = 0;
			//reset validator
			validatorBitCounter = 0x00;

			break;
		}
	}
}
