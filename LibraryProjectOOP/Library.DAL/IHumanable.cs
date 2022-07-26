using System.Linq;

namespace Library.DAL
{
    public interface IHumanable<T> where T : class
    {
        IQueryable<T> GetPeople();
        void AddPerson(T person);
        T RemovePerson(T person);
        void EndSale();
        void SetSale();
    }
}