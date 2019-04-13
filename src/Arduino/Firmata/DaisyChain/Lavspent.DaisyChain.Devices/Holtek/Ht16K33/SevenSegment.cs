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
using System.Collections.Generic;

namespace Lavspent.DaisyChain.Devices.Holtek.Ht16K33
{
    // TODO: 
    public interface ISevenSegmnet
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class SevenSegment : ISevenSegmnet
    {
        private Ht16K33 _ht16K33;
        private Dictionary<char, byte> _digitValues = new Dictionary<char, byte>
        {
            {' ', 0x00},
            {'-', 0x40},
            {'0', 0x3F},
            {'1', 0x06},
            {'2', 0x5B},
            {'3', 0x4F},
            {'4', 0x66},
            {'5', 0x6D},
            {'6', 0x7D},
            {'7', 0x07},
            {'8', 0x7F},
            {'9', 0x6F},
            {'A', 0x77},
            {'B', 0x7C},
            {'C', 0x39},
            {'D', 0x5E},
            {'E', 0x79},
            {'F', 0x71}
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ht16K33"></param>
        public SevenSegment(Ht16K33 ht16K33)
        {
            _ht16K33 = ht16K33;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="value"></param>
        public void SetDigit(byte position, byte value)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="digit"></param>
        public void SetDigit(byte position, char digit)
        {
            digit = Char.ToUpper(digit);
            SetDigit(position, _digitValues[digit]);
        }
    }
}
