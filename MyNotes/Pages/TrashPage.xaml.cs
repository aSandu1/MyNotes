using MyNotes.Models;
using MyNotes.Data;

namespace MyNotes.Pages
{
    public partial class TrashPage : ContentPage
    {
        public TrashPage()
        {
            InitializeComponent();
            LoadTrashNotes();
        }

        private async void LoadTrashNotes()
        {
            TrashStack.Children.Clear();

            var trashNotes = await App.Database.GetTrashNoteAsync();

            if (trashNotes.Count == 0)
            {
                TrashStack.Children.Add(new Label { Text = "Coșul este gol.", FontAttributes = FontAttributes.Italic });
                return;
            }

            foreach (var tn in trashNotes)
            {
                var frame = new Frame
                {
                    BorderColor = Colors.Gray,
                    Padding = 10,
                    CornerRadius = 10,
                    Content = new StackLayout
                    {
                        Spacing = 5,
                        Children =
                        {
                            new Label { Text = $"Titlu: {tn.Titlu}", FontAttributes = FontAttributes.Bold },
                            new Label { Text = $"Conținut: {tn.Continut}" },
                            new Label { Text = $"Ștearsă la: {tn.DeletedAt}" },
                            new Button
                            {
                                Text = "Restaurează",
                                BackgroundColor = Colors.Green,
                                TextColor = Colors.White,
                                Command = new Command(async () =>
                                {
                                    await App.Database.RestoreFromTrashAsync(tn);
                                    await DisplayAlert("Restaurat", "Notița a fost restaurată cu succes.", "OK");
                                    LoadTrashNotes(); // Reload
                                })
                            }
                        }
                    }
                };

                TrashStack.Children.Add(frame);
            }
        }
    }
}
