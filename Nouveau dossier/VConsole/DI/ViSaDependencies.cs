using ConsViSa.Menus.SubMenus;
using Microsoft.Extensions.DependencyInjection;
using ViSa.Interpretation;
using ViSa.Lessons;
using ViSa.Models;
using ViSa.Structural;
using _bl = ViSaBusinessLogic.DI;
using _gm = ViSaGameMechanics.DI;

namespace ConsViSa.DI;
internal static class ViSaDependencies
{
    public static void AddDependencies(IServiceCollection services)
    {
        AddMenus(services);
        AddAbstractFactories(services);
        _bl.DependencyInject.Add(services);
        _gm.DependencyInjejct.Add(services);
    }
    public static void AddMenus(IServiceCollection services)
    {
        services.AddSingleton<MenuCompose>();
        services.AddSingleton<MenuCombo>();
        services.AddSingleton<MenuMetronom>();
        services.AddSingleton<MenuLesson>();
        services.AddSingleton<MenuBrouillon>();
        services.AddSingleton<MenuBrouillon2>();
        services.AddSingleton<MenuBrouillon3>();
    }
    public static void AddAbstractFactories(IServiceCollection services)
    {
        services.AddAbstractFactory<IMesure, Mesure>();
        //services.AddTransient<INoteComposer, NoteComposer>();
        //services.AddSingleton<UserStateMachine>();
        //services.AddSingleton<LessonStepCount>();
    }

}