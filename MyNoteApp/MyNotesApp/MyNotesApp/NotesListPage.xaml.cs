#pragma warning disable CA1416
using MyNotes.Models;
using MyNotes.Data;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyNotes;

public partial class NotesListPage : ContentPage
{
    public ObservableCollection<Note> Notes { get; } = new();

    public ICommand NoteTappedCommand { get; }
    public ICommand ToggleFavoriteCommand { get; }
    public ICommand DeleteNoteCommand { get; }
    public ICommand EditNoteCommand { get; }

    public NotesListPage()
    {
        InitializeComponent();

        NoteTappedCommand = new Command<Note>(OnNoteTapped);
        ToggleFavoriteCommand = new Command<Note>(OnToggleFavorite);
        DeleteNoteCommand = new Command<Note>(OnDeleteNote);
        EditNoteCommand = new Command<Note>(OnEditNote);


        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadNotes();
    }

    private async Task LoadNotes()
    {
        Notes.Clear();

        var all = await App.Database.GetNoteAsync();
        var active = all
            .Where(n => !n.IsDeleted)
            .OrderByDescending(n => n.IsFavorite)
            .ThenByDescending(n => n.Data);

        foreach (var note in active)
            Notes.Add(note);
    }

    private async void OnNoteTapped(Note note)
    {
        await Shell.Current.GoToAsync($"{nameof(NoteDetailsPage)}?{nameof(NoteDetailsPage.NoteId)}={note.Id}");
    }

    private async void OnToggleFavorite(Note note)
    {
        note.IsFavorite = !note.IsFavorite;
        await App.Database.SaveNoteAsync(note);
        await LoadNotes();
    }

    private async void OnDeleteNote(Note note)
    {
        bool ok = await DisplayAlert("Stergere", $"Stergi '{note.Titlu}'?", "Da", "Nu");
        if (!ok) return;

        await App.Database.SoftDeleteNoteAsync(note);
        Notes.Remove(note);
    }

    private async void OnEditNote(Note note)
    {
        var page = new EditNotePage();
        page.NoteId = note.Id;
        await Navigation.PushAsync(page);
    }
}