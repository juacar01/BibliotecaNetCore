namespace Biblioteca.Api.Services;

public class PhysicalFileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<PhysicalFileStorageService> _logger;

    public PhysicalFileStorageService(IWebHostEnvironment env, ILogger<PhysicalFileStorageService> logger)
    {
        _env = env;
        _logger = logger;
    }

    public async Task<string?> SaveUploadAsync(IFormFile file, string folder = "Uploads", CancellationToken ct = default)
    {
        if (file == null || file.Length == 0) return null;

        // Fallback si WebRootPath es null
        var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
        var uploadsDir = Path.Combine(webRoot, folder);
        Directory.CreateDirectory(uploadsDir);

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        var fileName = $"{Guid.NewGuid()}{ext}";
        var fullPath = Path.Combine(uploadsDir, fileName);

        await using var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true);
        await file.CopyToAsync(stream, ct);

        // Devolver ruta relativa (para almacenar en BD o generar URL)
        return Path.Combine(folder, fileName).Replace('\\', '/');
    }
}