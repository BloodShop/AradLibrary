using Library.Model;
using System.Collections.Generic;

namespace Library.DAL
{
    /// <summary>
    /// Compare Item By Price IComparer
    /// </summary>
    public class CompareItemByPrice : IComparer<LibraryItem>
    {
        public int Compare(LibraryItem x, LibraryItem y) => x.Price.CompareTo(y.Price);
        public override string ToString() => "Price low - high";
    }
}