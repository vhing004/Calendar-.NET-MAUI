using LichBieu.Views;

namespace LichBieu;

public partial class App : Application
{
    private static IServiceProvider? _serviceProvider;

    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _serviceProvider = serviceProvider;
        MainPage = new NavigationPage(serviceProvider.GetRequiredService<MainPage>())
        {
            BarBackgroundColor = Colors.Transparent,
            BarTextColor = Colors.White
        };
    }

    /// <summary>Global service locator helper (only for code-behind use).</summary>
    public static T GetService<T>() where T : notnull
        => _serviceProvider!.GetRequiredService<T>();
}
