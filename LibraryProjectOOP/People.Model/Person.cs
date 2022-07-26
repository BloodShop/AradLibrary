using Library.Model;
using System;
using System.Collections.Generic;

namespace People.Model
{
    public abstract class Person : IFormattable
    {
        string _name;
        string _password;
        Address _address;
        Dictionary<LibraryItem, DateTime> _loanedItems;

        /// <summary>
        /// Get the name of this person
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// Get the password of this person 
        /// </summary>
        public string Password => _password;

        /// <summary>
        /// Get the address of this person
        /// </summary>
        public Address Address
        {
            get { return _address; }
            protected set { _address = value; }
        }

        /// <summary>
        /// Consturctor of new instance sets new: name, password and a new list of items
        /// </summary>
        /// <param name="name">set the person's name</param>
        /// <param name="password">set the person's password</param>
        public Person(string name, string password, Address address)
        {
            _name = name;
            _password = password;
            _address = address;
            _loanedItems = new Dictionary<LibraryItem, DateTime>(); 
        }

        /// <summary>
        /// get all the libraryItems we are loadned by this instance
        /// </summary>
        public Dictionary<LibraryItem, DateTime> LoanedItems => _loanedItems;

        /// <summary>
        /// Add a new library item to this instance
        /// </summary>
        /// <param name="item">The item you want to add to this instance</param>
        public void AddNewItem(LibraryItem item, DateTime exp)
        {
            if (item == null || exp < DateTime.Now)
                throw new ArgumentNullException($"item is null");
            LoanedItems.Add(item,exp);
        }
        public void RemoveItem(LibraryItem item) => LoanedItems?.Remove(item);
        public void SetNewPassword(string newPassword) => _password = newPassword;
        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "n":
                    return _name;
                default:
                    return ToString();
            }
        }
        public override string ToString() => _name + " " + _address;
    }
}