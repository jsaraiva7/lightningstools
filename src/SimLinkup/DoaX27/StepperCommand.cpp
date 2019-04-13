#include "StepperCommand.h"
#include <Arduino.h>
#include "DOA_Receive.h"
#include "DoaProcessor.h"


const byte _dataPin = 3;
const byte _clockPin = 2;
volatile byte DEVADDR = 0x70;
 

StepperCommand::StepperCommand(DoaReceive rec, SwitecX12 motors[4], DoaProcessor proc)
{
	DoaReceive receiver = rec;
	DoaProcessor processor = proc;
	for (size_t i = 0; i < 4; i++)
	{
		motor[i] = motors[i];
	}
	Setup();
}





void StepperCommand::Setup()
{



	motorCount = 0;
	StepperReset = false;


	int led_1_blink_time = 50;
	long led_1_last_blink;
	bool shouldBlink = false;

	//



	motorCount = (sizeof(motor) / sizeof(motor[0]));
	StepperReset = false;
	//Serial.println(motorCount);
}

//home-in the steppers
//NON BLOCKING FUNCTION as long as the original switec x12 library is modded to set   currentStep = steps
//on class initialization
void StepperCommand::ResetMotors()
{
	for (int i = 0; i < motorCount; i++)
	{
		motor[i].setPosition(0);
	}
}


//function to update motor position
void StepperCommand::UpdateSteppers()
{

	if (receiver.DataAvailable)
	{
		Serial.println("DataAvailable");
		processor.CheckWordCommand(DEVADDR, receiver.DOA_ADDRESS, receiver.SubAddress, receiver.Data);

		if (processor.DataAvailable)
		{
			if (processor.DoaState == 3)
			{
				ResetMotors();
			}
			else
			{
				motor[processor.DoaSubAddress].setPosition(processor.Data);
				Serial.println(processor.Data);
				//motors.PositionCommand(processor.DoaSubAddress, processor.Data);
			}
		}
		processor.Reset();
		receiver.Reset();
	}
	for (int i = 0; i < motorCount; i++) {

		//motor[i].update();
		//Serial.println(motor[i].targetStep);
	}

}

//Blink led on port 13 if any motor is moving
//
//void StepperCommand::BlinkLed()
//{
//  CheckBlinkLed();
//  if (shouldBlink)
//  {
//    if ((millis() - led_1_last_blink) >= led_1_blink_time)
//    {
//      if (digitalRead(13) == HIGH)
//      {
//        digitalWrite(13, LOW);
//      } else
//      {
//        digitalWrite(13, HIGH);
//      }
//      led_1_last_blink = millis();
//    }
//  }
//}


//comand stepper to a desired position
void StepperCommand::PositionCommand(unsigned char motorNum, unsigned int pos)
{
	if (StepperReset == false)
	{
		if (pos < STEPS)
		{
			motor[motorNum].setPosition(pos);
		}
	}
	else
	{
		if (motor[motorNum].stopped)
		{
			StepperReset = false;
			if (pos < STEPS)
			{
				motor[motorNum].setPosition(pos);
			}
		}
	}
}


////check if we need to blink the led
//void StepperCommand::CheckBlinkLed()
//{
//  bool allStopped = true;
//  for (int i = 0; i < motorCount; i++)
//  {
//    if (motor[i].currentStep != motor[i].targetStep)
//    {
//      allStopped = false;
//    }
//  }
//
//  if (allStopped == true)
//  {
//    led_1_last_blink = 0;
//    shouldBlink  = false;
//    digitalWrite(13, LOW);
//  }
//  else
//  {
//    shouldBlink = true;
//  }
//}
