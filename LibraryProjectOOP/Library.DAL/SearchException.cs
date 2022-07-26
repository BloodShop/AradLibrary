using System;

namespace Library.DAL
{
    [Serializable]
    public class SearchException : Exception
    {
        public SearchException(string message) : base(message) { }
    }
}