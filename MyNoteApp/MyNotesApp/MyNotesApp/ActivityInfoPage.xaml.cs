namespace MyNotes;

public partial class ActivityInfoPage : ContentPage
{
    public static DateTime? SelectedDateTime { get; private set; }
    public static string SelectedLocation { get; private set; }

    public ActivityInfoPage()
    {
        InitializeComponent();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        var date = ActivityDatePicker.Date;
        var time = ActivityTimePicker.Time;

        SelectedDateTime = date + time;
        SelectedLocation = LocationEntry.Text;

        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        SelectedDateTime = null;
        SelectedLocation = null;

        await Navigation.PopAsync();
    }
}