using System.Globalization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SahabatAnak.Api.DTOs.Reports;
using SahabatAnak.Api.Services;

namespace SahabatAnak.Api.Controllers;

[ApiController]
[Route("api/reports")]
public sealed class ReportsController : ControllerBase
{
    private readonly IReportsService _reports;
    private readonly IWebHostEnvironment _env;

    public ReportsController(IReportsService reports, IWebHostEnvironment env)
    {
        _reports = reports;
        _env = env;
    }

    [HttpPost]
    public async Task<ActionResult<SubmitReportResponseDto>> Submit([FromBody] SubmitReportRequestDto dto, CancellationToken ct)
    {
        try
        {
            var res = await _reports.SubmitAsync(dto, ct);
            return Ok(res);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<SubmitReportResponseDto>> SubmitForm([FromForm] SubmitReportFormRequestDto dto, CancellationToken ct)
    {
        try
        {
            var evidenceUrls = await SaveEvidenceAsync(dto.EvidenceFiles, ct);
            var mapped = new SubmitReportRequestDto
            {
                IsAnonymous = dto.IsAnonymous,
                Name = dto.Name ?? string.Empty,
                Contact = dto.Contact ?? string.Empty,
                Chronology = dto.Chronology ?? string.Empty,
                EvidenceUrls = evidenceUrls,
            };
            var res = await _reports.SubmitAsync(mapped, ct);
            return Ok(res);
        }
        catch (ArgumentException e)
        {
            return BadRequest(new { message = e.Message });
        }
    }

    private async Task<List<string>> SaveEvidenceAsync(IFormFile[]? files, CancellationToken ct)
    {
        if (files is null || files.Length == 0) return new List<string>();

        var maxFiles = Math.Min(files.Length, 3);
        var safeFiles = files.Take(maxFiles).ToArray();
        var allowedExt = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".jpg", ".jpeg", ".png", ".webp", ".pdf" };

        foreach (var f in safeFiles)
        {
            if (f.Length <= 0) continue;
            if (f.Length > 5 * 1024 * 1024) throw new ArgumentException("Ukuran file bukti maksimal 5MB per file.");
            var ext = Path.GetExtension(f.FileName ?? string.Empty);
            if (!allowedExt.Contains(ext)) throw new ArgumentException("Format bukti hanya mendukung JPG, PNG, WEBP, atau PDF.");
        }

        var now = DateTime.UtcNow;
        var y = now.ToString("yyyy", CultureInfo.InvariantCulture);
        var m = now.ToString("MM", CultureInfo.InvariantCulture);
        var webRoot = string.IsNullOrWhiteSpace(_env.WebRootPath) ? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot") : _env.WebRootPath;
        var relativeDir = Path.Combine("uploads", "evidence", y, m);
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

        return urls;
    }
}
