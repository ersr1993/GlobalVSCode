using Microsoft.Extensions.DependencyInjection;
using ViSa.Interpretation;
using ViSa.Lessons;
using ViSa.Models;
using ViSa.Structural;

namespace ViSaBusinessLogic.DI;
public static class DependencyInject
{
    public static void Add(IServiceCollection services)
    {
        //services.AddAbstractFactory<IMesure, Mesure>();
        services.AddTransient<INoteComposer, NoteComposer>();
        services.AddSingleton<UserStateMachine>();

        services.AddSingleton<LessonStepCount>();

    }
    //public static void Add(ICllection)
}
