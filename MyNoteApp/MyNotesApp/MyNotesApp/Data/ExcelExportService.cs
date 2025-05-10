using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MyNotes.Models;
using Microsoft.Maui.Storage;
using Microsoft.Maui.ApplicationModel;

namespace MyNotes.Data
{
    public static class ExcelExportService
    {
        public static async Task ExportNoteAsync(Note note)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Titlu,Continut,Data Crearii,Data,Locatie");
            var created = note.Data.ToString("s");
            var actDate = note.ActivitateData.HasValue
                ? note.ActivitateData.Value.ToString("s")
                : "";
            var loc = note.ActivitateLocatie?.Replace(",", " ") ?? "";

            sb.AppendLine($"\"{note.Titlu}\",\"{note.Continut}\",{created},{actDate},\"{loc}\"");

            var fileName = $"note_{note.Id}.csv";
            var path = Path.Combine(FileSystem.AppDataDirectory, fileName);
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

            await Launcher.OpenAsync(new OpenFileRequest
            {
                Title = "Export Note",
                File = new ReadOnlyFile(path)
            });
        }
    }
}