using Library.DAL;
using Library.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using People.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestOOPLibrary
{
    [TestClass]
    public class PeopleUnitTest
    {
        readonly LibraryRepository repo = new LibraryRepository();
        readonly List<LibraryItem> items = new List<LibraryItem>() { new Book("abc", DateTime.Now, 50, 2), new Journal("abc", DateTime.Now, 25, 2) };
        readonly Manager m = new Manager("Admin", "123", new Address());
        readonly Customer c = new Customer("Alon", "123", new Address());
        readonly Guest g = new Guest();

        [TestMethod]
        public void AddItemNullInvalidOperation() => Assert.ThrowsException<ArgumentNullException>(() => m.AddNewItem(null, DateTime.Now.AddDays(1)));
    }
}
