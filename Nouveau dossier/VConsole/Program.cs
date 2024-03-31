using EpuratedConsole.Console;
using System;
using _hst = Microsoft.AspNetCore.Hosting;
using _bld = Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ViSa.Structural;
using Microsoft.Extensions.Configuration;
using ConsViSa.DI;
using System.ComponentModel;
using ViSa.Interpretation;
using VsConsole;

namespace EpuratedConsole
{
    internal class Program
    {
        public static IHost _appHost { get; private set; }
        private static IMenu _menu;
        private static void Main()
        {
            _appHost = GetConfiguredHost();
            _menu = ActivatorUtilities.CreateInstance<MenuMain>(_appHost.Services);
            _menu.Open();
        }
        private static IHost GetConfiguredHost()
        {
            IHostBuilder hostBuilder;
            IHost host;

            hostBuilder = Host.CreateDefaultBuilder();
            hostBuilder.ConfigureServices(ViSaDependencies.AddDependencies);
            host = hostBuilder.Build();

            return host;
        }
    }
}
