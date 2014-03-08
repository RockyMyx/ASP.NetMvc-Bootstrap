using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using MvcBootstrap.MssqlEFModel;
//using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;

namespace MvcBootstrap.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DBEntity db = new DBEntity())
            {
                IEnumerable<T_Module> modules = db.GetModuleTree();
                foreach (T_Module module in modules)
                {
                    Console.WriteLine(module.Name);
                }
            }

            Console.ReadLine();
        }
    }
}
