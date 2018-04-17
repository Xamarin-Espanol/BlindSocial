using BlindSocial.Services;
using Plugin.Media;
using Plugin.TextToSpeech;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BlindSocial.Views
{
    public partial class IndexPage : ContentPage
    {
        ApiService apiService;

        public IndexPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            apiService = new ApiService("api/BlindSocial", string.Empty);
        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                Directory = "Sample",
                Name = $"BlindSocial{ DateTime.Now.Ticks }.jpg"
            });

            if (file == null)
                return;

            var languages = await CrossTextToSpeech.Current.GetInstalledLanguages();

            var selectedLanguage = languages.Where(x => x.Language == "es" && x.Country == "ES").FirstOrDefault();

            await CrossTextToSpeech.Current.Speak("Se esta procesando la imagen, aguarde por favor", selectedLanguage);
            
            var blobStorage = DependencyService.Get<IBlobStorage>();
            var url = await blobStorage.PerformBlobOperation(file.GetStream());

            var rootObject = await apiService.Analize(url);
            var text = string.Empty;

            if (rootObject.Description.Captions.Any())
            {
                text += "En la imagen puedo reconocer lo siguiente: \n";

                var englishText = string.Empty;
                foreach (var caption in rootObject.Description.Captions)
                {
                    englishText += caption.Text;
                }

                text += englishText;
            }

            await Navigation.PushModalAsync(new SpeakPage(text));
        }

    }
}
