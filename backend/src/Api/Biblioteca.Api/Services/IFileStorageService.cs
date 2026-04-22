namespace Biblioteca.Api.Services;

public interface IFileStorageService
{
    Task<string?> SaveUploadAsync(IFormFile file, string folder = "Uploads", CancellationToken ct = default);   
}
