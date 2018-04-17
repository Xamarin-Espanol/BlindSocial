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

            Current.MainPage = new MediaPage();
            //SetMainPage();
        }

        //public static void SetMainPage()
        //{
        //    Current.MainPage = new TabbedPage
        //    {
        //        Children =
        //        {
        //            new NavigationPage(new ItemsPage())
        //            {
        //                Title = "Browse",
        //                Icon = Device.OnPlatform("tab_feed.png",null,null)
        //            },
        //            new NavigationPage(new AboutPage())
        //            {
        //                Title = "About",
        //                Icon = Device.OnPlatform("tab_about.png",null,null)
        //            },
        //        }
        //    };
        //}
    }
}
