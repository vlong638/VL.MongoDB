using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBStarter
{
    class Program
    {
        static void Main(string[] args)
        {
            var business = new Business();
            string input = "";
            while ((input=Console.ReadLine())=="")
            {
                business.test();
            }
        }
    }
}
