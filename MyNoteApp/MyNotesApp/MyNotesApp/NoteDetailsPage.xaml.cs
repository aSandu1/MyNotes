using MyNotes.Data;
using MyNotes.Models;
using System;

namespace MyNotes;

[QueryProperty(nameof(NoteId), nameof(NoteId))]
public partial class NoteDetailsPage : ContentPage
{
    public int NoteId { get; set; }
    private Note currentNote;

    public NoteDetailsPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var note = await App.Database.GetNoteAsync(NoteId);
        currentNote = await App.Database.GetNoteAsync(NoteId);

        if (note != null)
        {
            TitleLabel.Text = note.Titlu;
            ContentLabel.Text = note.Continut;
            CreatedDateLabel.Text = note.Data.ToString("dd MMM yyyy HH:mm");

            FavoriteButton.Source = currentNote.IsFavorite ? "star_filled.png" : "star_outline.png";

            if (note.ActivitateData.HasValue)
            {
                ActivityDateLabel.Text = $"Data: {note.ActivitateData.Value:dd MMM yyyy HH:mm}";
                ActivityDateLabel.IsVisible = true;
            }

            if (!string.IsNullOrWhiteSpace(note.ActivitateLocatie))
            {
                LocationLabel.Text = $"Location: {note.ActivitateLocatie}";
                LocationLabel.IsVisible = true;
            }
        }
    }

    private async void OnFavoriteClicked(object sender, EventArgs e)
    {
        if (currentNote == null) return;

        currentNote.IsFavorite = !currentNote.IsFavorite;
        FavoriteButton.Source = currentNote.IsFavorite ? "star_filled.png" : "star_outline.png";
        await App.Database.SaveNoteAsync(currentNote);
    }

    private async void OnExportClicked(object sender, EventArgs e)
    {
        if (currentNote == null) return;
        try
        {
            await ExcelExportService.ExportNoteAsync(currentNote);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Eroare Export", ex.Message, "OK");
        }
    }
}