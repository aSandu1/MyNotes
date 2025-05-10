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

        public Task<int> DeleteNoteAsync(Note note)
        {
            return _database.DeleteAsync(note);
        }

        public async Task<int> SoftDeleteNoteAsync(Note note)
        {
            note.IsDeleted = true;
            return await _database.UpdateAsync(note);
        }

        public Task<List<Note>> GetNotesByDateAsync(DateTime date)
            => _database.Table<Note>()
                        .Where(n => n.Data.Date == date.Date)
                        .OrderByDescending(n => n.Data)
                        .ToListAsync();

        public Task<List<Note>> GetFavoriteNotesAsync()
            => _database.Table<Note>()
                        .Where(n => n.IsFavorite)
                        .OrderByDescending(n => n.Data)
                        .ToListAsync();
    }
}
