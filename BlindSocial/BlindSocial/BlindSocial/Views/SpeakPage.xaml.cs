using System;
using System.Linq;
using System.Threading.Tasks;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;
using Xamarin.Forms;

namespace BlindSocial.Views
{
    public partial class SpeakPage : ContentPage
    {
        CrossLocale? selectedLanguage;
        string text;

        public SpeakPage(string text)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(text))
                text = "No se pudo reconocer la imagen";

            this.text = text;

            Task.Run(async () =>
            {
                var languages = await CrossTextToSpeech.Current.GetInstalledLanguages();

                selectedLanguage = languages.Where(x => x.Language == "es" && x.Country == "ES").FirstOrDefault();

                await speakText(text);
            });
        }

        private bool tapHandled;

        void OnSingleTapGestureRecognizerTappedAsync(object sender, EventArgs args)
        {
            tapHandled = false;
            Xamarin.Forms.Device.StartTimer(new TimeSpan(0, 0, 0, 0, 300), Taptimer);
        }

        void OnDoubleTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            tapHandled = true;
            Navigation.PopModalAsync();
        }

        async Task speakText(string text)
        {
            await CrossTextToSpeech.Current.Speak(text, selectedLanguage);
        }

        private bool Taptimer()
        {
            if (!tapHandled)
            {
                tapHandled = true;
                var text = this.text;
                Task.Run(async () =>
                {
                    //var languages = await CrossTextToSpeech.Current.GetInstalledLanguages();

                    //selectedLanguage = languages.Where(x => x.Language == "es" && x.Country == "ES").FirstOrDefault();

                    await speakText(text);
                });
            }
            return false;
        }
    }
}
