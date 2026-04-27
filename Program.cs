var builder = WebApplication.CreateBuilder(args);

// ─── Đăng ký dịch vụ ───────────────────────────────────────────────────────
builder.Services.AddControllersWithViews();

// Cho phép controller API trả JSON chuẩn
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        // Giữ nguyên tên property (không camelCase)
        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

var app = builder.Build();

// ─── Middleware pipeline ────────────────────────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Route mặc định: controller=Home, action=Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ánh xạ API controllers (dùng attribute routing [Route])
app.MapControllers();

app.Run();
