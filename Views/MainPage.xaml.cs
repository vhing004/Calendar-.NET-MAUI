using LichBieu.Models;
using LichBieu.ViewModels;

namespace LichBieu.Views;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _vm;

    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = _vm;
    }

    // ── Calendar day tapped ──────────────────────────────────────────────────
    private void OnDayTapped(object sender, TappedEventArgs e)
    {
        if (sender is BindableObject bo && bo.BindingContext is CalendarDayViewModel day)
            _vm.SelectDateCommand.Execute(day);
    }

    // ── Theme toggle ─────────────────────────────────────────────────────────
    private void OnThemeToggleClicked(object sender, EventArgs e)
    {
        _vm.IsDarkMode = !_vm.IsDarkMode;
        Application.Current!.UserAppTheme = _vm.IsDarkMode ? AppTheme.Dark : AppTheme.Light;
        BtnTheme.Text = _vm.IsDarkMode ? "☀️" : "🌙";
    }

    // ── Add new item ─────────────────────────────────────────────────────────
    private async void OnAddItemClicked(object sender, EventArgs e)
    {
        var formPage = App.GetService<ItemFormPage>();
        formPage.PrepareForNew(_vm.SelectedDate.ToString("yyyy-MM-dd"));
        await Navigation.PushModalAsync(new NavigationPage(formPage)
        {
            BarBackgroundColor = Color.FromArgb("#6366f1"),
            BarTextColor = Colors.White
        });
    }

    // ── Edit item ────────────────────────────────────────────────────────────
    private async void OnEditItemClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is CalendarItem item)
        {
            var formPage = App.GetService<ItemFormPage>();
            formPage.PrepareForEdit(item);
            await Navigation.PushModalAsync(new NavigationPage(formPage)
            {
                BarBackgroundColor = Color.FromArgb("#6366f1"),
                BarTextColor = Colors.White
            });
        }
    }

    // ── Delete item ──────────────────────────────────────────────────────────
    private async void OnDeleteItemClicked(object sender, EventArgs e)
    {
        if (sender is Button btn && btn.BindingContext is CalendarItem item)
        {
            bool confirm = await DisplayAlert(
                "Xác nhận xóa",
                "Bạn có chắc chắn muốn xóa mục này?\nHành động này không thể hoàn tác.",
                "🗑️ Xóa", "Hủy");

            if (confirm)
            {
                await _vm.DeleteItemAsync(item);
                await ShowToastAsync("Đã xóa thành công!");
            }
        }
    }

    // ── Task checkbox ────────────────────────────────────────────────────────
    private async void OnTaskCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox cb && cb.BindingContext is CalendarItem item)
        {
            // Sync back: the binding already updated item.Completed via two-way,
            // but CheckBox is one-way so we do it manually
            item.Completed = e.Value;
            await _vm.SaveItemAsync(item);
        }
    }

    // ── Toast helper ─────────────────────────────────────────────────────────
    private async Task ShowToastAsync(string message)
    {
        // Simple DisplayAlert-based toast (non-blocking feel)
        var cts = new CancellationTokenSource();
        _ = Task.Delay(2000, cts.Token);
        // For a real toast, integrate CommunityToolkit.Maui's Toast
        await DisplayAlert("✓", message, "OK");
    }
}
