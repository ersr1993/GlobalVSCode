using ViSa.Melody;
using ViSa.Rythm;

namespace ViSa.Melody
{
    public interface INote
    {
        public NoteName name { get; }
        public NoteAlt alt{ get; }
        public NoteOct octave { get; }

        public NoteType duration { get; }
        public bool pointee { get; }

        public string ToString();
    }
}