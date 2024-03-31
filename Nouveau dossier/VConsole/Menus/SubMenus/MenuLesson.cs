using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsConsole;
using ViSa.Lessons;

namespace ConsViSa.Menus.SubMenus;
public class MenuLesson : AMenu
{
    private LessonStepCount _lessonStepCount;
    public MenuLesson(
            LessonStepCount LessonStepCount
        )
        : base("Lessons A : Compter les Tons")
    {
        _lessonStepCount = LessonStepCount;
        //this.AddCommand("DemiTon", this.AddFooterMessage(_lessonStepCount.Glossary.TryGetValue(Lessons.TonDemiTon)));
    }

    protected override void SetupCommands()
    {
        AddCommand("DemiTon", DemiTon);
    }

    private void DemiTon()
    {
        Func<string> lessonsFactory;
        string corpus;

        _lessonStepCount.Glossary.TryGetValue(Lessons.TonDemiTon, out lessonsFactory);
        corpus = lessonsFactory();

        AddFooterMessage(corpus);
    }
    private void DisplayLesson(Lessons lesson)
    {
        Func<string> lessonsFactory;
        string corpus;

        _lessonStepCount.Glossary.TryGetValue(Lessons.TonDemiTon, out lessonsFactory);
        corpus = lessonsFactory();

        AddFooterMessage(corpus);
    }

    //private void DemiTon()
    //{
    //    string text;

    //    text = $@"Sur le piano, la 'distance' qui sépare deux notes consécutives"
    //            + $@"est nommée un demi ton;";

    //    this.AddFooterMessage(text);
    //}
}
