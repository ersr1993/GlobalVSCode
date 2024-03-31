using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ViSa.Lessons;
public enum Lessons
{
    TonDemiTon,
}
public class LessonStepCount
{

    public Dictionary<Lessons, Func<string>> Glossary;

    public LessonStepCount()
    {
        Glossary = BuildLessonsDictionnary();
    }

    private Dictionary<Lessons, Func<string>>? BuildLessonsDictionnary()
    {
        Dictionary<Lessons, Func<string>> myGlossary;

        myGlossary = new Dictionary<Lessons, Func<string>>()
        {
            { Lessons.TonDemiTon, GetLessonTonDemiTon },
        };

        return myGlossary;
    }

    private string GetLessonTonDemiTon()
    {
        string lesson;

        lesson = $@"Sur le piano, la 'distance' qui sépare deux notes consécutives"
                + $@"est nommée un demi ton;";

        return lesson;
    }
}
