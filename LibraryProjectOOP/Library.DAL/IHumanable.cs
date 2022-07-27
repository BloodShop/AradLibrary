using System.Linq;

namespace Library.DAL
{
    public interface IHumanable<T> where T : class
    {
        IQueryable<T> Get();
        T Add(T person);
        T Delete(T person);
        void EndSale();
        void SetSale();
    }
}