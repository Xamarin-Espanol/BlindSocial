using BlindSocial.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BlindSocial
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new IndexPage();
        }
    }
}
