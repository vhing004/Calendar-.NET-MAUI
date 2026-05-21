using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LichBieu.Models;

namespace LichBieu.ViewModels;

/// <summary>ViewModel cho form thêm/sửa sự kiện và công việc.</summary>
public partial class ItemFormViewModel : ObservableObject
{
    // ─── Fields ──────────────────────────────────────────────────────────────

    [ObservableProperty] private int _itemId = 0;
    [ObservableProperty] private string _itemType = "event";   // event | task
    [ObservableProperty] private string _title = string.Empty;
    [ObservableProperty] private string _category = "other";
    [ObservableProperty] private string _date = DateTime.Today.ToString("yyyy-MM-dd");
    [ObservableProperty] private string _startTime = string.Empty;
    [ObservableProperty] private string _endTime = string.Empty;
    [ObservableProperty] private string _priority = "medium";
    [ObservableProperty] private string _location = string.Empty;
    [ObservableProperty] private string _reminder = "none";
    [ObservableProperty] private string _tags = string.Empty;
    [ObservableProperty] private string _description = string.Empty;
    [ObservableProperty] private bool _completed = false;
    [ObservableProperty] private string _createdAt = DateTime.Now.ToString("o");

    // ─── UI State ─────────────────────────────────────────────────────────────

    [ObservableProperty] private bool _isEventType = true;
    [ObservableProperty] private bool _isTaskType = false;
    [ObservableProperty] private string _formTitle = "Thêm mới";

    // Picker index helpers (bound to Picker.SelectedIndex)
    public List<string> CategoryKeys { get; } = new() { "work", "study", "personal", "family", "health", "meeting", "birthday", "other" };
    public List<string> CategoryLabels { get; } = new() { "💼 Công việc", "📚 Học tập", "👤 Cá nhân", "👨‍👩‍👧‍👦 Gia đình", "💪 Sức khỏe", "🤝 Họp", "🎂 Sinh nhật", "📌 Khác" };
    public List<string> PriorityKeys { get; } = new() { "low", "medium", "high" };
    public List<string> PriorityLabels { get; } = new() { "🟢 Thấp", "🟡 Trung bình", "🔴 Cao" };
    public List<string> ReminderKeys { get; } = new() { "none", "5min", "15min", "30min", "1hour", "1day" };
    public List<string> ReminderLabels { get; } = new() { "Không nhắc", "5 phút trước", "15 phút trước", "30 phút trước", "1 giờ trước", "1 ngày trước" };

    [ObservableProperty] private int _categoryIndex = 7;
    [ObservableProperty] private int _priorityIndex = 1;
    [ObservableProperty] private int _reminderIndex = 0;

    // ─── Commands ─────────────────────────────────────────────────────────────

    [RelayCommand]
    private void SetTypeEvent()
    {
        ItemType = "event";
        IsEventType = true;
        IsTaskType = false;
    }

    [RelayCommand]
    private void SetTypeTask()
    {
        ItemType = "task";
        IsEventType = false;
        IsTaskType = true;
    }

    // ─── Population ───────────────────────────────────────────────────────────

    public void LoadFromItem(CalendarItem item)
    {
        ItemId     = item.Id;
        ItemType   = item.Type;
        Title      = item.Title;
        Category   = item.Category;
        Date       = item.Date;
        StartTime  = item.StartTime;
        EndTime    = item.EndTime;
        Priority   = item.Priority;
        Location   = item.Location;
        Reminder   = item.Reminder;
        Tags       = item.Tags;
        Description = item.Description;
        Completed  = item.Completed;
        CreatedAt  = item.CreatedAt;
        FormTitle  = "Chỉnh sửa";

        IsEventType = item.Type == "event";
        IsTaskType  = item.Type == "task";

        CategoryIndex = Math.Max(0, CategoryKeys.IndexOf(Category));
        PriorityIndex = Math.Max(0, PriorityKeys.IndexOf(Priority));
        ReminderIndex = Math.Max(0, ReminderKeys.IndexOf(Reminder));
    }

    public void Reset(string defaultDateStr)
    {
        ItemId = 0;
        ItemType = "event";
        Title = string.Empty;
        Category = "other";
        Date = defaultDateStr;
        StartTime = string.Empty;
        EndTime = string.Empty;
        Priority = "medium";
        Location = string.Empty;
        Reminder = "none";
        Tags = string.Empty;
        Description = string.Empty;
        Completed = false;
        CreatedAt = DateTime.Now.ToString("o");
        FormTitle = "Thêm mới";
        IsEventType = true;
        IsTaskType = false;
        CategoryIndex = 7;
        PriorityIndex = 1;
        ReminderIndex = 0;
    }

    public CalendarItem ToCalendarItem() => new()
    {
        Id          = ItemId,
        Type        = ItemType,
        Title       = Title.Trim(),
        Category    = CategoryKeys.ElementAtOrDefault(CategoryIndex) ?? "other",
        Date        = Date,
        StartTime   = StartTime,
        EndTime     = EndTime,
        Priority    = PriorityKeys.ElementAtOrDefault(PriorityIndex) ?? "medium",
        Location    = Location.Trim(),
        Reminder    = ReminderKeys.ElementAtOrDefault(ReminderIndex) ?? "none",
        Tags        = Tags.Trim(),
        Description = Description.Trim(),
        Completed   = Completed,
        CreatedAt   = CreatedAt
    };

    public bool IsValid => !string.IsNullOrWhiteSpace(Title) && !string.IsNullOrWhiteSpace(Date);
}
