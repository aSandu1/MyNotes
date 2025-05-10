using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNotes.Data;
using SQLite;

namespace MyNotes.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Titlu { get; set; }

        public string Continut { get; set; }

        public DateTime Data { get; set; }

        public bool IsFavorite { get; set; }
        public bool IsDeleted { get; set; } = false;

        public DateTime? ActivitateData { get; set; }
        public string ActivitateLocatie { get; set; }

        public static implicit operator Note(NoteDB v)
        {
            throw new NotImplementedException();
        }
    }
}
