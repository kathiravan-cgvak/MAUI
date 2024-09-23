namespace Task1;

public partial class About : ContentPage
{
	public About()
	{
		InitializeComponent();
	}
    private async void OnNavigationClicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new MainPage());
    }
}