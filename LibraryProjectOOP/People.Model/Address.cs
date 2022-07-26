namespace People.Model
{
    public struct Address
    {
        /// <summary>
        /// Gets the city
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        /// Gets the street
        /// </summary>
        public string Street { get; private set; }

        /// <summary>
        /// Gets the house Number
        /// </summary>
        public int HouseNumber { get; private set; }

        /// <summary>
        /// Gets the House entry
        /// </summary>
        public char HouseEntry { get; private set; }

        /// <summary>
        /// Gets the postalCode / zip
        /// </summary>
        public int PostalCode { get; private set; }

        /// <summary>
        /// Ctor to build an address
        /// </summary>
        /// <param name="city"></param>
        /// <param name="street"></param>
        /// <param name="houseNumber"></param>
        /// <param name="houseEntry"></param>
        /// <param name="zip"></param>
        public Address(string city, string street, int houseNumber, char houseEntry = 'a', int zip = 0) 
        {
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            HouseEntry = houseEntry;
            PostalCode = zip;
        }
        public override string ToString() => $"[{City} , st. {Street} {HouseNumber} / {HouseEntry} , Zip: {PostalCode:D5}]";
    }
}