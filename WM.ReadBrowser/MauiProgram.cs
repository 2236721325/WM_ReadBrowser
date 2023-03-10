using CommunityToolkit.Maui;
using WM.ReadBrowser;
using WM.ReadBrowser.Databases;
using WM.ReadBrowser.Services;
using WM.ReadBrowser.ViewModels;
using WM.ReadBrowser.Views;

namespace WM.ReadBrowser
{
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




            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<MainPage>();

            builder.Services.AddTransient<ToolPopup>();
            builder.Services.AddTransient<ToolViewModel>();

            builder.Services.AddSingleton<MyDatabase>();

            builder.Services.AddSingleton<IMauiInitializeService>(new IocConfigurationService());


            //全局异常处理。我也不知道靠谱不靠谱，因为我还没发现我写的代码抛出过异常。
            MauiExceptions.UnhandledException += (sender, args) =>
            {
                App.Current.MainPage.DisplayAlert("异常", args.ExceptionObject.ToString(), "了解").Wait();
            };





            return builder.Build();
        }
    }
}
