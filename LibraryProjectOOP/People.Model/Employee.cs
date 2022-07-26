using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Model
{
    public class Employee : Person
    {
        public Employee(string name, string password, Address address) : base(name, password, address)
        { }
    }
}
