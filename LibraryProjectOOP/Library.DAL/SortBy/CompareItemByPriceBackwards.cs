using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.SortBy
{
    internal class CompareItemByPriceBackwards : IComparer<LibraryItem>
    {
        public int Compare(LibraryItem x, LibraryItem y) => y.Price.CompareTo(x.Price);

        public override string ToString()
        {
            return "Price high - low";
        }
    }
}
