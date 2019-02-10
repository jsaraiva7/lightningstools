using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArduinoTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            serialPort1.Open();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
          

            Write(trackBar1.Value);
        
        }



        public void Write(int value)
        {
            byte[] data = CommandMessage(value.ToString().ToCharArray());
            serialPort1.Write(data, 0, data.Length);  //the serial port that i assume you have set up
            //This will send "~TurnOnLights~" to the arduino

        }
        public byte[] CommandMessage(char[] CMD)
        {
            //This takes in a string and converts it to a byte array ready to be sent over serial
            
            byte[] message = new byte[] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 }; //Add 2 for the terminal chars
            message[0] = BitConverter.GetBytes('~')[0]; //Add Start terminal char
            for (int i = 1; i < message.Length - 1; i++)
            {
                if(i <= CMD.Length)
                message[i] = BitConverter.GetBytes(CMD[i - 1])[0];
            }
            message[message.Length - 1] = BitConverter.GetBytes('~')[0]; //Add end terminal char

            return message;
        }
        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Debug.Print("Data Received:");
            Debug.Print(indata);
        }
    }
}
