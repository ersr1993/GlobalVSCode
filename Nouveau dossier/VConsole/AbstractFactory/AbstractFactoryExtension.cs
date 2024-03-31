using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsViSa.DI;
public static class AbstractFactoryExtension
{
    public static void AddAbstractFactory<TInterface, TImpelemtation>(this IServiceCollection services)
        where TInterface : class
        where TImpelemtation : class, TInterface
    {
        services.AddSingleton<Func<TInterface>>(ConcreteFactory<TInterface>);
        services.AddSingleton<IAbstractFactory<TInterface>, AbstractFactory<TInterface>>();
        services.AddTransient<TInterface, TImpelemtation>();
    }
    private static Func<TInterface> ConcreteFactory<TInterface>(IServiceProvider serviceProvider)
    {
        Func<TInterface> concreteFactory;
        concreteFactory = () => serviceProvider.GetService<TInterface>();
        return concreteFactory;
    }
}
