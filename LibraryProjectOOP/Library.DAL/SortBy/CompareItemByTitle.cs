using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class CompareItemByTitle : IComparer<LibraryItem>
    {
        public int Compare(LibraryItem x, LibraryItem y) => x.Title.CompareTo(y.Title);

        public override string ToString()
        {
            return "A - Z";
        }

    }
}
