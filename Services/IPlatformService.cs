namespace PunchKioskMobile.Services
{
    public interface IPlatformService
    {
        Task<string> CapturePhotoAsync();
        Task<bool> SaveFileAsync(string filename, byte[] data);
        Task ShareFileAsync(string filepath, string title);
        Task<string> GetDeviceInfoAsync();
    }
}