using LichBieu.Models;
using LichBieu.ViewModels;

namespace LichBieu.Views;

public partial class ItemFormPage : ContentPage
{
    private readonly ItemFormViewModel _formVm;
    private readonly MainViewModel _mainVm;

    public ItemFormPage(MainViewModel mainVm)
    {
        InitializeComponent();
        _mainVm = mainVm;
        _formVm = new ItemFormViewModel();
        BindingContext = _formVm;
    }

    public void PrepareForNew(string defaultDateStr)
    {
        _formVm.Reset(defaultDateStr);
        SyncDatePickerFromModel();
        UpdateTypeButtons();
    }

    public void PrepareForEdit(CalendarItem item)
    {
        _formVm.LoadFromItem(item);
        SyncDatePickerFromModel();
        SyncTimePickersFromModel();
        UpdateTypeButtons();
    }

    private void SyncDatePickerFromModel()
    {
        if (DateTime.TryParseExact(_formVm.Date, "yyyy-MM-dd", null,
            System.Globalization.DateTimeStyles.None, out var dt))
            DatePickerControl.Date = dt;
    }

    private void SyncTimePickersFromModel()
    {
        if (TimeSpan.TryParse(_formVm.StartTime, out var st)) StartTimePicker.Time = st;
        if (TimeSpan.TryParse(_formVm.EndTime,   out var et)) EndTimePicker.Time   = et;
    }

    private void UpdateTypeButtons()
    {
        bool isEvent = _formVm.IsEventType;
        BtnEvent.BackgroundColor = isEvent ? Color.FromArgb("#6366f1") : Color.FromArgb("#f1f5f9");
        BtnEvent.TextColor       = isEvent ? Colors.White              : Color.FromArgb("#64748b");
        BtnTask.BackgroundColor  = isEvent ? Color.FromArgb("#f1f5f9") : Color.FromArgb("#6366f1");
        BtnTask.TextColor        = isEvent ? Color.FromArgb("#64748b") : Colors.White;
    }

    private void OnEventTypeClicked(object sender, EventArgs e)
    {
        _formVm.SetTypeEventCommand.Execute(null);
        UpdateTypeButtons();
    }

    private void OnTaskTypeClicked(object sender, EventArgs e)
    {
        _formVm.SetTypeTaskCommand.Execute(null);
        UpdateTypeButtons();
    }

    private void OnDateSelected(object sender, DateChangedEventArgs e)
        => _formVm.Date = e.NewDate.ToString("yyyy-MM-dd");

    private void OnStartTimeChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TimePicker.Time))
            _formVm.StartTime = StartTimePicker.Time.ToString(@"hh\:mm");
    }

    private void OnEndTimeChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(TimePicker.Time))
            _formVm.EndTime = EndTimePicker.Time.ToString(@"hh\:mm");
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (!_formVm.IsValid)
        {
            await DisplayAlert("Lỗi", "Vui lòng nhập tiêu đề và chọn ngày.", "OK");
            return;
        }

        var item = _formVm.ToCalendarItem();
        bool ok = await _mainVm.SaveItemAsync(item);

        if (!ok)
        {
            await DisplayAlert("Giới hạn", "Đã đạt giới hạn 999 mục!", "OK");
            return;
        }

        await Navigation.PopModalAsync();
    }

    private async void OnCloseClicked(object sender, EventArgs e)
        => await Navigation.PopModalAsync();
}
