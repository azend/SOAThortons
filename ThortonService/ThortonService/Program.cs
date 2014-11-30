using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThortonService
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedList<String, String> configs = ConfigHandler.readConfigFile();
            foreach(KeyValuePair<String,String> a in configs)
            {
                Console.WriteLine("<{0}>,<{1}>", a.Key, a.Value);
            }
            Console.ReadLine();

        }
    }
}
