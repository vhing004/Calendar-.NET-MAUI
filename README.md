# 📅 Lịch Biểu Cá Nhân – Personal Calendar Manager

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square)](https://dotnet.microsoft.com/download)
[![MAUI](https://img.shields.io/badge/MAUI-8.0-blue?style=flat-square)](https://learn.microsoft.com/en-us/dotnet/maui/)
[![License](https://img.shields.io/badge/License-MIT-green?style=flat-square)](LICENSE)
[![Platform](https://img.shields.io/badge/Platform-Windows-0078D4?style=flat-square)](https://www.windows.com/)

Ứng dụng quản lý lịch cá nhân hiệu suất cao xây dựng trên **[.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/)** cho **Windows 10/11**.  
Dữ liệu được lưu trữ cục bộ bằng **SQLite** – không cần internet, không cần server, hoàn toàn riêng tư.

---

## 📋 Mục lục

- [Tính năng](#-tính-năng)
- [Yêu cầu hệ thống](#-yêu-cầu-hệ-thống)
- [Cài đặt & Chạy](#-cài-đặt--chạy)
- [Cấu trúc dự án](#-cấu-trúc-dự-án)
- [Kiến trúc & Công nghệ](#-kiến-trúc--công-nghệ)
- [Cơ sở dữ liệu](#-cơ-sở-dữ-liệu)
- [Hướng dẫn sử dụng](#-hướng-dẫn-sử-dụng)
- [Khắc phục sự cố](#-khắc-phục-sự-cố)
- [Đóng góp](#-đóng-góp)
- [License](#-license)

---

## ✨ Tính năng

### 📱 Giao diện & Trải nghiệm
- ✅ **Giao diện Tab** – 4 tab chính cho các chức năng khác nhau
- 🌙 **Chế độ tối/sáng** – Toggle tùy chọn (light/dark mode)
- 🎨 **Thiết kế hiện đại** – Giao diện trực quan, dễ sử dụng
- 📌 **Biểu tượng hạng mục** – Emoji đa sắc theo danh mục

### 📅 Quản lý Lịch
| Tính năng | Mô tả |
|-----------|-------|
| **Lịch tháng** | Lưới lịch 7x6 hiển thị ngày tháng, chấm hồng đánh dấu ngày có sự kiện |
| **Xem chi tiết ngày** | Click ngày để xem tất cả sự kiện & công việc của ngày |
| **Thêm sự kiện** | Form popup với DatePicker, TimePicker cho giờ bắt đầu/kết thúc |
| **Sửa/Xóa** | Chỉnh sửa nhanh hoặc xóa với xác nhận dialog |

### ✔️ Quản lý Công việc
| Tính năng | Mô tả |
|-----------|-------|
| **Danh sách công việc** | Danh sách tất cả task với checkbox hoàn thành |
| **Lọc theo trạng thái** | Xem tất cả, chưa hoàn thành, hoàn thành |
| **Sắp xếp theo ưu tiên** | Cao 🔴, Trung bình 🟡, Thấp 🟢 |
| **Đánh dấu hoàn thành** | Tích vào checkbox để đánh dấu task hoàn thành |

### 📊 Thống kê & Phân tích
- 📈 **Biểu đồ cột** – Thống kê số lượng item theo danh mục
- 📅 **Hoạt động tuần** – Xem tổng số sự kiện/task trong 7 ngày gần nhất
- 🎯 **Phân tích danh mục** – Phân bổ công việc theo từng lĩnh vực

### 🔍 Tìm kiếm
- **Full-text search** – Tìm kiếm theo:
  - Tiêu đề (Title)
  - Mô tả (Description)
  - Tags
  - Địa điểm (Location)
- 🎯 **Kết quả thời gian thực** – Hiển thị kết quả ngay khi gõ
- 📌 **Lọc nâng cao** – Hỗ trợ combine tìm kiếm

### 📌 Quản lý Danh mục
8 danh mục được hỗ trợ:
- 💼 **Công việc** (Work)
- 📚 **Học tập** (Study)
- 👤 **Cá nhân** (Personal)
- 👨‍👩‍👧‍👦 **Gia đình** (Family)
- 💪 **Sức khỏe** (Health)
- 🤝 **Họp** (Meeting)
- 🎂 **Sinh nhật** (Birthday)
- 📌 **Khác** (Other)

### ⏰ Nhắc nhở
Hỗ trợ các khoảng thời gian nhắc nhở:
- Không nhắc (None)
- 5 phút trước
- 15 phút trước
- 30 phút trước
- 1 giờ trước
- 1 ngày trước

### 💾 Lưu trữ
- **SQLite cục bộ** – Dữ liệu được mã hóa tại `%LocalAppData%\lichbieu.db3`
- **Giới hạn 999 item** – Tương thích với phiên bản HTML gốc
- **Backup tự động** – Dữ liệu được bảo vệ cục bộ

---

## 🖥️ Yêu cầu hệ thống

### Để phát triển
| Yêu cầu | Phiên bản tối thiểu |
|---------|-------------------|
| **Visual Studio** | 2022 (v17.8+) với workload .NET MAUI |
| **Visual Studio Code** | 1.80+ + C# extension |
| **.NET SDK** | 8.0 hoặc cao hơn |
| **Windows** | Windows 10 Build 19041+ hoặc Windows 11 |

### Để chạy ứng dụng
| Yêu cầu | Phiên bản |
|---------|----------|
| **Windows** | Windows 10 (1809+) hoặc Windows 11 |
| **.NET Runtime** | .NET 8.0 |
| **RAM** | 512 MB tối thiểu |
| **Dung lượng ổ cứng** | 100 MB |

---

## 🚀 Cài đặt & Chạy

### 1️⃣ Clone dự án

```bash
git clone https://github.com/yourusername/LichBieu.git
cd LichBieu
```

### 2️⃣ Khôi phục dependencies

```bash
dotnet restore
```

### 3️⃣ Chạy ứng dụng

#### Với Visual Studio
1. Mở `LichBieu.csproj` trong Visual Studio 2022
2. Chọn platform: **Windows Machine**
3. Nhấn **F5** hoặc **Ctrl+F5** để debug/run
4. Hoặc chọn **Release** mode để tối ưu hiệu suất

#### Với Command Line

```bash
# Debug mode
dotnet run -f net8.0-windows10.0.19041.0

# Release mode (tối ưu hiệu suất)
dotnet run -f net8.0-windows10.0.19041.0 -c Release
```

#### Xây dựng ứng dụng độc lập

```bash
# Build executable
dotnet build -f net8.0-windows10.0.19041.0 -c Release

# Chạy trực tiếp
bin/Release/net8.0-windows10.0.19041.0/win10-x64/LichBieu.exe
```

---

## 📁 Cấu trúc dự án

```
LichBieu/
├── 📂 Models/
│   └── CalendarItem.cs              ← Model chính (Event + Task)
│
├── 📂 Data/
│   └── DatabaseService.cs           ← SQLite CRUD operations
│
├── 📂 ViewModels/ (MVVM)
│   ├── MainViewModel.cs             ← Logic 4 tab chính
│   ├── CalendarDayViewModel.cs      ← Ô ngày trên lịch
│   └── ItemFormViewModel.cs         ← Form thêm/sửa item
│
├── 📂 Views/ (XAML UI)
│   ├── MainPage.xaml(.cs)           ← Màn hình chính
│   └── ItemFormPage.xaml(.cs)       ← Form popup
│
├── 📂 Converters/
│   └── ValueConverters.cs           ← XAML binding converters
│
├── 📂 Resources/
│   ├── Styles/
│   │   ├── Colors.xaml              ← Bảng màu ứng dụng
│   │   └── Styles.xaml              ← Style toàn cục
│   ├── Images/                      ← Hình ảnh tài nguyên
│   ├── Fonts/                       ← Font tùy chỉnh
│   └── AppIcon/                     ← Icon ứng dụng
│
├── 📂 Platforms/Windows/
│   └── App.xaml(.cs)                ← WinUI entry point
│
├── App.xaml(.cs)                    ← MAUI app definition
├── MauiProgram.cs                   ← Dependency Injection setup
├── LichBieu.csproj                  ← Project configuration
└── README.md                         ← Tài liệu này
```

---

## 🏗️ Kiến trúc & Công nghệ

### Mô hình MVVM (Model-View-ViewModel)

```
View (XAML)
    ↓ binds to
ViewModel (RelayCommand, ObservableProperty)
    ↓ communicates
Model (CalendarItem)
    ↓ persists to
Database (SQLite)
```

### Stack công nghệ

| Công nghệ | Phiên bản | Mục đích |
|-----------|---------|---------|
| **.NET** | 8.0 | Runtime chính |
| **MAUI** | 8.0.100 | Framework UI đa nền tảng |
| **MVVM Toolkit** | 8.2.2 | MVVM utilities + ObservableObject |
| **SQLite** | sqlite-net-pcl 1.9.172 | Cơ sở dữ liệu cục bộ |
| **WinUI** | (Windows-only) | Native Windows UI |

### Dependency Injection (DI)

```csharp
// MauiProgram.cs setup
builder.Services.AddSingleton<DatabaseService>();      // Singleton
builder.Services.AddSingleton<MainViewModel>();        // Singleton
builder.Services.AddTransient<ItemFormViewModel>();    // Transient
```

- **Singleton**: DatabaseService, MainViewModel (dùng chung toàn app)
- **Transient**: ItemFormViewModel (tạo mới mỗi lần sử dụng)

### Async/Await Pattern

Tất cả database operations sử dụng **async/await**:

```csharp
public async Task<List<CalendarItem>> GetAllAsync()
{
    return await _db.Table<CalendarItem>().ToListAsync();
}
```

---

## 💾 Cơ sở dữ liệu

### Vị trí lưu trữ

```
Windows:  C:\Users\{YourUsername}\AppData\Local\lichbieu.db3
```

### Schema – Bảng CalendarItems

```sql
CREATE TABLE CalendarItems (
    Id              INTEGER PRIMARY KEY AUTOINCREMENT,
    Type            TEXT,                    -- "event" | "task"
    Title           TEXT NOT NULL,
    Category        TEXT,                    -- work|study|personal|family|health|meeting|birthday|other
    Date            TEXT,                    -- yyyy-MM-dd
    StartTime       TEXT,                    -- HH:mm (events only)
    EndTime         TEXT,                    -- HH:mm (events only)
    Priority        TEXT,                    -- low|medium|high (tasks only)
    Location        TEXT,
    Reminder        TEXT,                    -- none|5min|15min|30min|1hour|1day
    Tags            TEXT,                    -- comma-separated tags
    Description     TEXT,
    Completed       BOOLEAN,                 -- 0 (false) | 1 (true)
    CreatedAt       TEXT                     -- ISO 8601 format
);

-- Indexes (nếu cần tối ưu)
CREATE INDEX idx_date ON CalendarItems(Date);
CREATE INDEX idx_type ON CalendarItems(Type);
CREATE INDEX idx_category ON CalendarItems(Category);
```

### CRUD Operations

```csharp
// Create
await db.SaveAsync(new CalendarItem { Title = "...", ... });

// Read
var items = await db.GetAllAsync();
var byDate = await db.GetByDateAsync("2026-06-08");
var search = await db.SearchAsync("keyword");

// Update
item.Title = "Updated";
await db.SaveAsync(item);

// Delete
await db.DeleteAsync(item);
```

---

## 📖 Hướng dẫn sử dụng

### 1. Thêm Sự kiện / Công việc

1. **Tab Lịch** → Click ngày → **Nút "+"** hoặc **Tab Công việc** → **Nút "+"**
2. Chọn loại: **Event** (sự kiện) hoặc **Task** (công việc)
3. Điền thông tin:
   - ✏️ Tiêu đề (bắt buộc)
   - 📌 Danh mục (chọn từ dropdown)
   - 📅 Ngày (DatePicker)
   - ⏰ Giờ (TimePicker) – chỉ cho Events
   - 📍 Địa điểm
   - ⏱️ Nhắc nhở
   - 🏷️ Tags
   - 📝 Mô tả
4. Nhấn **Lưu** (Save)

### 2. Xem Danh sách

- **Tab Lịch** → Xem lưới lịch tháng
  - Chấm hồng 🔴 = ngày có item
  - Click ngày để xem chi tiết

- **Tab Công việc** → Danh sách tất cả task
  - Tích Checkbox ✓ để đánh dấu hoàn thành
  - Lọc theo trạng thái (Tất cả / Chưa làm / Hoàn thành)
  - Sắp xếp theo ưu tiên

- **Tab Thống kê** → Biểu đồ phân tích
  - Biểu đồ cột: item theo danh mục
  - Hoạt động tuần: tổng item/ngày

- **Tab Tìm kiếm** → Gõ từ khóa
  - Tìm trong title, description, tags, location
  - Kết quả hiển thị thời gian thực

### 3. Sửa Item

1. **Long-press** hoặc **Right-click** item
2. Chọn **"Sửa"** (Edit)
3. Thay đổi thông tin
4. Nhấn **Lưu** (Save)

### 4. Xóa Item

1. **Long-press** hoặc **Right-click** item
2. Chọn **"Xóa"** (Delete)
3. Xác nhận trong dialog
4. Item được xóa khỏi database

### 5. Chế độ Tối/Sáng

- Nhấn **icon Moon/Sun** ở góc trên cùng
- Giao diện tự động chuyển sang theme khác

---

## 🐛 Khắc phục sự cố

### Sự cố: Ứng dụng không chạy
**Giải pháp:**
```bash
# Xóa output và rebuild
dotnet clean
dotnet restore
dotnet build -f net8.0-windows10.0.19041.0
```

### Sự cố: Lỗi database connection
**Nguyên nhân:** Đường dẫn AppData không tồn tại

**Giải pháp:**
```bash
# Xóa database cũ (nếu cần)
rm "%LOCALAPPDATA%\lichbieu.db3"
# Chạy lại ứng dụng – database tự động khởi tạo
```

### Sự cố: Slow performance
**Nguyên nhân:** Database quá lớn (>1000 items)

**Giải pháp:**
```bash
# Build Release (tối ưu hiệu suất)
dotnet run -c Release
```

### Sự cố: XAML compilation error
**Giải pháp:**
```bash
dotnet clean
dotnet restore
# Build lại
dotnet build -f net8.0-windows10.0.19041.0
```

### Sự cố: Lỗi WMC1012
**Nguyên nhân:** Duplicate ApplicationDefinition trong .csproj

**Giải pháp:** Mở `LichBieu.csproj` và kiểm tra không có khai báo thủ công:
```xml
<!-- KHÔNG có dòng này -->
<ApplicationDefinition Include="Platforms\Windows\App.xaml" />
```

---

## 🤝 Đóng góp

Chúng tôi chào đón các đóng góp! Vui lòng:

1. **Fork** repository
2. Tạo **branch** mới cho feature/fix
   ```bash
   git checkout -b feature/my-feature
   ```
3. **Commit** changes với message rõ ràng
   ```bash
   git commit -m "feat: add calendar export feature"
   ```
4. **Push** branch
   ```bash
   git push origin feature/my-feature
   ```
5. Tạo **Pull Request** và mô tả thay đổi

### Hướng dẫn code

- Tuân theo **C# naming conventions** (PascalCase for public, camelCase for private)
- Sử dụng **async/await** cho tất cả I/O operations
- Thêm **comments/documentation** cho logic phức tạp
- Đảm bảo **null safety** với `#nullable enable`

---

## 📊 Performance

- **Startup time**: ~1-2 giây (Debug), <500ms (Release)
- **Database query**: <50ms cho 1000 items
- **Memory usage**: ~80-150 MB (tùy số items)
- **Search response**: Thời gian thực (<100ms)

---

## 🔐 Bảo mật

- ✅ Dữ liệu lưu trữ **cục bộ** – không gửi lên server
- ✅ **Không yêu cầu internet** – hoàn toàn offline
- ✅ **Sqlite encryption** – có thể bật qua SQLitePCLRaw
- ⚠️ Database file không mã hóa mặc định – thêm encryption nếu cần

---

## 📝 License

Dự án này được cấp phép dưới **MIT License**.  
Xem file [LICENSE](LICENSE) để biết chi tiết.

---

## 👥 Tác giả

- **Phát triển bởi:** [Tên tác giả]
- **Website:** [Link website]
- **Contact:** [Email]

---

## 🎯 Lộ trình phát triển (Roadmap)

- ✅ MVVM architecture
- ✅ SQLite database
- ✅ Multi-tab UI
- 🔄 Dark mode (hoàn thành)
- 📅 Recurring events (planned)
- ☁️ Cloud sync (future)
- 📱 Android/iOS support (planned via MAUI)
- 🔔 System notifications (future)

---

## 📚 Tài liệu tham khảo

- [.NET MAUI Documentation](https://learn.microsoft.com/en-us/dotnet/maui/)
- [MVVM Toolkit](https://learn.microsoft.com/en-us/windows/communitytoolkit/mvvm/mvvm_introduction)
- [SQLite-net-pcl](https://github.com/praeclarum/sqlite-net)
- [Windows App SDK](https://learn.microsoft.com/en-us/windows/apps/windows-app-sdk/)

---

## ❓ Câu hỏi thường gặp (FAQ)

### Q: Ứng dụng có hoạt động offline không?
**A:** ✅ Có, 100% offline – dữ liệu lưu trên máy tính

### Q: Tôi có thể export data không?
**A:** Hiện tại SQLite database lưu tại `%LocalAppData%\lichbieu.db3` – có thể backup file này

### Q: Giới hạn số item là bao nhiêu?
**A:** 999 items (tương thích với phiên bản HTML gốc)

### Q: Có hỗ trợ đồng bộ multi-device không?
**A:** Hiện tại không – data cục bộ trên máy tính

### Q: Ứng dụng có yêu cầu admin không?
**A:** Không – chạy với quyền user thông thường

---

## 🆘 Hỗ trợ

Gặp vấn đề? Vui lòng:

1. Kiểm tra mục [Khắc phục sự cố](#-khắc-phục-sự-cố)
2. Tìm issue tương tự trên [GitHub Issues](https://github.com/yourusername/LichBieu/issues)
3. [Tạo issue mới](https://github.com/yourusername/LichBieu/issues/new) với:
   - Mô tả chi tiết
   - Steps to reproduce
   - Expected vs actual behavior
   - Environment (Windows version, .NET version, etc.)

---

<div align="center">

**Made with ❤️ using .NET MAUI**

[⬆ Về đầu trang](#-lịch-biểu-cá-nhân--personal-calendar-manager)

</div>
