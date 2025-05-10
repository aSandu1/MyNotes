using MyNotes.Models;
using System.Collections.ObjectModel;

namespace MyNotes;

public partial class FavoritesPage : ContentPage
{
    public ObservableCollection<Note> Favorites { get; set; } = new();

    public FavoritesPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadFavorites();
    }

    private async Task LoadFavorites()
    {
        Favorites.Clear();
        var notes = await App.Database.GetNoteAsync();
        foreach (var note in notes.Where(n => n.IsFavorite))
        {
            Favorites.Add(new Note
            {
                Id = note.Id,
                Titlu = note.Titlu,
                Continut = note.Continut,
                Data = note.Data,
                IsFavorite = note.IsFavorite,
                ActivitateData = note.ActivitateData,
                ActivitateLocatie = note.ActivitateLocatie
            });
        }
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Note selected)
        {
            await Shell.Current.GoToAsync($"{nameof(NoteDetailsPage)}?{nameof(NoteDetailsPage.NoteId)}={selected.Id}");
            ((CollectionView)sender).SelectedItem = null;
        }
    }
}