using System;
using SQLite;

namespace MyNotes.Models
{
    public class TrashNote
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Titlu { get; set; }

        public string Continut { get; set; }

        public DateTime Data { get; set; }

        public DateTime DeletedAt { get; set; } // Data în care a fost mutată în coșul de gunoi

        public static implicit operator TrashNote(Note note)
        {
            return new TrashNote
            {
                Id = note.Id,
                Titlu = note.Titlu,
                Continut = note.Continut,
                Data = note.Data,
                DeletedAt = DateTime.Now
            };
        }
    }
}
