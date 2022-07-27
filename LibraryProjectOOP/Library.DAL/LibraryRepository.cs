using Library.Model;
using People.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Library.DAL
{
    public class LibraryRepository : IRepository<LibraryItem>, ILoanable<LibraryItem>, IEnumerable
    {
        // Singleton DataBase creation
        readonly DataBase _context = DataBase.Instance;

        // Indexers for find specific items by given criterias
        /// <summary>
        /// Indexer for getting list by the given item's title
        /// </summary>
        /// <param name="currentList">Given list to work on</param>
        /// <param name="title">The item's title you are searching for</param>
        /// <returns>List items with given title/></returns>
        /// <exception cref="SearchException"></exception>
        public List<LibraryItem> this[List<LibraryItem> currentList, string title]
        {
            get
            {
                List<LibraryItem> tempList = currentList.FindAll(i => i.Title.ToLower().Contains(title.ToLower()));
                if (tempList.Count == 0)
                    throw new SearchException($"Library doesn't contain:\n\'{title}\'");
                return tempList;
            }
        }

        /// <summary>
        /// Search an item by its GUID
        /// </summary>
        /// <param name="id">The GUID of an item <see cref="Guid"/></param>
        /// <returns>Returns an item with the same Guid <see cref="Guid"/></returns>
        /// <exception cref="SearchException"></exception>
        public LibraryItem this[Guid id]
        {
            get
            {
                LibraryItem li = _context.LibraryItems.Find(item => item.Id == id);
                if (li is null)
                    throw new SearchException("No item found by GUID");
                return li;
            }
        }

        /// <summary>
        /// Search an item by its ISBN
        /// </summary>
        /// <param name="isbn">Builded ISBN by the books settings <see cref="ISBN"/></param>
        /// <returns>Return the book with the same ISBN <see cref="ISBN"/> or throws an SearchException if not found <see cref="SearchException"/>/></returns>
        /// <exception cref="SearchException"></exception>
        public Book this[ISBN isbn]
        {
            get
            {
                List<LibraryItem> tempBooks = _context.LibraryItems.FindAll((i) => i is Book);
                foreach (Book book in tempBooks)
                    if (book.ISBN.ToString() == isbn.ToString())
                        return book;
                throw new SearchException($"No book found by this isbn - {isbn}");
            }
        }

        /// <summary>
        /// Search all items by their unique Type (Journal / Book etc..)
        /// </summary>
        /// <param name="type">Type you are searching for</param>
        /// <returns>Retrun a list of all the items that are by the given type</returns>
        public static void Swap(ref LibraryItem old, LibraryItem @new) // static function to swap between items 
        {
            if (@new != null)
            {
                @new.Reviews.AddRange(old.Reviews); // save reviews
                old = @new;
                Manager.EndSaleEventHandler += old.OnEndSale;
                Manager.SetSaleEventHandler += old.OnSetSale;
            }
        }
        public List<LibraryItem> FindAllByType(Type type)
        {
            // Switch cannot use typeof parameter
            if (type == null || type.Equals(typeof(LibraryItem)))
                return _context.LibraryItems;
            else if (type.Equals(typeof(Book)))
                return _context.LibraryItems.FindAll((i) => i is Book);
            else if (type.Equals(typeof(Journal)))
                return _context.LibraryItems.FindAll((i) => i is Journal);
            return _context.LibraryItems;
        }

        #region IRepository
        public LibraryItem Get(Guid id) => _context.LibraryItems.FirstOrDefault(i => i.Id == id);
        public LibraryItem Add(LibraryItem item)
        {
            if (item == null)
                throw new NullReferenceException("Null input isn't valid");

            Manager.SetSaleEventHandler += item.OnSetSale;
            Manager.EndSaleEventHandler += item.OnEndSale;
            _context.LibraryItems.Add(item);
            return item;
        }
        public LibraryItem Delete(LibraryItem libraryItem = null, Guid id = default(Guid))
        {
            LibraryItem item = null;
            if (id != default(Guid))
            {
                item = _context.LibraryItems.FirstOrDefault(i => i.Id == id);
                if (item != null)
                    _context.LibraryItems.Remove(item);
            }
            else if (libraryItem != null)
            {
                item = _context.LibraryItems.FirstOrDefault(i => i == libraryItem);
                if (item != null) _context.LibraryItems.Remove(libraryItem);
            }
            return item;
        }
        public LibraryItem Update(LibraryItem old, LibraryItem @new)
        {
            var temp = old;
            if (@new != null && old != null)
            {
                _context.LibraryItems.Remove(old);
                @new.Reviews.AddRange(temp.Reviews); // save reviews
                Manager.EndSaleEventHandler += @new.OnEndSale;
                Manager.SetSaleEventHandler += @new.OnSetSale;
                _context.LibraryItems.Add(@new);
            }
            return @new;
        }
        public IQueryable<LibraryItem> GetAvailable() => _context.LibraryItems.AsQueryable();
        #endregion

        #region ILoanable
        public Dictionary<LibraryItem, Person> GetLoaned() => _context.LoanedLibraryItems;
        public LibraryItem RetrieveItem(int index)
        {
            if (index > _context.LoanedLibraryItems.Count || index < 0)
                throw new IndexOutOfRangeException();

            LibraryItem temp = Add(_context.LoanedLibraryItems.ElementAt(index).Key);
            Person p = _context.LoanedLibraryItems.ElementAt(index).Value;
            p.RemoveItem(temp);
            _context.LoanedLibraryItems.Remove(temp);
            return temp;
        } // Retrieve the Item by Index input
        public LibraryItem RetrieveItem(LibraryItem item)
        {
            if (item == null || !_context.LoanedLibraryItems.ContainsKey(item))
                throw new SearchException($"No loadned item named: {item}");
            LibraryItem temp = this.Add(_context.LoanedLibraryItems.FirstOrDefault(x => x.Key == item).Key);
            _context.LoanedLibraryItems.Remove(temp);
            return temp;
        } // Retrieve the Item by Item input
        public LibraryItem RetrieveItem(Person person)
        {
            var element = _context.LoanedLibraryItems.FirstOrDefault(x => x.Value == person).Key;
            if (element != null)
            {
                _context.LoanedLibraryItems.Remove(element);
                return element;
            }
            return null;
        } // Retrieve the Item by Person input
        public LibraryItem AddLoan(LibraryItem item, Person owner, DateTime itemReturnDate)
        {
            if (itemReturnDate < DateTime.Now) itemReturnDate = DateTime.Now.AddDays(1);
            _context.LoanedLibraryItems.Add(item, owner);
            owner.AddNewItem(item, itemReturnDate);
            return Delete(item);
        }
        #endregion

        public IEnumerator GetEnumerator() // IEnumerator
        {
            //return new LibraryItemEnum(_context.LibraryItems);

            // instead of writing CustomerEnum
            for (int i = 0; i < _context.LibraryItems.Count; i++)
                if (_context.LibraryItems[i] != null)
                    yield return _context.LibraryItems[i];
        }
    }
}