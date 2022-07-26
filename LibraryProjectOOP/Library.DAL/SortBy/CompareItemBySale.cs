using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL.SortBy
{
    public class CompareItemBySale : IComparer<LibraryItem>
    {
        public int Compare(LibraryItem x, LibraryItem y) => x.Sale.CompareTo(y.Sale);
        public override string ToString() => "Sale%";
    }
}
