using Microsoft.Extensions.Logging;
using LichBieu.Data;
using LichBieu.ViewModels;
using LichBieu.Views;

namespace LichBieu;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<ItemFormViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<ItemFormPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
