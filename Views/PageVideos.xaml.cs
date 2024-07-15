using CommunityToolkit.Maui.Views;
using PM2T24Video.Models;
using System.IO;
using Microsoft.Maui.Controls;

namespace PM2T24Video.Views
{
    public partial class PageVideos : ContentPage
    {
        public PageVideos()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            VideosPicker.ItemsSource = await App.DataBase.GetListVideos();
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

                var video = new Videos { nombre = result.FileName, fecha = DateTime.Now.ToString() };
                await App.DataBase.AddVideos(video);
                VideosPicker.ItemsSource = await App.DataBase.GetListVideos();
            }
        }

        private void videosSelects_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedVideo = (Videos)VideosPicker.SelectedItem;
            if (selectedVideo != null)
            {
                var videoPath = Path.Combine(FileSystem.AppDataDirectory, selectedVideo.nombre);
                if (File.Exists(videoPath))
                {
                    VideoPlayer.Source = MediaSource.FromFile(videoPath);
                }
                else
                {
                    DisplayAlert("Error", "Video file not found.", "OK");
                }
            }
        }
    }
}
