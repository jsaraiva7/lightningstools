﻿using System;
using System.Runtime.InteropServices;

namespace Henkie.Common
{
    /// <summary>
    ///   <see cref = "DeviceDataReceivedEventArgs" /> objects provides data received from the device.
    ///   The <see cref = "Device.DataReceived" /> event
    ///   provides <see cref = "DeviceDataReceivedEventArgs" /> event-args
    ///   objects during the raising of each event.
    /// </summary>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ComVisible(true)]
    public sealed class DeviceDataReceivedEventArgs : EventArgs
    {
        public byte[] Data { get; set; }
    }
}