# ChartDemo — ASP.NET Core MVC + Chart.js

Demo toàn diện tích hợp **Chart.js 4.x** với **ASP.NET Core MVC (.NET 8)**.

---

## Yêu cầu hệ thống

| Công cụ | Phiên bản |
|---------|-----------|
| .NET SDK | 8.0+ |
| Visual Studio | 2022 (v17.8+) hoặc VS Code |
| Hệ điều hành | Windows / macOS / Linux |

---

## Hướng dẫn chạy project

### Cách 1: Visual Studio 2022
```
1. Mở file ChartDemo.csproj
2. Nhấn F5 (hoặc Ctrl+F5 để chạy không debug)
3. Trình duyệt tự mở tại https://localhost:5001
```

### Cách 2: .NET CLI (Terminal)
```bash
# Di chuyển vào thư mục project
cd ChartDemo

# Restore dependencies
dotnet restore

# Chạy project
dotnet run

# Hoặc chạy với hot-reload
dotnet watch run
```

---

## Cấu trúc thư mục

```
ChartDemo/
├── Controllers/
│   ├── HomeController.cs          # Controller MVC (Index, Dashboard)
│   └── ChartDataController.cs     # API Controller — trả JSON cho Chart.js
├── Views/
│   ├── Home/
│   │   ├── Index.cshtml           # Trang chủ + bảng so sánh thư viện
│   │   └── Dashboard.cshtml       # Dashboard với 5 loại biểu đồ
│   └── Shared/
│       └── _Layout.cshtml         # Layout chung (sidebar, topbar)
├── Program.cs                     # Entry point, DI container
├── appsettings.json
└── ChartDemo.csproj
```

---

## 🌐 API Endpoints

| Endpoint | Mô tả | Dùng cho |
|----------|-------|---------|
| `GET /api/chartdata/sales`    | Doanh thu 12 tháng | Line Chart |
| `GET /api/chartdata/products` | Bán hàng theo sản phẩm | Bar Chart |
| `GET /api/chartdata/category` | Tỷ trọng danh mục (%) | Doughnut Chart |
| `GET /api/chartdata/radar`    | So sánh thư viện Chart | Radar Chart |
| `GET /api/chartdata/realtime` | Điểm dữ liệu ngẫu nhiên | Realtime Chart |

Tất cả endpoint trả về **JSON** — có thể test trực tiếp trên trình duyệt.

---

## Các loại biểu đồ được demo

| Biểu đồ | Mô tả |
|---------|-------|
| **Line Chart** | Doanh thu thực tế vs mục tiêu theo tháng, fill area, tension |
| **Bar Chart** | Sản phẩm đã bán và tồn kho, grouped bars, màu sắc từng danh mục |
| **Doughnut Chart** | Tỷ trọng danh mục sản phẩm, custom legend |
| **Radar Chart** | So sánh Chart.js / ApexCharts / Syncfusion theo 6 tiêu chí |
| **Realtime Chart** | Fetch API mỗi 1.5s, tự động thêm / xóa điểm, nút pause/resume |

---

## 🔧 Hướng dẫn kết nối Database thực tế

Để thay dữ liệu giả bằng database thực:

### 1. Cài Entity Framework Core
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### 2. Tạo DbContext
```csharp
public class AppDbContext : DbContext
{
    public DbSet<SaleRecord> SaleRecords { get; set; }
    // ...
}
```

### 3. Inject vào Controller
```csharp
public class ChartDataController : ControllerBase
{
    private readonly AppDbContext _db;
    public ChartDataController(AppDbContext db) => _db = db;

    [HttpGet("sales")]
    public async Task<IActionResult> GetSalesData()
    {
        var data = await _db.SaleRecords
            .GroupBy(r => r.Month)
            .Select(g => new { Month = g.Key, Total = g.Sum(r => r.Amount) })
            .ToListAsync();
        return Ok(data);
    }
}
```

---

## Thư viện sử dụng (CDN — không cần cài NuGet)

- **Chart.js 4.4.3** — `cdn.jsdelivr.net`
- **Bootstrap 5.3.3** — CSS framework
- **Bootstrap Icons 1.11.3** — Icon set

---

## Hướng phát triển tiếp theo

- [ ] Tích hợp **SignalR** để push dữ liệu realtime từ server
- [ ] Kết nối **SQL Server** qua Entity Framework Core
- [ ] Thêm **ApexCharts** để so sánh trực tiếp
- [ ] Export biểu đồ sang PDF / PNG
- [ ] Authentication + phân quyền xem dashboard

---

*Tài liệu tham khảo: [learn.microsoft.com](https://learn.microsoft.com/en-us/aspnet/core/) · [chartjs.org](https://www.chartjs.org/)*
