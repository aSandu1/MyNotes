using MyNotes.Models;
using MyNotes.Data;
using MyNotesApp;

namespace MyNotes
{
    public partial class CreateNotePage : ContentPage
    {

        private bool isFavorite = false;
        public CreateNotePage()
        {
            InitializeComponent();
        }

        private async void OnSaveNoteClicked(object sender, EventArgs e)
        {
            var note = new Note
            {
                Titlu = NoteTitleEntry.Text,
                Continut = NoteContentEditor.Text,
                Data = DateTime.Now,
                ActivitateData = ActivityInfoPage.SelectedDateTime,
                ActivitateLocatie = ActivityInfoPage.SelectedLocation,
                IsFavorite = isFavorite
            };

            await App.Database.SaveNoteAsync(note);
            await DisplayAlert("Success", "Note Saved!", "OK");
            await Navigation.PopAsync();
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void OnSetActivityInfoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ActivityInfoPage());
        }

        private void OnFavoriteClicked(object sender, EventArgs e)
        {
            isFavorite = !isFavorite;
            FavoriteButton.Source = isFavorite ? "star_filled.png" : "star_outline.png";
        }
    }
}
