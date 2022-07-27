using People.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class PeopleRepository : IHumanable<Person>, IEnumerable
    {
        // Singleton DataBase creation
        readonly DataBase _context = DataBase.Instance;

        public void SetSale() => Manager.OnSetSale();
        public void EndSale() => Manager.OnEndSale();
        public Person Add(Person person)
        {
            if (person != null)
            {
                _context.People.Add(person);
                return person;
            }
            return null;
        }
        public Person Delete(Person person)
        {
            var removePerson = _context.People.FirstOrDefault(i => i == person);
            if (removePerson != null)
            {
                _context.People.Remove(removePerson);
                return removePerson;
            }
            return null;
        }
        public IQueryable<Person> Get() => _context.People.AsQueryable();
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < _context.People.Count; i++)
                if (_context.People[i] != null)
                    yield return _context.People[i];
        }
    }
}
