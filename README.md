Documentatie MyNotesApp (Deaconu Emilia-Andreea, Corodescu Serban-Florin, Sandu Alexandru, Lazar Stefania-Daniela)

1. Prezentare generala
  Aplicatia MyNotes dezvoltata cu .NET MAUI si SQLite permite crearea, vizualizarea, editarea, stergerea (soft si hard), marcare favorite, export in CSV si recuperare notite sterse

2. Structura proiect
  MyNotes.App – clasa principala, configureaza baza de date si fereastra initiala
  MyNotesApp (MauiProgram.cs) – configureaza builderul MAUI, fonturi, handleri nativi
  AppShell – defineste meniul Shell si rutele paginilor

  Data/
    NoteDB.cs – wrapper SQLite, operatii CRUD, soft delete, restaurare
    ExcelExportService.cs – exporta o nota intr-un fisier CSV si il deschide
  
  Models/
    Note.cs – modelul de date pentru o nota: Id, Titlu, Continut, Data crearii, Data/Locatie activitate, IsFavorite, IsDeleted
  
  Pages/
    1) MainPage – ecranul de start cu butoane catre toate notitele, favorite si sterse
    2) CreateNotePage – fluxul de creare a unei note si setarea activitatii
    3) NotesListPage – lista notitelor active, cu comenzi edit, delete, favorite si tap pentru detalii
    4) NoteDetailsPage – afisarea unui singur detaliu nota
    5) EditNotePage – editarea unei note existente (titlu, continut, data, ora, locatie)
    6) ActivityInfoPage – setarea datei, orei si locatiei activitatii
    7) FavoritesPage – lista notitelor favorite
    8) DeletedNotesPage – lista notitelor sterse (soft delete), cu optiuni recover si delete definitiv

3. Fluxuri principale

    3.1. Pornire aplicatie
      App initializat: se configureaza ruta bazei SQLite in LocalApplicationData
      AppShell defineste meniul Flyout si rutele
      Se afiseaza MainPage
    
    3.2. Creare notita
      Din MainPage se apasa "Creati o noua notita"
      CreateNotePage instantiata pe stiva de navigatie (PushAsync)
      Utilizatorul introduce titlu, continut
      Apasa "Set Activity Info" deschide ActivityInfoPage pentru data/ora/locatie
      Datele sunt stocate temporar in proprietati statice ale paginii
      Apasa "Save Note" in CreateNotePage
      se creeaza model Note cu campurile completate
      NoteDB.SaveNoteAsync(note) persista in SQLite
      navigare inapoi la MainPage sau NotesListPage
    
    3.3. Vizualizare lista notite
    NotesListPage.OnAppearing apeleaza LoadNotes
    NoteDB.GetNoteAsync() citeste toate notitele unde IsDeleted=false
    sorteaza dupa favorite desc si data desc
    populeaza ObservableCollection<Note>
    UI afiseaza un card per nota cu titlu, stea (favorit), icoana delete, icoana edit
    Apasare pe card pentru navigheaza la NoteDetailsPage
    
    3.4. Detalii nota
      NoteDetailsPage primeste NoteId prin QueryProperty
      OnAppearing incarca Note din DB
      Afiseaza titlu, continut, date activitate, locatie, data crearii
      Buton export CSV (ExcelExportService.ExportNoteAsync)
      Stea favorite, toggle si salvare imediata
    
    3.5. Editare nota
      Din NotesListPage sau NoteDetailsPage se apeleaza Navigation.PushAsync(new EditNotePage{NoteId=...})
      EditNotePage.LoadNoteAsync incarca _currentNote
      Campuri pre-populate: titlu, continut, data, ora, locatie
      UI permite modificari, date si ora se pot edita direct
      
      Apasa Save:
        validare titlu continut
        actualizare model, SaveNoteAsync in DB
        PopAsync() inapoi la NotesListPage
        Cancel: PopAsync() fara salvare
    
    3.6. Stergere nota (soft)
      Apasa icon-ul delete in NotesListPage
      NoteDB.SoftDeleteNoteAsync(note) seteaza IsDeleted=true
      Notes.Remove(note) scoate imediat din lista
      Nota apare in DeletedNotesPage
    
    3.7. Favorite
      Apasa steaua de pe card sau in detalii
      note.IsFavorite = !note.IsFavorite, SaveNoteAsync
      LoadNotes() actualizeaza ordinea listei, favoritele apar primele
      FavoritesPage incarca doar IsFavorite=true
      
    3.8. Recover nota stearsa
      DeletedNotesPage.OnRecover seteaza IsDeleted=false, SaveNoteAsync
      DeletedNotes.Remove(note) scoate din lista sterse
      Nota reapare in NotesListPage
    
    3.9. Delete definitiv
      DeletedNotesPage.OnPermanentDelete face DeleteNoteAsync(note)
      elimina complet din SQLite
      DeletedNotes.Remove(note) scoate din listă
    
    3.10. Export note
      In NoteDetailsPage, OnExportClicked apeleaza ExcelExportService.ExportNoteAsync
      genereaza CSV cu titlu, continut, date
      File.WriteAllText in FileSystem.AppDataDirectory
      Launcher.OpenAsync deschide fisierul in Excel

4. Arhitectura baza de date
     Aplicatia MyNotes ofera un flux complet de management al notitelor cu persistenta SQLite, navigare intuitiva Shell/Navigation, functii avansate: favorite, soft-delete, recover, export
        Clasa NoteDB initiaza SQLiteAsyncConnection
        Tabele: Note
        Metode:
        GetNoteAsync() / GetNoteAsync(id)
        SaveNoteAsync(note) inserare/update
        DeleteNoteAsync(note) stergere fizica
        SoftDeleteNoteAsync(note) update IsDeleted
