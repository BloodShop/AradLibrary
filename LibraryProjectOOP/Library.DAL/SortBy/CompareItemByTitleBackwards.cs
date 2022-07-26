using Library.Model;
using System.Collections.Generic;

namespace Library.DAL.SortBy
{
    internal class CompareItemByTitleBackwards : IComparer<LibraryItem>
    {
        public int Compare(LibraryItem x, LibraryItem y) => y.Title.CompareTo(x.Title);
        public override string ToString() => "Z - A";
    }
}
