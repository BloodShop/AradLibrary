using Library.DAL;
using Library.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestOOPLibrary
{
    [TestClass]
    public class ISBNUnitTest
    {
        readonly LibraryRepository repo = new LibraryRepository();
        readonly ISBN isbn = new ISBN();

        #region ISBN UnitTest
        [TestMethod] public void AddExictingPublisherToISBN_Invalid() => Assert.ThrowsException<ArgumentException>(() => ISBN.Publishers.Add("Anonymus", 666));
        [TestMethod] public void AddExictingCountryToISBN_Invalid() => Assert.ThrowsException<ArgumentException>(() => ISBN.Countries.Add("Israel", 965));
        [TestMethod] public void SetNoneExictingCountry() => Assert.ThrowsException<IsbnException>(() => isbn.Country = "AEngland");
        [TestMethod] public void SetExictingCountry() => Assert.IsTrue(AddExictingCountry());
        bool AddExictingCountry()
        {
            if (ISBN.Countries.ContainsKey("French"))
            {
                isbn.Country = "French";
                return true;
            }
            return false;
        }

        [TestMethod] public void SetExictingPublisher() => Assert.IsTrue(AddExictingPublisher());
        bool AddExictingPublisher()
        {
            if (ISBN.Publishers.ContainsKey("Harper"))
            {
                isbn.Publisher = "Harper";
                return true;
            }
            return false;
        }

        [TestMethod] public void SetNoneExictingPublisher() => Assert.ThrowsException<IsbnException>(() => isbn.Publisher = "AHarper");
        #endregion
    }
}
