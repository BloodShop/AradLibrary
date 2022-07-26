using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public struct Review
    {
        /// <summary>
        /// Name of the reviewer
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Rating the the user set to an item
        /// </summary>
        public int Rate { get; private set; }

        /// <summary>
        /// The comment on the Item
        /// </summary>
        public string Comment { get; private set; }

        /// <summary>
        /// When the review was published
        /// </summary>
        public DateTime PublishDate { get; private set; }

        public Review(string name, int rate, DateTime dateTime, string comment)
        {
            Name = name;
            Rate = rate;
            Comment = comment;
            PublishDate = dateTime;
        }
        public override string ToString() => $"{Name} : {Rate} stars [{PublishDate:d}]\n{Comment}";
    }
}