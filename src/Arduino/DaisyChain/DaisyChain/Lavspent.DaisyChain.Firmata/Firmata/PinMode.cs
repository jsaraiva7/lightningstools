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

namespace Lavspent.DaisyChain.Firmata.Firmata
{
    /// <summary>
    /// 
    /// </summary>
    public enum PinMode : byte
    {
        /// <summary>
        /// 
        /// </summary>
        Input = 0,

        /// <summary>
        /// 
        /// </summary>
        Output = 1,

        /// <summary>
        /// 
        /// </summary>
        Analog = 2,

        /// <summary>
        /// 
        /// </summary>
        Pwm = 3,

        /// <summary>
        /// 
        /// </summary>
        Servo = 4,

        /// <summary>
        /// 
        /// </summary>
        I2c = 6,

        /// <summary>
        /// 
        /// </summary>
        OneWire = 7,

        /// <summary>
        /// 
        /// </summary>
        Stepper = 8,

        /// <summary>
        /// 
        /// </summary>
        Encoder = 9,

        /// <summary>
        /// 
        /// </summary>
        Serial = 10,

        /// <summary>
        /// 
        /// </summary>
        Pullup = 11
    }
}
