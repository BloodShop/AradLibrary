using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Model
{
    /// <summary>
    /// Manager / Admin user
    /// </summary>
    public class Manager : Person
    {
        public static event Action SetSaleEventHandler;
        public static event Action EndSaleEventHandler;
        public Manager(string name, string password, Address address) : base(name, password, address)
        { }
        public static void OnSetSale() => SetSaleEventHandler.Invoke();
        public static void OnEndSale() => EndSaleEventHandler.Invoke();
    }
}