using ViSa.Melody;

namespace ViSa.Harmony;

public interface IChord
{
    public INote Tonic { get;  }
    public IQuality quality { get;  }
}