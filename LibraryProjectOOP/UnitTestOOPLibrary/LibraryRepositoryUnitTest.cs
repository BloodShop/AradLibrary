using System;
using System.Collections.Generic;
using System.Linq;
using Library.DAL;
using Library.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using People.Model;

namespace UnitTestOOPLibrary
{
    [TestClass]
    public class LibraryRepositoryUnitTest
    {
        readonly LibraryRepository repo = new LibraryRepository();
        readonly List<LibraryItem> list = new List<LibraryItem>() { new Book("abc", DateTime.Now, 2, 2) };
        readonly ISBN isbn = new ISBN();

        [TestMethod] public void SingleToneLegit()
        {
            DataBase a = DataBase.Instance;
            DataBase b = DataBase.Instance;
            Assert.AreEqual(a, b);
        }
        //// DataBase SingleTone instance creation check

        #region LibraryRepository Test
        [TestMethod] public void SwapLibraryItemByNullValid()
        {
            LibraryItem a = new Book("a", DateTime.Now, 1, 1);
            LibraryItem b = null;
            Assert.AreEqual(a, a);
            Assert.IsNull(b);
            LibraryRepository.Swap(ref a, b);
            Assert.IsFalse(a == b);
        }
        [TestMethod] public void SwapLibraryItemRepoBy()
        {
            LibraryItem a = new Book("a", DateTime.Now, 1, 1);
            LibraryItem b = new Book("b", DateTime.Now, 1, 1);
            Assert.AreNotEqual(a, b);
            LibraryRepository.Swap(ref a, b);
            Assert.IsTrue(a == b);
        }
        [TestMethod] public void FindByTypeNullRetriveAll() => Assert.IsTrue(repo.FindAllByType(null).TrueForAll(x => x is LibraryItem));
        [TestMethod] public void ValidUnKnownGetAllByType() => Assert.IsTrue(repo.FindAllByType(typeof(Employee)).TrueForAll(x => x is Book || x is Journal)); // In case we have journals and books
        [TestMethod] public void ValidLibraryItemGetAllByType() => Assert.IsTrue(repo.FindAllByType(typeof(LibraryItem)).TrueForAll(x => x is LibraryItem));
        [TestMethod] public void ValidJounalGetAllByType() => Assert.IsTrue(repo.FindAllByType(typeof(Journal)).TrueForAll(x => x is Journal));
        [TestMethod] public void ValidBookGetAllByType() => Assert.IsTrue(repo.FindAllByType(typeof(Book)).TrueForAll(x => x is Book));
        [TestMethod] public void RetriveNullItemExceptionCheck()
        {
            var book1 = new Book("123", DateTime.UtcNow, 12, 12);
            Journal @null = null;

            Assert.ThrowsException<SearchException>(() => repo.RetrieveItem(book1));
            Assert.ThrowsException<SearchException>(() => repo.RetrieveItem(@null));
        }
        [TestMethod] public void AddNullItemExceptionCatch() => Assert.ThrowsException<NullReferenceException>(() => repo.Add((LibraryItem)null));
        [TestMethod] public void VaildInputRetrieveItem()
        {
            var item = repo.GetAvailable().ToList()[0];
            var c = new Customer("Alon", "123", new Address());
            repo.AddLoan(item, c, DateTime.Now.AddDays(2));
            Assert.IsNotNull(repo.RetrieveItem(item));
        }
        [TestMethod] public void IndexRetrieveItemFromLoan()
        {
            var c = new Customer("Alon", "123", new Address());
            var b1 = repo.GetAvailable().ToList()[0];
            var b2 = repo.GetAvailable().ToList()[1];
            repo.AddLoan(b1, c, DateTime.Now.AddDays(1));
            repo.AddLoan(b2, c, DateTime.Now.AddDays(1));
            Assert.ThrowsException<IndexOutOfRangeException>(() => repo.RetrieveItem(10));
        }
        [TestMethod] public void IsNotNullRetrieved()
        {
            var c = new Customer("Alon", "123", new Address());
            var b1 = repo.GetAvailable().ToList()[0];
            var b2 = repo.GetAvailable().ToList()[1];
            repo.AddLoan(b1, c, DateTime.Now.AddDays(1));
            repo.AddLoan(b2, c, DateTime.Now.AddDays(1));
            Assert.IsNotNull(repo.RetrieveItem(1));
        }
        [TestMethod] public void ExceptionCatchFindByTitle() => Assert.ThrowsException<SearchException>(() => repo[list, "aaaa"]);
        [TestMethod] public void ValidFindByTitle()
        {
            List<LibraryItem> temp = repo[list, "a"];
            Assert.IsTrue(temp.Count > 0);
        }
        [TestMethod] public void InValidGUIDSearch() => Assert.ThrowsException<SearchException>(() => repo[Guid.Parse($"{{d0d24253-3a93-4e48-83bf-c55c339c3df5}}")]);
        [TestMethod] public void InvalidISBN()
        {
            ISBN isbn = new ISBN()
            {
                Publisher = ISBN.Publishers.FirstOrDefault().Key,
                Country = ISBN.Countries.FirstOrDefault().Key,
                SerialNumber = 999,
            };
            Assert.ThrowsException<SearchException>(() => repo[isbn]);
        }
        [TestMethod] public void InvalidISBNAurgument()
        {
            
            Assert.ThrowsException<IsbnException>(() => {
                ISBN isbn = new ISBN()
                {
                    Publisher = "Bob",
                    Country = "Shelly",
                    SerialNumber = 999,
                };
            });
        }
        [TestMethod] public void ValidISBN()
        {
            Book b = repo.GetAvailable().ToList().Find(x => x is Book) as Book;
            ISBN isbn = b.ISBN;
            Assert.IsNotNull(repo[isbn]);
            Assert.IsTrue(b == repo[isbn]);
        }
        [TestMethod] public void SwapLibraryItemWorks()
        {
            LibraryItem a = new Book("cba", DateTime.Now, 12, 100);
            LibraryItem b = new Book("abc", DateTime.Now, 24, 250);
            LibraryRepository.Swap(ref a, b);
            Assert.IsTrue(a.Equals(b));
        }
        [TestMethod] public void DeleteNull() => Assert.IsNull(repo.Delete()); // Try to delete Null > get back null
        [TestMethod] public void DeleteByGUIDNotExict() => Assert.IsNull(repo.Delete(id: Guid.Parse($"{{d0d24253-3a93-4e48-83bf-c55c339c3df5}}"))); // Try Delete an item bt guid that not exicts > get back null
        [TestMethod] public void DeleteByLibraryItemNotExict() => Assert.IsNull(repo.Delete(list[0])); // Try Delete an item that not exicts > get back null
        #endregion
    }
    
    [TestClass]
    public class UI_UnitTest
    {

    }
}
