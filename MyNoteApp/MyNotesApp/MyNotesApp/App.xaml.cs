using MyNotes.Data;
using MyNotes.Models;
using MyNotesApp;

namespace MyNotes
{
    public partial class App : Application
    {
        static NoteDB _database;
        public static NoteDB Database
        {
            get
            {
                if (_database == null)
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "notite.db3");
                    _database = new NoteDB(path);
                }
                return _database;
            }


        }

        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}