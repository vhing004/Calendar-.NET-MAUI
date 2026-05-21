using SQLite;
using LichBieu.Models;

namespace LichBieu.Data;

/// <summary>
/// Service xử lý toàn bộ thao tác SQLite.
/// Singleton – dùng chung trong app thông qua DI.
/// </summary>
public class DatabaseService
{
    private SQLiteAsyncConnection? _db;

    private async Task Init()
    {
        if (_db is not null) return;

        var dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "lichbieu.db3");

        _db = new SQLiteAsyncConnection(dbPath,
            SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);

        await _db.CreateTableAsync<CalendarItem>();
    }

    // ─── CRUD ────────────────────────────────────────────────────────────────

    public async Task<List<CalendarItem>> GetAllAsync()
    {
        await Init();
        return await _db!.Table<CalendarItem>().OrderByDescending(x => x.CreatedAt).ToListAsync();
    }

    public async Task<List<CalendarItem>> GetByDateAsync(string dateStr)
    {
        await Init();
        return await _db!.Table<CalendarItem>()
            .Where(x => x.Date == dateStr)
            .ToListAsync();
    }

    public async Task<CalendarItem?> GetByIdAsync(int id)
    {
        await Init();
        return await _db!.Table<CalendarItem>().Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveAsync(CalendarItem item)
    {
        await Init();
        if (item.Id == 0)
            return await _db!.InsertAsync(item);
        return await _db!.UpdateAsync(item);
    }

    public async Task<int> DeleteAsync(CalendarItem item)
    {
        await Init();
        return await _db!.DeleteAsync(item);
    }

    public async Task<int> CountAsync()
    {
        await Init();
        return await _db!.Table<CalendarItem>().CountAsync();
    }

    /// <summary>
    /// Tìm kiếm theo title, description, tags, location.
    /// </summary>
    public async Task<List<CalendarItem>> SearchAsync(string query)
    {
        await Init();
        var lower = query.ToLower();
        var all = await GetAllAsync();
        return all.Where(x =>
            x.Title.ToLower().Contains(lower) ||
            x.Description.ToLower().Contains(lower) ||
            x.Tags.ToLower().Contains(lower) ||
            x.Location.ToLower().Contains(lower)
        ).ToList();
    }

    /// <summary>
    /// Lấy danh sách ngày có sự kiện trong tháng.
    /// </summary>
    public async Task<HashSet<string>> GetDatesWithItemsAsync(int year, int month)
    {
        await Init();
        var prefix = $"{year:D4}-{month:D2}";
        var items = await _db!.Table<CalendarItem>()
            .Where(x => x.Date.StartsWith(prefix))
            .ToListAsync();
        return items.Select(x => x.Date).ToHashSet();
    }
}
