using ViSa.Melody;
using ViSa.Rythm;
using ViSa.Structural;

namespace ViSa.Interpretation
{
    /// <summary>
    ///  Provides concrete classes
    /// </summary>
    public class NoteComposer : INoteComposer
    {
        public IMesure GetMesure()
        {
            IMesure myMesure;
            List<INote> notes;

            notes = GetRandomNotes(4);
            myMesure = new Mesure()
            {
                Notes = notes,
            };

            return myMesure;
        }

        private List<INote> GetRandomNotes(int count)
        {
            List<INote> notes;


            notes = new List<INote>();
            for (int i = 0; i < count; i++)
            {
                notes.Add(GetRandomNoire());
            }
            //{
            //    GetRandomNoire(),
            //    GetRandomNoire(),
            //    GetRandomNoire(),
            //    GetRandomNoire()
            //};


            return notes;
        }
        private INote GetNote(NoteType type)
        {
            INote note;

            note = new MyNote()
            {
                alt = GetRandAlt(),
                name = GetRandNoteName(),
                duration = NoteType.noire,
                pointee = false,
            };

            return note;
        }

        private NoteAlt GetRandAlt()
        {
            NoteAlt alt;
            Random rand;

            rand = new Random();
            alt = (NoteAlt)rand.Next(0, 3);

            return alt;

        }
        private NoteName GetRandNoteName()
        {
            NoteName note;
            Random rand;

            rand = new Random();
            note = (NoteName)rand.Next((int)NoteName.C, (int)NoteName.B + 1);

            return note;
        }
        private INote GetRandomNoire()
        {
            INote randomNote;

            randomNote = GetNote(NoteType.noire);

            return randomNote;
        }
    }
}
