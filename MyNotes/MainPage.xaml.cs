using MyNotes.Models;
using MyNotes.Data;
using MyNotes.Pages;



namespace MyNotes
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            
            InitializeComponent();
            TestSQLite();
        }

        private async void OnViewTrashClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TrashPage());
        }


        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        private async void TestSQLite()
        {
            DebugText.Text = "Apel funcție";

            // Salvăm o notiță nouă
            await App.Database.SaveNoteAsync(new Note
            {
                Titlu = "Din MainPage",
                Continut = "Notiță de test",
                Data = DateTime.Now
            });

            // Obținem toate notițele
            var notite = await App.Database.GetNoteAsync();

            // Dacă avem cel puțin 2, le ștergem și le punem în Trash
            if (notite.Count >= 2)
            {
                DebugText.Text += $"\nMutăm primele 2 notițe în trash...";
                await App.Database.MoveToTrashAsync(notite[0]);
                await App.Database.MoveToTrashAsync(notite[1]);
            }

            // Re-obținem notițele rămase
            notite = await App.Database.GetNoteAsync();
            DebugText.Text += $"\n\n--- NOTIȚE ({notite.Count}) ---";
            foreach (var n in notite)
            {
                DebugText.Text += $"\n{n.Titlu} - {n.Continut}";
            }

            // Obținem și afișăm notițele din Trash
            var trash = await App.Database.GetTrashNoteAsync();
            DebugText.Text += $"\n\n--- TRASH ({trash.Count}) ---";
            foreach (var t in trash)
            {
                DebugText.Text += $"\n{t.Titlu} - {t.Continut} (șters la: {t.DeletedAt})";
            }
        }


    }
}
