namespace BlindSocial.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new BlindSocial.App());
        }
    }
}