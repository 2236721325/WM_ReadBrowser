using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.Logging;
using WM_ReadBrowser.Databases;
using WM_ReadBrowser.Services;
using WM_ReadBrowser.ViewModels;
using WM_ReadBrowser.Views;

namespace WM_ReadBrowser;

public static class MauiProgram
{

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif


        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<MainPage>();

        builder.Services.AddTransient<ToolPopup>();
        builder.Services.AddTransient<ToolViewModel>();

        builder.Services.AddSingleton<MyDatabase>();

        builder.Services.AddSingleton<IMauiInitializeService>(new IocConfigurationService());


        return builder.Build();
    }

 
}
