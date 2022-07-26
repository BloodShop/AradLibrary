using Library.Model;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Library.DAL
{
    class LibraryItemEnum : IEnumerator
    {
        public List<LibraryItem> _libraryItem;
        int position = -1;
        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        public LibraryItemEnum(List<LibraryItem> list) => _libraryItem = list;
        public bool MoveNext()
        {
            position++;
            return (position < _libraryItem.Count);
        }
        public void Reset() => position = -1;
        object IEnumerator.Current { get => Current; }
        private LibraryItem Current
        {
            get
            {
                try { return _libraryItem[position]; }
                catch (IndexOutOfRangeException) { throw new InvalidOperationException(); }
            }
        }
    }
}