using Microsoft.AspNetCore.StaticFiles;

namespace BackendServer.Services;
public interface IFileManager
{
    Task<string> UploadFile(IFormFile _IFormFile);
    Task<(byte[], string, string)> DownloadFile(string FileName);
}
public class FileManager : IFileManager
{
    public async Task<string> UploadFile(IFormFile _IFormFile)
    {
        string FileName = "";
        try
        {
            FileInfo _FileInfo = new FileInfo(_IFormFile.FileName);
            FileName = _IFormFile.FileName + "_" + DateTime.Now.Ticks.ToString() + _FileInfo.Extension;
            var _GetFilePath = GetFilePath(FileName);
            using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
            {
                await _IFormFile.CopyToAsync(_FileStream);
            }
            return FileName;
        }
        catch (Exception)
        {
            throw;
        }

    }
    public async Task<(byte[], string, string)> DownloadFile(string FileName)
    {
        try
        {
            var _GetFilePath = GetFilePath(FileName);
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(_GetFilePath, out var _ContentType))
            {
                _ContentType = "application/octet-stream";
            }
            var _ReadAllBytesAsync = await File.ReadAllBytesAsync(_GetFilePath);
            return (_ReadAllBytesAsync, _ContentType, Path.GetFileName(_GetFilePath));
        }
        catch (Exception)
        {
            throw;
        }
    }
    private static string GetFilePath(string FileName)
    {
        var _GetStaticContentDirectory = GetStaticContentDirectory();
        var result = Path.Combine(_GetStaticContentDirectory, FileName);
        return result;
    }
    private static string GetStaticContentDirectory()
    {
        var result = Path.Combine(Directory.GetCurrentDirectory(), "static");
        if (!Directory.Exists(result))
        {
            Directory.CreateDirectory(result);
        }
        return result;
    }
}