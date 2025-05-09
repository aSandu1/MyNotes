using MyNotes.Models;
using MyNotes.Data;
using static MyNotes.CreateNotePage;  // pentru CreateNotePage
using static MyNotes.NotesListPage;

namespace MyNotes
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        // Eveniment pentru butonul de creare notita
        private async void OnCreateNoteClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateNotePage()); // Asigură-te că ai CreateNotePage creat
        }

        // Eveniment pentru butonul de vizualizare notite
        private async void OnViewNotesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NotesListPage()); // Asigură-te că ai NotesListPage creat
        }
    }
}
