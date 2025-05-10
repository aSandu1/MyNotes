#pragma warning disable CS8618
#pragma warning disable CA1416
using MyNotes.Models;

namespace MyNotes;

[QueryProperty(nameof(NoteId), nameof(NoteId))]
public partial class EditNotePage : ContentPage
{
    Note _currentNote;
    private int noteId;
    public int NoteId
    {
        get => noteId;
        set
        {
            noteId = value;
            LoadNoteAsync(value);
        }
    }

    public EditNotePage()
    {
        InitializeComponent();
    }

    async void LoadNoteAsync(int id)
    {
        _currentNote = await App.Database.GetNoteAsync(id);
        if (_currentNote == null)
        {
            await DisplayAlert("Eroare", "Notita nu a fost gssita", "OK");
            await Shell.Current.GoToAsync("..");
            return;
        }

        TitleEntry.Text = _currentNote.Titlu;
        ContentEditor.Text = _currentNote.Continut;
        LocationEntry.Text = _currentNote.ActivitateLocatie ?? "";

        if (_currentNote.ActivitateData.HasValue)
        {
            var dt = _currentNote.ActivitateData.Value;
            ActivityDatePicker.Date = dt.Date;
            ActivityTimePicker.Time = dt.TimeOfDay;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (_currentNote == null)
            return;

        if (string.IsNullOrWhiteSpace(TitleEntry.Text) ||
            string.IsNullOrWhiteSpace(ContentEditor.Text))
        {
            await DisplayAlert("Eroare", "Titlul si continutul nu pot fi goale", "OK");
            return;
        }

        _currentNote.Titlu = TitleEntry.Text.Trim();
        _currentNote.Continut = ContentEditor.Text.Trim();
        _currentNote.ActivitateLocatie = LocationEntry.Text?.Trim() ?? "";

        var d = ActivityDatePicker.Date;
        var t = ActivityTimePicker.Time;
        _currentNote.ActivitateData = new DateTime(
            d.Year, d.Month, d.Day,
            t.Hours, t.Minutes, t.Seconds);

        await App.Database.SaveNoteAsync(_currentNote);
        await DisplayAlert("Salvat", "Notita a fost actualizata", "OK");
        await Shell.Current.GoToAsync("..");
        await Navigation.PopAsync();
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
        await Navigation.PopAsync();
    }
}