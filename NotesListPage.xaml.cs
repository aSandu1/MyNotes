using MyNotes.Models;
using MyNotes.Data;
using System.Collections.ObjectModel;

namespace MyNotes
{
    public partial class NotesListPage : ContentPage
    {
        public ObservableCollection<Note> Notes { get; set; }

        public NotesListPage()
        {
            InitializeComponent();
            Notes = new ObservableCollection<Note>();
            LoadNotes();
            BindingContext = this;
        }

        private async void LoadNotes()
        {
            var notes = await App.Database.GetNoteAsync();
            foreach (var note in notes)
            {
                Notes.Add(note);
            }
        }
    }
}
