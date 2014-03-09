using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using MvcBootstrap.MssqlEFModel;
//using MvcBootstrap.MysqlEFModel;
using MvcBootstrap.OracleEFModel;
using System.Data.Objects;

namespace MvcBootstrap.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DBEntity db = new DBEntity())
            {
                //IEnumerable<T_Module> modules = db.GetModuleTree();
                //foreach (T_Module module in modules)
                //{
                //    Console.WriteLine(module.Name);
                //}

                //List<UserBrowseViewModel> models = db.GetUserBrowse(1).ToList();
                //foreach (UserBrowseViewModel m in models)
                //{
                //    Console.WriteLine(m.Name);
                //}
                //Console.WriteLine(models.Count);

                //List<string> operations = db.GetUserOperation(1, 2).ToList();
                //foreach (string o in operations)
                //{
                //    Console.WriteLine(o);
                //}
                //Console.WriteLine(operations.Count);

                //db.DeleteObjects("2,3", "T_UserRole", "UserID");
            }

            Console.WriteLine("ok");
            Console.ReadLine();
        }
    }
}
