# Lịch Biểu Cá Nhân – .NET MAUI (Windows, .NET 8)

Ứng dụng quản lý lịch cá nhân chuyển đổi từ HTML sang .NET MAUI.
Dữ liệu lưu local bằng **SQLite** – không cần internet, không cần server.

---

## Cấu trúc dự án

```
LichBieu/
├── Models/
│   └── CalendarItem.cs          ← Model cho Sự kiện & Công việc
├── Data/
│   └── DatabaseService.cs       ← SQLite CRUD (GetAll, Save, Delete, Search)
├── ViewModels/
│   ├── CalendarDayViewModel.cs  ← Ô ngày trên lịch
│   ├── MainViewModel.cs         ← Logic 4 tab
│   └── ItemFormViewModel.cs     ← Form thêm/sửa
├── Views/
│   ├── MainPage.xaml(.cs)       ← Màn hình chính
│   └── ItemFormPage.xaml(.cs)   ← Form popup
├── Converters/
│   └── ValueConverters.cs       ← Converter cho XAML binding
└── Resources/Styles/
    ├── Colors.xaml              ← Bảng màu
    └── Styles.xaml              ← Style toàn cục
```

---

## Yêu cầu môi trường

- **Visual Studio 2022** (v17.8+) với workload **.NET MAUI**
- hoặc **Visual Studio Code** + .NET MAUI extension
- **.NET 8 SDK**
- **Windows 10** 1809 trở lên (build 17763+)

---

## Cách chạy

### Visual Studio
1. Mở file `LichBieu.csproj`
2. Chọn target **Windows Machine**
3. Nhấn **F5** (hoặc Ctrl+F5)

### Command Line
```bash
cd LichBieu
dotnet restore
dotnet build -f net8.0-windows10.0.19041.0
dotnet run -f net8.0-windows10.0.19041.0
```

---

## Tính năng

| Tính năng | Mô tả |
|---|---|
| 📅 **Tab Lịch** | Lưới lịch tháng, chấm hồng = ngày có sự kiện |
| ✅ **Tab Công việc** | Danh sách task với checkbox, filter, sort theo priority |
| 📊 **Tab Thống kê** | Bar chart theo danh mục + biểu đồ hoạt động tuần |
| 🔍 **Tab Tìm kiếm** | Full-text search theo tiêu đề, mô tả, tags, địa điểm |
| ➕ **Thêm/Sửa** | Form popup với DatePicker, TimePicker, Picker |
| 🗑️ **Xóa** | Confirm dialog trước khi xóa |
| 🌙 **Dark mode** | Toggle sáng/tối |
| 💾 **SQLite** | Dữ liệu lưu tại `%LocalAppData%\lichbieu.db3` |

---

## Danh mục hỗ trợ

💼 Công việc · 📚 Học tập · 👤 Cá nhân · 👨‍👩‍👧‍👦 Gia đình  
💪 Sức khỏe · 🤝 Họp · 🎂 Sinh nhật · 📌 Khác

---

## Ghi chú kỹ thuật

- **MVVM** với `CommunityToolkit.Mvvm` (`[ObservableProperty]`, `[RelayCommand]`)
- **SQLite** qua `sqlite-net-pcl` – async hoàn toàn
- **Dependency Injection** qua `MauiProgram.cs`
- Giới hạn **999 mục** (như phiên bản HTML gốc)
