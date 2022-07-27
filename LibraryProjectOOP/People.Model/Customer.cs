using Library.Model;
using System;
using System.Collections.Generic;

namespace People.Model
{
    public class Customer : Person
    {
        /// <summary>
        /// MaxItem the customer can hold loaned items
        /// </summary>
        public const int MAX_ITEMS = 5;

        public Customer(string name, string password, Address address) : base(name, password, address)
        { }
    }
}