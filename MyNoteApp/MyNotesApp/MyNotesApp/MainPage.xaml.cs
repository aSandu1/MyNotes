using MyNotes.Models;
using MyNotes.Data;
using static MyNotes.CreateNotePage;
using static MyNotes.NotesListPage;

namespace MyNotes
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCreateNoteClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateNotePage());
        }

        private async void OnViewNotesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotesListPage());
        }

        private async void OnViewFavoritesClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(FavoritesPage));
        }
        private async void OnDeletedNotesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeletedNotesPage());
        }

    }
}
