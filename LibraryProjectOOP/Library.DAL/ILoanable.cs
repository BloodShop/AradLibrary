using People.Model;
using System;
using System.Collections.Generic;

namespace Library.DAL
{
    public interface ILoanable<T> where T : class
    {
        Dictionary<T, Person> GetLoaned();
        T AddLoan(T item, Person owner, DateTime itemReturnDate);
        T RetrieveItem(T item);
        T RetrieveItem(int index);
    }
}
