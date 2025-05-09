using MyNotes.Models;
using MyNotes.Data;
using MyNotesApp;

namespace MyNotes
{
    public partial class CreateNotePage : ContentPage
    {
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
                Data = DateTime.Now
            };

            await App.Database.SaveNoteAsync(note);
            await DisplayAlert("Success", "Note Saved!", "OK");
            await Navigation.PopAsync();
        }
    }
}
