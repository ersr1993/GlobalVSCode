namespace ViSa.Melody;
using ViSa.Rythm;
using ViSa.Melody;
using System.Net.Http;

public class MyNote : INote
{
    public NoteOct octave { get; init; }

    public NoteName name { get; init; }
    public NoteAlt alt { get; init; }
    public bool pointee { get; init; }

    public NoteType duration { get; init; }

    public string ToString()
    {
        string message;
        string name,alt;


        name = this.name.ToString();
        alt = ToString(this.alt);
        message = $"{name}{alt}";

        return message;
    }
    private string ToString(NoteAlt alt)
    {
        string altSymbol;

        switch (alt)
        {
            case NoteAlt.Becar:
                altSymbol = string.Empty;
                break;
            case NoteAlt.Sharp:
                altSymbol = "#";
                break;
            case NoteAlt.Flat:
                altSymbol = "b";
                break;
            default:
                string message;
                message = $"Alteration {alt.ToString()} inconnue";
                throw new System.Exception(message);
        }

        return altSymbol;
    }
}