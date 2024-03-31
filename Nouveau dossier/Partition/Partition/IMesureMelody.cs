namespace ViSa.Structural;

using System.Collections.Generic;
using ViSa.Harmony;
using ViSa.Melody;

public interface IMesureMelody
{
    List<INote> Notes { get; }
    //List<IChord> Chords { get; }
}