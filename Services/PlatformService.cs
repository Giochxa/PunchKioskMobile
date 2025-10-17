using Microsoft.Maui.Media;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Storage;

namespace PunchKioskMobile.Services
{
    public class PlatformService : IPlatformService
    {
        public async Task<string> CapturePhotoAsync()
        {
            try
            {
                if (!MediaPicker.IsCaptureSupported)
                    return null;

                var photo = await MediaPicker.CapturePhotoAsync();
                return photo?.FullPath;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> SaveFileAsync(string filename, byte[] data)
        {
            try
            {
                var filePath = Path.Combine(FileSystem.CacheDirectory, filename);
                await File.WriteAllBytesAsync(filePath, data);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task ShareFileAsync(string filepath, string title)
        {
            try
            {
                await Share.Default.RequestAsync(new ShareFileRequest
                {
                    Title = title,
                    File = new ShareFile(filepath)
                });
            }
            catch
            {
                // Sharing not supported
            }
        }

        public async Task<string> GetDeviceInfoAsync()
        {
            return $"{DeviceInfo.Manufacturer} {DeviceInfo.Model} ({DeviceInfo.Platform})";
        }
    }
}