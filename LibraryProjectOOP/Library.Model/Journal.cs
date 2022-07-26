using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    /// <summary>
    /// class representing journal handled by the library
    /// </summary>
    public class Journal : LibraryItem
    {
        /// <summary>
        /// Journal's all genres exict
        /// </summary>
        public static List<string> JournalGenres = new List<string>();

        /// <summary>
        /// The journal's contributers
        /// </summary>
        public List<string> Contributors { get; private set; }

        /// <summary>
        /// The journal's editors
        /// </summary>
        public List<string> Editors { get; private set; }

        /// <summary>
        /// The journal's genres
        /// </summary>
        public List<string> Genres { get; private set; }

        /// <summary>
        /// The journal's Frequency
        /// </summary>
        public JournalFrequency Frequency { get; set; }

        public Journal(string title, DateTime publishDate, double price, int numOfPages, int sale = 0)
            :base(title,publishDate,price,numOfPages,sale)
        {
            Contributors = new List<string>();
            Editors = new List<string>();
            Genres = new List<string>();
        }

        public override string ToString() => $"Journal: {Title}, {PublicationDate:d}";
        public override string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "s": return $"J: {Title} ({Frequency})";
                case "$": return $"J: {Title} - ({Frequency}) - {Price:c} - Sale {Sale}%";
                case "$$": return $"{Price:c}";
                case "S": return $"Journal: {Title}, Published: {PublicationDate}, Frequency: {Frequency}, Category: {string.Join(", ", Genres)}";
                case "NoSale": return $"{_price:c}";
                default:
                    return this.ToString();
            }
        }
        public override void OnSetSale() => Sale = 20;
        public override void OnEndSale() => Sale = 0;
    }

    public enum JournalFrequency
    {
        Daily = 1,
        Weekly,
        BiWeekly,
        Monthly,
        BiMonthly,
        Qurterly,
        SemiAnnualy,
        Annualy,
        BiAnnualy,
        Other = 0
    }
}