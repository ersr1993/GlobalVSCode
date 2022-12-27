namespace ProtoMIDI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using System;

    public class DeviceCallBacks : MonoBehaviour
    {
        public static event Action<ProtoMIDI.MidiNoteControl> PushNote;
        public static event Action<ProtoMIDI.MidiNoteControl> ReleaseNote;
        void Start()
        {
            InputSystem.onDeviceChange += (device, change) =>
            {
                if (change != InputDeviceChange.Added) return;

            //var midiDevice = device as ProtoMIDI.MidiDevice;
            ProtoMIDI.MidiDevice midiDevice = device as ProtoMIDI.MidiDevice;
                if (midiDevice == null) return;

                midiDevice.onWillNoteOn += (note, velocity) =>
                {
                // Note that you can't use note.velocity because the state
                // hasn't been updated yet (as this is "will" event). The note
                // object is only useful to specify the target note (note
                // number, channel number, device name, etc.) Use the velocity
                // argument as an input note velocity.

                displayNoteProperties(note);
                    PushNote?.Invoke(note);
                };
                midiDevice.onWillNoteOff += (note) =>
                {
                    ReleaseNote?.Invoke(note);
                };
            };
        }
        void displayNoteProperties(ProtoMIDI.MidiNoteControl note)
        {
            Debug.Log(string.Format(
            "Note On #{0} (nom:{1}) vel:{2:0.00} ch:{3} dev:'{4}'",
            note.noteNumber,
            note.shortDisplayName,
            note.velocity,
            (note.device as ProtoMIDI.MidiDevice)?.channel,
            note.device.description.product));
        }
    }
}