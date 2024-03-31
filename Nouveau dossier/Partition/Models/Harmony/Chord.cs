namespace ViSa.Harmony;
using ViSa.Melody;

public class Chord  : IChord
{
    public INote Tonic { get; init; }
    public IQuality quality { get; init; }
}
