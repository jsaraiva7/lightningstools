using System;
using System.Collections.Generic;
using Common.MacroProgramming;
using Common.Statistics;

namespace CommonUi
{
    public class SignalViewer
    {

        private TimeSpan _duration;
        private readonly Signal _signal;
        private readonly List<TimestampedDecimal> _signalStateHistory = new List<TimestampedDecimal>();
        private readonly object _signalStateHistoryLock = new object();
        private readonly DateTime _startTime = DateTime.UtcNow;
        public delegate void SignalChanged();
        public event SignalChanged Changed;

        public SignalViewer(Signal signal, int durationMs = 5000)
        {
            _signal = signal;
            _duration = TimeSpan.FromMilliseconds(durationMs);
            RegisterForChangedEvent(signal);
        }
        private void AnalogSignalChanged(object sender, AnalogSignalChangedEventArgs args)
        {
            CaptureNewSample();
            if (Changed != null) Changed();
        }
        public List<TimestampedDecimal> SignalStateHistory
        {
            get
            {
                if (_signalStateHistory != null)
                {
                    return _signalStateHistory;
                }
                else
                {
                    return new List<TimestampedDecimal>();
                }
            }
        }
        private void CaptureNewSample()
        {
            lock (_signalStateHistoryLock)
            {
                PurgeOldSamples();
                var signal = _signal as AnalogSignal;
                if (signal != null)
                {
                    _signalStateHistory.Add(new TimestampedDecimal
                    {
                        Timestamp = DateTime.UtcNow,
                        Value = signal.State,
                        CorrelatedValue = signal.CorrelatedState
                    });
                }
                else if (_signal is DigitalSignal)
                {
                    _signalStateHistory.Add(new TimestampedDecimal
                    {
                        Timestamp = DateTime.UtcNow,
                        Value = ((DigitalSignal)_signal).State ? 1 : 0
                    });
                }
            }
        }

        private void DigitalSignalChanged(object sender, DigitalSignalChangedEventArgs args)
        {
            CaptureNewSample();
            if (Changed != null) Changed();
        }

        private void PurgeOldSamples()
        {
            lock (_signalStateHistoryLock)
            {
                _signalStateHistory.RemoveAll(x => x.Timestamp < DateTime.UtcNow.Subtract(_duration));
            }
        }

        private void RegisterForChangedEvent(Signal signal)
        {
            var analogSignal = signal as AnalogSignal;
            if (analogSignal != null)
            {
                analogSignal.SignalChanged += AnalogSignalChanged;
            }
            else if (signal is DigitalSignal)
            {
                ((DigitalSignal)signal).SignalChanged += DigitalSignalChanged;
            }
        }
    }
}