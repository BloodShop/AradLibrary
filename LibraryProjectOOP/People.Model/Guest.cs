using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Model
{
    public class Guest : Person
    {
        /// <summary>
        /// Guest user
        /// </summary>
        public Guest() : base("Guest", "", new Address())
        { }

        /// <summary>
        /// Set new address to this guest instance
        /// </summary>
        /// <param name="city"></param>
        /// <param name="street"></param>
        /// <param name="houseNumber"></param>
        /// <param name="houseEntry"></param>
        /// <param name="zip"></param>
        public void SetAddress(string city, string street, int houseNumber, char houseEntry = 'a', int zip = 0)
            => this.Address = new Address(city, street, houseNumber, houseEntry, zip);
    }
}