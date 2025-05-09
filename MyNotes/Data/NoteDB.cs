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
            _database.CreateTableAsync<TrashNote>().Wait(); // Creăm tabela TrashNote
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

        public Task<int> SaveNoteAsync(Note note)
        {
            if (note.Id != 0)
                return _database.UpdateAsync(note);
            else
                return _database.InsertAsync(note);
        }

        public Task<int> DeleteNoteAsync(Note note)
        {
            return _database.DeleteAsync(note);
        }

       
        public Task<List<TrashNote>> GetTrashNoteAsync()
        {
            return _database.Table<TrashNote>().ToListAsync();
        }

        public Task<TrashNote> GetTrashNoteAsync(int id)
        {
            return _database.Table<TrashNote>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveTrashNoteAsync(TrashNote trashNote)
        {
            return _database.InsertAsync(trashNote);
        }

        public Task<int> DeleteTrashNoteAsync(TrashNote trashNote)
        {
            return _database.DeleteAsync(trashNote);
        }

        public async Task MoveToTrashAsync(Note note)
        {
            var trashNote = new TrashNote
            {
                Titlu = note.Titlu,
                Continut = note.Continut,
                Data = note.Data,
                DeletedAt = DateTime.Now 
            };

            await SaveTrashNoteAsync(trashNote);

            await DeleteNoteAsync(note);
        }

        // Funcție pentru a restaura o notiță din coșul de gunoi
        public async Task RestoreFromTrashAsync(TrashNote trashNote)
        {
            var note = new Note
            {
                Id = trashNote.Id,
                Titlu = trashNote.Titlu,
                Continut = trashNote.Continut,
                Data = trashNote.Data
            };

            // Salvăm înapoi în Notes
            await SaveNoteAsync(note);

            // Ștergem din TrashNote
            await DeleteTrashNoteAsync(trashNote);
        }
    }
}
