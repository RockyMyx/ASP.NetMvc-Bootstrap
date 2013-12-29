using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using System.Data.Objects;

namespace MvcBootstrap.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DBEntity db = new DBEntity())
            {
                IEnumerable<Module> modules = db.GetModuleTree();
                foreach (Module module in modules)
                {
                    Console.WriteLine(module.Name);
                }
            }

            Console.ReadLine();
        }
    }
}
