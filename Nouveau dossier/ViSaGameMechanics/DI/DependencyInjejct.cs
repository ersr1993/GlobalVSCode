using Microsoft.Extensions.DependencyInjection;
using StandardTools.Analysis;

namespace ViSaGameMechanics.DI;
public class DependencyInjejct
{
    public static void Add(IServiceCollection services)
    {
        services.AddTransient<IViSaMyClock, ViSaClock>();
        services.AddSingleton<Metronom>();
        services.AddSingleton<EventEmitter>();
        services.AddSingleton<EventListener>();

        AddGameEngine(services);
    }

    private static void AddGameEngine(IServiceCollection services)
    {
        services.AddSingleton<GameEngine>();
        services.AddSingleton<GameScene>();
        services.AddSingleton<IDebug, MyDebug>();

    }
}
