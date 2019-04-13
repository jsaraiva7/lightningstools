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

using Lavspent.DaisyChain.Firmata.Firmata.Messages;
using Lavspent.DaisyChain.Firmata.Firmata.Messages.Capability;
using Lavspent.DaisyChain.Firmata.Firmata.Messages.I2c;
using Lavspent.DaisyChain.Stream;

namespace Lavspent.DaisyChain.Firmata.Firmata
{
    /// <summary>
    /// Implementation of the FirmataReader that reads server side request
    /// type messages. 
    /// </summary>
    public class FirmataServerReader : FirmataReader
    {
        public FirmataServerReader(IStream stream)
                : base(stream)
        {
            // Register the commands that the client reader understands
            AddMessageDescriptors(
                ReportAnalogPin.MessageDescriptor,
                ReportDigitalPin.MessageDescriptor,
                SetPinMode.MessageDescriptor,
                SetDigitalPinValue.MessageDescriptor,
                SystemReset.MessageDescriptor,

                // Sysex commands
                AnalogMappingQuery.MessageDescriptor,
                CapabilityQuery.MessageDescriptor,
                PinStateQuery.MessageDescriptor,
                I2cRequest.MessageDescriptor,
                I2cConfig.MessageDescriptor,
                FirmwareQuery.MessageDescriptor,
                SamplingInterval.MessageDescriptor
                );
        }
    }
}
