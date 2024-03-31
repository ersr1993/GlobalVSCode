
namespace ViSa.Structural;

using System.Collections.Generic;
using ViSa.Harmony;
using ViSa.Melody;
using ViSa.Rythm;

public class Mesure :IMesure, IMesureHarmony, IMesureMelody 
{
    public List<INote> Notes { get; set; }
    public List<IChord> Chords { get; set; }
}
