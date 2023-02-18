using System.Collections.Generic;

namespace ProtoMIDI
{
    //
    // MIDI device driver class that manages all MIDI ports (interfaces) found
    // in the system.
    //
    sealed class MidiDriver : System.IDisposable
    {
        #region Internal objects and methods

        private MidiProbe _midiProbe;
        private List<MidiPort> _ports = new List<MidiPort>();

        void ScanPorts()
        {
            for (int i = 0; i < _midiProbe.PortCount; i++)
            {
                string portName;
                MidiPort port;

                portName = _midiProbe.GetPortName(i);
                port = new MidiPort(i, portName);

                _ports.Add(port);
            }
        }

        void DisposePorts()
        {
            foreach (MidiPort midiPort in _ports)
            {
                midiPort.Dispose();
            }
            _ports.Clear();
        }

        #endregion

        #region Public methods

        public void Update()
        {
            _midiProbe = _midiProbe ?? new MidiProbe();

            bool portsCountMatch;
            portsCountMatch = _ports.Count == _midiProbe.PortCount;
            if (!portsCountMatch)
            {
                DisposePorts();
                ScanPorts();
            }

            foreach (MidiPort port in _ports)
            {
                port.ProcessMessageQueue();
            }
        }

        public void Dispose()
        {
            DisposePorts();

            _midiProbe?.Dispose();
            _midiProbe = null;
        }

        #endregion
    }
}
