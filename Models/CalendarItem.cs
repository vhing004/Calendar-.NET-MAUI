using SQLite;

namespace LichBieu.Models;

/// <summary>
/// Model chính cho cả Sự kiện (Event) và Công việc (Task)
/// </summary>
[Table("CalendarItems")]
public class CalendarItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    /// <summary>"event" hoặc "task"</summary>
    public string Type { get; set; } = "event";

    public string Title { get; set; } = string.Empty;

    /// <summary>work | study | personal | family | health | meeting | birthday | other</summary>
    public string Category { get; set; } = "other";

    /// <summary>Định dạng yyyy-MM-dd</summary>
    public string Date { get; set; } = string.Empty;

    /// <summary>Giờ bắt đầu HH:mm (chỉ cho sự kiện)</summary>
    public string StartTime { get; set; } = string.Empty;

    /// <summary>Giờ kết thúc HH:mm (chỉ cho sự kiện)</summary>
    public string EndTime { get; set; } = string.Empty;

    /// <summary>low | medium | high (chỉ cho công việc)</summary>
    public string Priority { get; set; } = "medium";

    public string Location { get; set; } = string.Empty;

    /// <summary>none | 5min | 15min | 30min | 1hour | 1day</summary>
    public string Reminder { get; set; } = "none";

    public string Tags { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public bool Completed { get; set; } = false;

    public string CreatedAt { get; set; } = DateTime.Now.ToString("o");

    // ─── Computed (không lưu DB) ─────────────────────────────────────────────

    [Ignore]
    public bool IsEvent => Type == "event";

    [Ignore]
    public bool IsTask => Type == "task";

    [Ignore]
    public string CategoryEmoji => Category switch
    {
        "work"     => "💼",
        "study"    => "📚",
        "personal" => "👤",
        "family"   => "👨‍👩‍👧‍👦",
        "health"   => "💪",
        "meeting"  => "🤝",
        "birthday" => "🎂",
        _          => "📌"
    };

    [Ignore]
    public string CategoryLabel => Category switch
    {
        "work"     => "Công việc",
        "study"    => "Học tập",
        "personal" => "Cá nhân",
        "family"   => "Gia đình",
        "health"   => "Sức khỏe",
        "meeting"  => "Họp",
        "birthday" => "Sinh nhật",
        _          => "Khác"
    };

    [Ignore]
    public Color CategoryColor => Category switch
    {
        "work"     => Color.FromArgb("#3b82f6"),
        "study"    => Color.FromArgb("#a855f7"),
        "personal" => Color.FromArgb("#10b981"),
        "family"   => Color.FromArgb("#ec4899"),
        "health"   => Color.FromArgb("#22c55e"),
        "meeting"  => Color.FromArgb("#f59e0b"),
        "birthday" => Color.FromArgb("#f43f5e"),
        _          => Color.FromArgb("#64748b")
    };

    [Ignore]
    public string PriorityLabel => Priority switch
    {
        "high"   => "🔴 Cao",
        "medium" => "🟡 Trung bình",
        _        => "🟢 Thấp"
    };

    [Ignore]
    public string TimeDisplay
    {
        get
        {
            if (string.IsNullOrEmpty(StartTime)) return string.Empty;
            return string.IsNullOrEmpty(EndTime) ? StartTime : $"{StartTime} - {EndTime}";
        }
    }
}
