using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notebook.Entities
{
    class Notebook
    {
        private Dictionary<int, Note> _notes;

        public Notebook()
        {
            _notes = new Dictionary<int, Note>();
        }

        public IReadOnlyCollection<Note> Notes => _notes.Values;

        public Note GetNoteById(int id) => _notes[id];

        public void AddNote(Note note)
        {
            if (note == null)
                throw new ArgumentNullException(nameof(note));

            if (!_notes.TryAdd(note.Id, note))
                throw new ArgumentException($"note with {note.Id} already added");
        }

        public void DeleteNote(int id)
        {
            if (!_notes.Remove(id))
                throw new KeyNotFoundException($"note with id {id} not found");
        }
    }
}
