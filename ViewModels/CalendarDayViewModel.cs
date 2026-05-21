using CommunityToolkit.Mvvm.ComponentModel;

namespace LichBieu.ViewModels;

public partial class CalendarDayViewModel : ObservableObject
{
    public int Day { get; set; }
    public string DateStr { get; set; } = string.Empty;
    public bool IsCurrentMonth { get; set; }

    [ObservableProperty] private bool _isToday;
    [ObservableProperty] private bool _isSelected;
    [ObservableProperty] private bool _hasItems;

    public bool IsEmpty => Day == 0;

    public Color DayBackground
    {
        get
        {
            if (IsToday) return Color.FromArgb("#6366f1");
            if (IsSelected) return Color.FromArgb("#e0e7ff");
            return Colors.Transparent;
        }
    }

    public Color DayTextColor
    {
        get
        {
            if (IsToday) return Colors.White;
            if (IsSelected) return Color.FromArgb("#6366f1");
            return Color.FromArgb("#374151");
        }
    }

    public FontAttributes DayFontAttr
        => (IsToday || IsSelected) ? FontAttributes.Bold : FontAttributes.None;

    protected override void OnPropertyChanged(System.ComponentModel.PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        if (e.PropertyName == nameof(IsToday) || e.PropertyName == nameof(IsSelected))
        {
            OnPropertyChanged(nameof(DayBackground));
            OnPropertyChanged(nameof(DayTextColor));
            OnPropertyChanged(nameof(DayFontAttr));
        }
    }
}
