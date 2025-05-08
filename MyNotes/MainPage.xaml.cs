using MyNotes.Models;
using MyNotes.Data;

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

            await App.Database.SaveNoteAsync(new Note
            {
                Titlu = "Din MainPage",
                Continut = "Notiță de test",
                Data = DateTime.Now
            });

            var notite = await App.Database.GetNoteAsync();
            DebugText.Text += $"\nGăsite: {notite.Count}";

            foreach (var n in notite)
            {
                DebugText.Text += $"\n{n.Titlu} - {n.Continut}";
            }
        }


    }

}
