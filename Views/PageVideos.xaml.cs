using PM2T24Video.Models;
using System.IO;

namespace PM2T24Video.Views;

public partial class PageVideos : ContentPage
{
    public PageVideos()
    {
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        VideosList.ItemsSource = await App.DataBase.GetListVideos();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var result = await MediaPicker.CaptureVideoAsync();
        if (result != null)
        {
            var newFile = Path.Combine(FileSystem.AppDataDirectory, result.FileName);
            using (var stream = await result.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
            {
                await stream.CopyToAsync(newStream);
            }
        }
    }
}