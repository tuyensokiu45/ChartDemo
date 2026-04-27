using Microsoft.AspNetCore.Mvc;

namespace ChartDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartDataController : ControllerBase
    {
        // ===== DỮ LIỆU GIẢ LẬP (thay bằng DbContext thực tế) =====
        private static readonly string[] Months = 
            { "T1", "T2", "T3", "T4", "T5", "T6", "T7", "T8", "T9", "T10", "T11", "T12" };

        /// <summary>
        /// GET /api/chartdata/sales — dữ liệu doanh thu theo tháng
        /// </summary>
        [HttpGet("sales")]
        public IActionResult GetSalesData()
        {
            var rnd = new Random(42); // seed cố định để kết quả ổn định
            var labels = Months;
            var revenueData = new[] { 120, 190, 170, 210, 250, 310, 290, 340, 370, 410, 390, 450 };
            var targetData  = new[] { 150, 200, 200, 220, 260, 300, 300, 350, 360, 400, 400, 440 };

            var result = new
            {
                labels,
                datasets = new object[]
                {
                    new {
                        label       = "Doanh thu thực tế (triệu VND)",
                        data        = revenueData,
                        borderColor = "#6C63FF",
                        backgroundColor = "rgba(108,99,255,0.15)",
                        fill        = true,
                        tension     = 0.4,
                        pointRadius = 5,
                        pointHoverRadius = 8
                    },
                    new {
                        label       = "Doanh thu mục tiêu (triệu VND)",
                        data        = targetData,
                        borderColor = "#FF6584",
                        backgroundColor = "rgba(255,101,132,0.08)",
                        fill        = true,
                        tension     = 0.4,
                        borderDash  = new[] { 8, 4 },
                        pointRadius = 4
                    }
                }
            };
            return Ok(result);
        }

        /// <summary>
        /// GET /api/chartdata/products — dữ liệu bán hàng theo sản phẩm (Bar Chart)
        /// </summary>
        [HttpGet("products")]
        public IActionResult GetProductData()
        {
            var labels = new[] { "Laptop", "Điện thoại", "Máy tính bảng", "Tai nghe", "Smartwatch", "Phụ kiện" };
            var sold   = new[] { 320, 580, 245, 412, 198, 633 };
            var stock  = new[] { 80,  120, 55,  88,  42,  150 };

            var result = new
            {
                labels,
                datasets = new object[]
                {
                    new {
                        label           = "Đã bán",
                        data            = sold,
                        backgroundColor = new[]
                        {
                            "rgba(108,99,255,0.8)", "rgba(255,101,132,0.8)",
                            "rgba(67,206,162,0.8)", "rgba(255,180,0,0.8)",
                            "rgba(32,201,151,0.8)", "rgba(253,126,20,0.8)"
                        },
                        borderRadius = 6,
                        borderSkipped = false
                    },
                    new {
                        label           = "Tồn kho",
                        data            = stock,
                        backgroundColor = new[]
                        {
                            "rgba(108,99,255,0.25)", "rgba(255,101,132,0.25)",
                            "rgba(67,206,162,0.25)", "rgba(255,180,0,0.25)",
                            "rgba(32,201,151,0.25)", "rgba(253,126,20,0.25)"
                        },
                        borderRadius = 6,
                        borderSkipped = false
                    }
                }
            };
            return Ok(result);
        }

        /// <summary>
        /// GET /api/chartdata/category — phân bố danh mục (Doughnut / Pie Chart)
        /// </summary>
        [HttpGet("category")]
        public IActionResult GetCategoryData()
        {
            var result = new
            {
                labels = new[] { "Laptop", "Điện thoại", "Máy tính bảng", "Tai nghe", "Smartwatch", "Phụ kiện" },
                datasets = new object[]
                {
                    new {
                        data = new[] { 24, 35, 15, 12, 8, 6 },
                        backgroundColor = new[]
                        {
                            "#6C63FF", "#FF6584", "#43CEA2",
                            "#FFB400", "#20C997", "#FD7E14"
                        },
                        borderWidth = 3,
                        borderColor = "#ffffff",
                        hoverOffset  = 12
                    }
                }
            };
            return Ok(result);
        }

        /// <summary>
        /// GET /api/chartdata/realtime — dữ liệu ngẫu nhiên cho biểu đồ realtime
        /// </summary>
        [HttpGet("realtime")]
        public IActionResult GetRealtimePoint()
        {
            var rnd   = new Random();
            var value = rnd.Next(30, 120);
            return Ok(new { value, timestamp = DateTime.Now.ToString("HH:mm:ss") });
        }

        /// <summary>
        /// GET /api/chartdata/radar — so sánh kỹ năng (Radar Chart)
        /// </summary>
        [HttpGet("radar")]
        public IActionResult GetRadarData()
        {
            var result = new
            {
                labels = new[] { "Hiệu năng", "Thiết kế", "Bảo mật", "Hỗ trợ", "Tích hợp", "Chi phí" },
                datasets = new object[]
                {
                    new {
                        label           = "Chart.js",
                        data            = new[] { 85, 80, 70, 90, 75, 95 },
                        backgroundColor = "rgba(108,99,255,0.2)",
                        borderColor     = "#6C63FF",
                        pointBackgroundColor = "#6C63FF"
                    },
                    new {
                        label           = "ApexCharts",
                        data            = new[] { 80, 90, 75, 80, 80, 85 },
                        backgroundColor = "rgba(255,101,132,0.2)",
                        borderColor     = "#FF6584",
                        pointBackgroundColor = "#FF6584"
                    },
                    new {
                        label           = "Syncfusion",
                        data            = new[] { 95, 92, 90, 85, 95, 40 },
                        backgroundColor = "rgba(67,206,162,0.2)",
                        borderColor     = "#43CEA2",
                        pointBackgroundColor = "#43CEA2"
                    }
                }
            };
            return Ok(result);
        }
    }
}
