using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LichBieu.Data;
using LichBieu.Models;

namespace LichBieu.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly DatabaseService _db;

    public MainViewModel(DatabaseService db)
    {
        _db = db;
        SelectedDate = DateTime.Today;
        CurrentMonth = DateTime.Today;
        _ = LoadDataAsync();
    }

    [ObservableProperty] private string _currentTabIndex = "0";
    [ObservableProperty] private DateTime _currentMonth;
    [ObservableProperty] private DateTime _selectedDate;
    [ObservableProperty] private string _currentMonthLabel = string.Empty;
    [ObservableProperty] private string _selectedDateLabel = string.Empty;
    [ObservableProperty] private bool _isDarkMode = false;
    [ObservableProperty] private string _searchQuery = string.Empty;
    [ObservableProperty] private string _taskFilter = "all";
    [ObservableProperty] private string _searchFilter = "all";
    [ObservableProperty] private int _totalEvents;
    [ObservableProperty] private int _completedTaskCount;
    [ObservableProperty] private int _pendingTaskCount;
    [ObservableProperty] private string _completionRate = "0%";
    [ObservableProperty] private string _eventCountLabel = "0 sự kiện";
    [ObservableProperty] private double _calendarGridHeight = 280;

    public ObservableCollection<CalendarDayViewModel> CalendarDays { get; } = new();
    public ObservableCollection<CalendarItem> DayEvents { get; } = new();
    public ObservableCollection<CalendarItem> TasksList { get; } = new();
    public ObservableCollection<CalendarItem> SearchResults { get; } = new();
    public ObservableCollection<CategoryStatItem> CategoryStats { get; } = new();
    public ObservableCollection<WeekBarItem> WeekBars { get; } = new();

    private List<CalendarItem> _allData = new();
    private HashSet<string> _datesWithItems = new();

    public async Task LoadDataAsync()
    {
        _allData = await _db.GetAllAsync();
        await RefreshCalendarDatesAsync();
        RefreshCurrentView();
    }

    private async Task RefreshCalendarDatesAsync()
    {
        _datesWithItems = await _db.GetDatesWithItemsAsync(CurrentMonth.Year, CurrentMonth.Month);
        BuildCalendarGrid();
    }

    private void RefreshCurrentView()
    {
        switch (CurrentTabIndex)
        {
            case "0": RefreshCalendarView(); break;
            case "1": RefreshTasksView(); break;
            case "2": RefreshStatsView(); break;
            case "3": RefreshSearchView(); break;
        }
    }

    private void BuildCalendarGrid()
    {
        CalendarDays.Clear();

        var months = new[] { "Tháng 1","Tháng 2","Tháng 3","Tháng 4","Tháng 5","Tháng 6",
                              "Tháng 7","Tháng 8","Tháng 9","Tháng 10","Tháng 11","Tháng 12" };
        CurrentMonthLabel = $"{months[CurrentMonth.Month - 1]} {CurrentMonth.Year}";

        var firstDay = new DateTime(CurrentMonth.Year, CurrentMonth.Month, 1);
        int startDow = (int)firstDay.DayOfWeek;
        int totalDays = DateTime.DaysInMonth(CurrentMonth.Year, CurrentMonth.Month);
        var today = DateTime.Today;

        for (int i = 0; i < startDow; i++)
            CalendarDays.Add(new CalendarDayViewModel { Day = 0 });

        for (int day = 1; day <= totalDays; day++)
        {
            var date = new DateTime(CurrentMonth.Year, CurrentMonth.Month, day);
            var dateStr = date.ToString("yyyy-MM-dd");
            CalendarDays.Add(new CalendarDayViewModel
            {
                Day = day,
                DateStr = dateStr,
                IsCurrentMonth = true,
                IsToday = date == today,
                IsSelected = date == SelectedDate.Date,
                HasItems = _datesWithItems.Contains(dateStr)
            });
        }

        // Height: number of rows * 42px
        int rows = (int)Math.Ceiling((startDow + totalDays) / 7.0);
        CalendarGridHeight = rows * 42 + 4;

        // Update selected highlight
        RefreshSelectedHighlight();
    }

    private void RefreshSelectedHighlight()
    {
        var selStr = SelectedDate.ToString("yyyy-MM-dd");
        foreach (var d in CalendarDays)
        {
            if (!d.IsEmpty)
                d.IsSelected = d.DateStr == selStr;
        }
    }

    private void RefreshCalendarView()
    {
        RefreshSelectedHighlight();

        var dateStr = SelectedDate.ToString("yyyy-MM-dd");
        var events = _allData
            .Where(x => x.Date == dateStr && x.Type == "event")
            .OrderBy(x => x.StartTime)
            .ToList();

        DayEvents.Clear();
        foreach (var e in events) DayEvents.Add(e);

        EventCountLabel = $"{events.Count} sự kiện";

        SelectedDateLabel = SelectedDate.Date == DateTime.Today
            ? "Sự kiện hôm nay"
            : SelectedDate.ToString("dddd, dd/MM/yyyy");
    }

    [RelayCommand]
    private async Task PreviousMonthAsync()
    {
        CurrentMonth = CurrentMonth.AddMonths(-1);
        await RefreshCalendarDatesAsync();
    }

    [RelayCommand]
    private async Task NextMonthAsync()
    {
        CurrentMonth = CurrentMonth.AddMonths(1);
        await RefreshCalendarDatesAsync();
    }

    [RelayCommand]
    private void SelectDate(CalendarDayViewModel? day)
    {
        if (day is null || day.IsEmpty) return;
        SelectedDate = DateTime.ParseExact(day.DateStr, "yyyy-MM-dd", null);
        RefreshCalendarView();
    }

    private void RefreshTasksView()
    {
        var tasks = _allData.Where(x => x.Type == "task");
        tasks = TaskFilter switch
        {
            "pending"   => tasks.Where(x => !x.Completed),
            "completed" => tasks.Where(x => x.Completed),
            _           => tasks
        };

        var priorityOrder = new Dictionary<string, int> { ["high"] = 0, ["medium"] = 1, ["low"] = 2 };
        var sorted = tasks.OrderBy(x => priorityOrder.GetValueOrDefault(x.Priority, 2)).ToList();

        TasksList.Clear();
        foreach (var t in sorted) TasksList.Add(t);

        CompletedTaskCount = _allData.Count(x => x.Type == "task" && x.Completed);
        PendingTaskCount   = _allData.Count(x => x.Type == "task" && !x.Completed);
    }

    [RelayCommand]
    private async Task ToggleCompleteAsync(CalendarItem item)
    {
        item.Completed = !item.Completed;
        await _db.SaveAsync(item);
        await LoadDataAsync();
    }

    private void RefreshStatsView()
    {
        TotalEvents = _allData.Count;

        var tasks = _allData.Where(x => x.Type == "task").ToList();
        var completed = tasks.Count(x => x.Completed);
        CompletionRate = tasks.Count > 0
            ? $"{Math.Round((double)completed / tasks.Count * 100)}%"
            : "0%";

        var catGroups = _allData.GroupBy(x => x.Category).OrderByDescending(g => g.Count()).ToList();
        CategoryStats.Clear();
        int total = _allData.Count == 0 ? 1 : _allData.Count;
        foreach (var g in catGroups)
        {
            var sample = new CalendarItem { Category = g.Key };
            int pct = (int)Math.Round((double)g.Count() / total * 100);
            CategoryStats.Add(new CategoryStatItem
            {
                Emoji    = sample.CategoryEmoji,
                Label    = sample.CategoryLabel,
                Count    = g.Count(),
                Percent  = pct,
                BarColor = sample.CategoryColor,
                BarWidth = Math.Max(pct * 2.2, 6) // scale to max ~220px
            });
        }

        var today = DateTime.Today;
        var weekStart = today.AddDays(-(int)today.DayOfWeek);
        var dayLabels = new[] { "CN","T2","T3","T4","T5","T6","T7" };
        var counts = new int[7];
        foreach (var item in _allData)
        {
            if (DateTime.TryParse(item.Date, out var d))
            {
                int diff = (int)(d - weekStart).TotalDays;
                if (diff >= 0 && diff < 7) counts[diff]++;
            }
        }
        int maxCount = counts.Max() == 0 ? 1 : counts.Max();
        WeekBars.Clear();
        for (int i = 0; i < 7; i++)
        {
            WeekBars.Add(new WeekBarItem
            {
                Label       = dayLabels[i],
                Count       = counts[i],
                HeightRatio = Math.Max((double)counts[i] / maxCount, 0.05)
            });
        }
    }

    private void RefreshSearchView()
    {
        if (string.IsNullOrWhiteSpace(SearchQuery) && SearchFilter == "all")
        {
            SearchResults.Clear();
            return;
        }

        var results = _allData.AsEnumerable();
        results = SearchFilter switch
        {
            "event"    => results.Where(x => x.Type == "event"),
            "task"     => results.Where(x => x.Type == "task"),
            "upcoming" => results.Where(x => string.Compare(x.Date, DateTime.Today.ToString("yyyy-MM-dd"), StringComparison.Ordinal) >= 0),
            _          => results
        };

        if (!string.IsNullOrWhiteSpace(SearchQuery))
        {
            var q = SearchQuery.ToLower();
            results = results.Where(x =>
                x.Title.ToLower().Contains(q) ||
                x.Description.ToLower().Contains(q) ||
                x.Tags.ToLower().Contains(q) ||
                x.Location.ToLower().Contains(q));
        }

        SearchResults.Clear();
        foreach (var r in results) SearchResults.Add(r);
    }

    partial void OnSearchQueryChanged(string value) => RefreshSearchView();
    partial void OnSearchFilterChanged(string value) => RefreshSearchView();
    partial void OnTaskFilterChanged(string value) => RefreshTasksView();

    [RelayCommand] private void SetSearchFilter(string filter) => SearchFilter = filter;
    [RelayCommand] private void SetTaskFilter(string filter) => TaskFilter = filter;

    [RelayCommand]
    private void SwitchTab(string index)
    {
        CurrentTabIndex = index;
        RefreshCurrentView();
    }

    public async Task<bool> SaveItemAsync(CalendarItem item)
    {
        var count = await _db.CountAsync();
        if (item.Id == 0 && count >= 999) return false;
        item.CreatedAt = item.Id == 0 ? DateTime.Now.ToString("o") : item.CreatedAt;
        await _db.SaveAsync(item);
        await LoadDataAsync();
        return true;
    }

    public async Task DeleteItemAsync(CalendarItem item)
    {
        await _db.DeleteAsync(item);
        await LoadDataAsync();
    }
}

public class CategoryStatItem
{
    public string Emoji    { get; set; } = string.Empty;
    public string Label    { get; set; } = string.Empty;
    public int    Count    { get; set; }
    public int    Percent  { get; set; }
    public Color  BarColor { get; set; } = Colors.Gray;
    public double BarWidth { get; set; } = 8;
    public string Display  => $"{Count} ({Percent}%)";
}

public class WeekBarItem
{
    public string Label       { get; set; } = string.Empty;
    public int    Count       { get; set; }
    public double HeightRatio { get; set; } = 0.05;
    public double BarHeight   => Math.Max(HeightRatio * 120, 4);
}
