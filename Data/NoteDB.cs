using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using MyNotes.Models;

namespace MyNotes.Data
{
    public class NoteDB
    {
        readonly SQLiteAsyncConnection _database;

        public NoteDB(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Note>().Wait();
        }

        public Task<List<Note>> GetNoteAsync()
        {
            return _database.Table<Note>().ToListAsync();
        }

        public Task<Note> GetNoteAsync(int id)
        {
            return _database.Table<Note>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Note Note)
        {
            if (Note.Id != 0)
                return _database.UpdateAsync(Note);
            else
                return _database.InsertAsync(Note);
        }

        public Task<int> DeleteNoteAsync(Note Note)
        {
            return _database.DeleteAsync(Note);
        }
    }
}
