using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    /// <summary>
    /// class representing book handled by the library
    /// </summary>
    public class Book : LibraryItem
    {
        private const string _defaultCountry = "Israel";

        /// <summary>
        /// list of known genres for books
        /// </summary>
        public static List<string> BookGenres = new List<string>();

        /// <summary>
        /// list of known authers
        /// </summary>
        public static List<string> KnownAuthors = new List<string>();

        /// <summary>
        /// get international standard book number <see cref="Library.Model.ISBN"/>
        /// </summary>
        public ISBN ISBN { get; private set; }

        /// <summary>
        /// get collection of book's authors
        /// </summary>
        public List<string> Authors { get; private set; }

        /// <summary>
        /// Get or set book's publisher. Must be a known publisher by the <see cref="Library.Model.ISBN"/>
        /// </summary>
        /// <exception cref="IsbnException">thrown when attempting to set publisher value that is not recognized by ISBN</exception>
        public string Publisher 
        {
            get { return this.ISBN.Publisher; }
            set { this.ISBN.Publisher = value; }
        }


        /// <summary>
        /// get collection of book's genres
        /// </summary>
        public List<string> Genres { get; private set; }

        /// <summary>
        /// get or set book's revision
        /// </summary>
        public int Revision { get; set; }

        /// <summary>
        /// get or set book's synopsis (short summary of book)
        /// </summary>
        public string Synopsis { get; set; }

        /// <summary>
        /// create an instance of book
        /// </summary>
        /// <param name="title">the title of the new book</param>
        /// <param name="publishDate">the date the new book was published</param>
        /// <param name="serialNumber">optional parameter, the book's serial number for the <see cref="Model.ISBN"/>. 
        /// Every title from the same publisher should have a unique serial number</param>
        /// <param name="country">optional parameter, the country form the <see cref="Model.ISBN"/>.
        /// Default value is "Israel"</param>
        public Book(string title, DateTime publishDate, double price, int numOfPages, int serialNumber = 0, string country = _defaultCountry, int sale = 0)
            :base(title, publishDate, price, numOfPages,sale)
        {
            this.ISBN = new ISBN() { SerialNumber = serialNumber, Country = country };
            Authors = new List<string>();
            Genres = new List<string>();
        }

        public override string ToString() => $"Book: {Title}, {PublicationDate:d}";
        public override string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "s": return $"B: {Title} {Revision}";
                case "$": return $"B: {Title} - {Revision} - {Price:c} - Sale {Sale}%";
                case "$$": return $"{Price:c}";
                case "S": return $"Book: {Title}, Published: {PublicationDate}, Revision: {Revision}, Category: {string.Join(", ", Genres)}";
                case "NoSale": return $"{_price:c}";
                default:
                    return this.ToString();
            }
        }
        public override void OnSetSale() => Sale = 15;
        public override void OnEndSale() => Sale = 0;
    }
}