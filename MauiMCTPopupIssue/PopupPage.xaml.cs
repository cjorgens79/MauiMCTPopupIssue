using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Views;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiMCTPopupIssue;

public partial class PopupPage : Popup
{
    public bool? DontShowAgain { get; private set; } = null;
    public bool? Result { get; private set; } = null;
    public string Title { get; private set; } = "";
    public string Message { get; private set; } = "";

    public PopupPage(string title, string message)
    {
        Title = title;
        Message = message;

        InitializeComponent();

        BindingContext = this;
    }

    private async void Yes_Clicked(object sender, EventArgs e)
	{
        Result = true;
        await CloseAsync();
    }

    private async void No_Clicked(object sender, EventArgs e)
    {
        Result = false;
        await CloseAsync();
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        Result = null;
        await this.CloseAsync();
    }
}