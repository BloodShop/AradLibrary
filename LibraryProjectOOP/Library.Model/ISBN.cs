using System.Collections.Generic;

namespace Library.Model
{
    public class ISBN
    {
        private const int _prefix = 978;

        /// <summary>
        /// Dicticonary of all countries
        /// </summary>
        public static Dictionary<string, int> Countries { get; set; }

        /// <summary>
        /// Dictionary of all publishers
        /// </summary>
        public static Dictionary<string, int> Publishers { get; set; }

        private string _country;
        private string _publisher;

        static ISBN()
        {
            Countries = new Dictionary<string, int>();
            Publishers = new Dictionary<string, int>();
        }

        /// <summary>
        /// Get the cpnstant prefix
        /// </summary>
        public static int Prefix { get => _prefix; }

        /// <summary>
        /// Get / Set country prop
        /// </summary>
        public string Country
        {
            get => _country;
            set
            {
                if (!Countries.ContainsKey(value))
                    throw new IsbnException($"Country '{value}' is not recognized by ISBN");
                _country = value;
            }
        }

        /// <summary>
        /// Get / Set publisher prop
        /// </summary>
        public string Publisher
        {
            get => _publisher;
            set
            {
                if (!Publishers.ContainsKey(value))
                    throw new IsbnException($"Publisher '{value}' is not recognized by ISBN");
                _publisher = value;
            }
        }

        /// <summary>
        /// Get / Set serial Number prop
        /// </summary>
        public int SerialNumber { get; set; }

        /// <summary>
        /// Get control number
        /// </summary>
        public int Control { get => (Countries[Country] + Publishers[Publisher] + SerialNumber) % 10; }
        public static bool operator /(ISBN iSBN1, ISBN iSBN2)
        {
            if (iSBN1.ToString().Equals(iSBN2.ToString())) 
                return true;
            throw new IsbnException("ISBN is incorrect");
        }
        public override string ToString() => $"{_prefix}-{Countries[Country]:D3}-{Publishers[Publisher]:D3}-{SerialNumber:D3}-{Control}";
    }
}