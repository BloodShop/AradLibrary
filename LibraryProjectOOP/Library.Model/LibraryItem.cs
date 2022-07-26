using System;
using System.Collections.Generic;

namespace Library.Model
{
    /// <summary>
    /// abstract class representing an item handled by the library
    /// </summary>
    public abstract class LibraryItem : IFormattable
    {
        protected double _price;
        double _sale;

        /// <summary>
        /// unique identifier of this item
        /// </summary>
        public Guid Id { get; protected set; } = Guid.NewGuid();

        /// <summary>
        /// title of name of item
        /// </summary>
        public string Title { get; protected set; }

            /// <summary>
            /// Publication date of item
            /// </summary>
        public DateTime PublicationDate { get; protected set; }

        /// <summary>
        /// price of item
        /// </summary>
        public double Price { get { return _price - (_price * Sale); } }

        /// <summary>
        /// item's sale
        /// </summary>
        public double Sale
        {
            get => _sale;
            protected set
            {
                if (value >= 0 && value <= 50) _sale = value / 100;
                else _sale = 0;
            }
        }

        /// <summary>
        /// Get the item's reviews
        /// </summary>
        public List<Review> Reviews { get; protected set; } = new List<Review>();

        /// <summary>
        /// Get number of pages
        /// </summary>
        public int NumOfPages { get; private set; }

        /// <summary>
        /// ImageSource Uri of the item's Cover / Binding
        /// </summary>
        public Uri ImageSource { get; set; } = new Uri("about:blank");

        /// <summary>
        /// create a instance of library item
        /// </summary>
        /// <param name="title">the name or title of the new item</param>
        /// <param name="publishDate">the publication date of the new item</param>
        /// <param name="price">item's price</param>
        /// <param name="numOfPages">item's number of pages</param>
        /// <param name="sale">item's sale precentages</param>
        protected LibraryItem(string title, DateTime publishDate, double price, int numOfPages, double sale = 0)
        {
            Title = title;
            PublicationDate = publishDate;
            _price = price;
            Sale = sale;
            NumOfPages = numOfPages;
        }

        /// <summary>
        /// Add a review to this item
        /// </summary>
        /// <param name="name">user's name</param>
        /// <param name="review">the comment you want to add</param>
        /// <param name="stars">amount of stars you rate the item</param>
        public void AddReview(Review review) => Reviews.Add(review);
        public abstract string ToString(string format, IFormatProvider formatProvider);

        // Added actions to the manager
        public abstract void OnSetSale();
        public abstract void OnEndSale();
    }

}