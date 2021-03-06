﻿/*
    Copyright(c) 2017 Petter Labråten/LAVSPENT.NO. All rights reserved.

    The MIT License(MIT)

    Permission is hereby granted, free of charge, to any person obtaining a
    copy of this software and associated documentation files (the "Software"),
    to deal in the Software without restriction, including without limitation
    the rights to use, copy, modify, merge, publish, distribute, sublicense,
    and/or sell copies of the Software, and to permit persons to whom the
    Software is furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
    FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
    DEALINGS IN THE SOFTWARE.
*/


using System;

namespace Lavspent.DaisyChain.Firmata.Firmata.Messages.I2c
{
    public class I2cReply : AbstractFirmataMessage
    {
        public static readonly FirmataMessageDescriptor MessageDescriptor =
            new FirmataMessageDescriptor(0x77, 0xFF, true, 0, I2cReply.Create);

        /// <summary>
        /// 
        /// </summary>
        public byte SlaveAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte Register { get; set; }

        /// <summary>
        /// TODO: Name conflict. Data, i2cData.... 
        /// </summary>
        public byte[] I2cData { get; private set; }

        public override byte[] Data
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public I2cReply()
            : base(MessageDescriptor)
        {
        }

        public static IFirmataMessage Create(FirmataMessageDescriptor commandInfo, uint command, byte[] data = null)
        {
            var i2cReply = new I2cReply()
            {
                SlaveAddress = (byte)FirmataUtils.MidiDecodeUShort(data, 0),
                Register = (byte)FirmataUtils.MidiDecodeUShort(data, 2),
                I2cData = FirmataUtils.Decode7BitData(data, 4, data.Length - 4)
            };

            return i2cReply;
        }
    }
}