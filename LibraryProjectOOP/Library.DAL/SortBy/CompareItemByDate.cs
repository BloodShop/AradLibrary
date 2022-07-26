using Library.Model;
using System.Collections.Generic;

namespace Library.DAL.SortBy
{
    public class CompareItemByDate : IComparer<LibraryItem>
    {
        public int Compare(LibraryItem x, LibraryItem y) => x.PublicationDate.CompareTo(y.PublicationDate);
        public override string ToString() => "Date Old - New";
    }
}
