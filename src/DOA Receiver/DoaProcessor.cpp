
#include "DoaProcessor.h"


DoaProcessor::DoaProcessor()
{
	Setup();
}

void DoaProcessor::Setup()
{
	DoaAddress = 0x00;
	DoaSubAddress = 0x00;
	Data = 0x00;
	DataAvailable = false;
	DoaState = DataProcessed;
}

//call this function once you are done with the data
void DoaProcessor::Reset()
{
	DoaAddress = 0x00;
	DoaSubAddress = 0x00;
	Data = 0x00;
	DataAvailable = false;
	DoaState = DataProcessed;
}


//function to retrieve motor number and desired position. Range from 0 to 4096
//last stepper might be more limited to allow for specific doa commands (Ie Reset)
//calling finction need to reset the  DataAvailable flags!

void DoaProcessor::CheckWordCommand(byte addressToCheck, byte doaAddress, byte doaSubAddress, byte doaData)
{
	if (doaAddress == addressToCheck)
	{

		unsigned int pos = doaSubAddress * 256 + doaData;

		if (pos <= 4096)
		{
			Data = pos;
			DoaSubAddress = 0x00;
			DataAvailable = true;
			DoaState = DataAvail;
		}

		if (pos > 4096 && pos <= 8192)
		{
			Data = (pos - 4096);
			DoaSubAddress = 0x01;
			DataAvailable = true;
			DoaState = DataAvail;
		}
		if (pos > 8192 && pos <= 12288)
		{
			Data = (pos - 8192);
			DoaSubAddress = 0x02;
			DataAvailable = true;
			DoaState = DataAvail;
		}
		if (pos > 12288 && pos <= 16380)
		{

			Data = (pos - 12288);
			DoaSubAddress = 0x03;
			DataAvailable = true;
			DoaState = DataAvail;
		}
		if (pos >= 16381)
		{

			delay(10);
			//software_Reset();
			DoaState = ValidReset;
		}
	}
	else
	{
		DataAvailable = false;
		DoaState = DataProcessed;
	}
}


//function to check for a specific DOA Address / SubAddress,

void DoaProcessor::CheckAddressAndSubaddress(byte addressToCheck, byte subAddressToCheck, byte doaAddress, byte doaSubAddress, byte doaData)
{
	if (doaAddress == addressToCheck)
	{
		if (doaSubAddress == subAddressToCheck)
		{
			Data = doaData;
			DoaSubAddress = doaSubAddress;
			DoaAddress = doaAddress;
			DataAvailable = true;
			DoaState = DataAvail;
		}
	}
	else
	{
		DataAvailable = false;
		DoaState = DataProcessed;
	}
}



///reset the Arduino Code
void DoaProcessor::software_Reset() // Restarts program from beginning but does not reset the peripherals and registers
{
	__asm {
		jmp 0
	}
}
