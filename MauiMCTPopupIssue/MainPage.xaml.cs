namespace MauiMCTPopupIssue
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnOpenSecondWindow_Clicked(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new SecondPage());
        }
    }
}
