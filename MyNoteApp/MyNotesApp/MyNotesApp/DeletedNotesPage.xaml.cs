using MyNotes.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyNotes;

public partial class DeletedNotesPage : ContentPage
{
    public ObservableCollection<Note> DeletedNotes { get; set; } = new();
    public ICommand RecoverCommand { get; }
    public ICommand PermanentDeleteCommand { get; }

    public DeletedNotesPage()
    {
        InitializeComponent();
        RecoverCommand = new Command<Note>(OnRecover);
        PermanentDeleteCommand = new Command<Note>(OnPermanentDelete);
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadDeletedNotes();
    }

    private async Task LoadDeletedNotes()
    {
        DeletedNotes.Clear();
        var all = await App.Database.GetNoteAsync();
        foreach (var note in all.Where(n => n.IsDeleted))
            DeletedNotes.Add(note);
    }

    private async void OnRecover(Note note)
    {
        note.IsDeleted = false;
        await App.Database.SaveNoteAsync(note);
        DeletedNotes.Remove(note);
    }

    private async void OnPermanentDelete(Note note)
    {
        var confirm = await DisplayAlert("Confirmare", $"Stergi definitiv '{note.Titlu}'?", "Da", "Nu");
        if (confirm)
        {
            await App.Database.DeleteNoteAsync(note);
            await LoadDeletedNotes();
        }
    }
}