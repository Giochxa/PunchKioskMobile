using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Storage;
using PunchKioskMobile.Models;
using PunchKioskMobile.Services;

namespace PunchKioskMobile.ViewModels;

public class PunchViewModel : INotifyPropertyChanged
{
    private string _enteredId;
    public string EnteredId
    {
        get => _enteredId;
        set
        {
            _enteredId = value;
            OnPropertyChanged();
        }
    }

    public ICommand CapturePhotoCommand { get; }
    public ICommand PunchCommand { get; }

    public PunchViewModel()
    {
        CapturePhotoCommand = new Command(CapturePhoto);
        PunchCommand = new Command(PunchInOut);
    }

    private async void CapturePhoto()
    {
        try
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            if (photo != null)
            {
                await Application.Current.MainPage.DisplayAlert("Photo Captured", photo.FullPath, "OK");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private void PunchInOut()
    {
        using var db = new AppDbContext();
        db.Punches.Add(new Punch { EmployeeId = EnteredId, Timestamp = DateTime.Now });
        db.SaveChanges();
        Application.Current.MainPage.DisplayAlert("Success", "Punch recorded!", "OK");
        EnteredId = string.Empty;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
