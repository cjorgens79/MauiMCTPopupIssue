using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using SkiaSharp;

namespace MauiMCTPopupIssue
{
    public partial class SecondPage : ContentPage
    {
        private bool initialised = false;
        public SecondPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // once only when this form loads
            if (!initialised)
            {
                initialised = true;

                // spawn a task to show each required popup sequentially
                Task.Run(async () =>
                {
                    try
                    {
                        for (int i = 1; i <= 4; i++)
                        {
                            await ShowPopup(i, delayBeforePopup: TimeSpan.FromMilliseconds(500));
                        }
                    }
                    catch (Exception)
                    {
                    }
                });
            }
        }

        private async void OnClose_Clicked(object? sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public async Task ShowPopup(int popupId, TimeSpan? delayBeforePopup = null)
        {
            // if a delay is requested before the popup shows (in situations where they are triggered during form load)
            if (delayBeforePopup != null)
                await Task.Delay(delayBeforePopup.Value);

            var popup = new PopupPage(title: "This is the title", message: $"This is a message number {popupId} to display in the popup");

            var result = await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                return await App.Current.MainPage.ShowPopupAsync(popup, new PopupOptions
                {
                    PageOverlayColor = Colors.Black.WithAlpha(0.5f),
                    
                    // setting the below to false seem to resolve the issue, so it hints that the issue is specific to the click detection that occurs on that overlay
                    //CanBeDismissedByTappingOutsideOfPopup = false,      
                });
            });
        }

        private void glView_PaintSurface(object sender, SkiaSharp.Views.Maui.SKPaintGLSurfaceEventArgs e)
        {
            using (var paint = new SKPaint() { Color = SKColors.Blue })
            {
                e.Surface.Canvas.Clear(SKColors.Red);
                e.Surface.Canvas.DrawCircle(250, 250, 100, paint);
            }
        }
    }
}
