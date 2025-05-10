#pragma warning disable CA1416
using MyNotes;
using MyNotesApp;

namespace MyNotesApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MyNotes.NoteDetailsPage), typeof(MyNotes.NoteDetailsPage));
            Routing.RegisterRoute(nameof(MyNotes.CreateNotePage), typeof(MyNotes.CreateNotePage));
            Routing.RegisterRoute(nameof(ActivityInfoPage), typeof(ActivityInfoPage));
            Routing.RegisterRoute(nameof(FavoritesPage), typeof(FavoritesPage));
            Routing.RegisterRoute(nameof(EditNotePage), typeof(EditNotePage));
        }
    }
}