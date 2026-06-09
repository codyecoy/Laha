using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SahabatAnak.Api.Controllers.Admin;

[ApiController]
[Authorize(Roles = "Admin,Editor,Operator")]
[Route("api/admin/uploads")]
public sealed class AdminUploadsController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public AdminUploadsController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<object>> Upload([FromForm] IFormFile[] files, CancellationToken ct)
    {
        if (files is null || files.Length == 0) return BadRequest(new { message = "File kosong." });

        var maxFiles = Math.Min(files.Length, 5);
        var safeFiles = files.Take(maxFiles).ToArray();
        var allowedExt = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".jpg", ".jpeg", ".png", ".webp" };

        foreach (var f in safeFiles)
        {
            if (f.Length <= 0) continue;
            if (f.Length > 6 * 1024 * 1024) return BadRequest(new { message = "Ukuran file maksimal 6MB per file." });
            var ext = Path.GetExtension(f.FileName ?? string.Empty);
            if (!allowedExt.Contains(ext)) return BadRequest(new { message = "Format file hanya mendukung JPG, PNG, atau WEBP." });
        }

        var now = DateTime.UtcNow;
        var y = now.ToString("yyyy", CultureInfo.InvariantCulture);
        var m = now.ToString("MM", CultureInfo.InvariantCulture);
        var webRoot = string.IsNullOrWhiteSpace(_env.WebRootPath) ? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot") : _env.WebRootPath;
        var relativeDir = Path.Combine("uploads", "admin", y, m);
        var fullDir = Path.Combine(webRoot, relativeDir);
        Directory.CreateDirectory(fullDir);

        var urls = new List<string>();
        foreach (var f in safeFiles)
        {
            if (f.Length <= 0) continue;
            var ext = Path.GetExtension(f.FileName ?? string.Empty);
            var fileName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(fullDir, fileName);
            await using (var stream = System.IO.File.Create(fullPath))
            {
                await f.CopyToAsync(stream, ct);
            }
            var url = $"{Request.Scheme}://{Request.Host}/{relativeDir.Replace('\\', '/')}/{fileName}";
            urls.Add(url);
        }

        return Ok(new { urls });
    }
}

