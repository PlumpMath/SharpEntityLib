using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpEntity;

namespace TestItemInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            Weapon sword = new Weapon("The big one", 50, 1);
            
            Console.WriteLine(sword.ID);
            Console.WriteLine(sword.Name);
            Console.WriteLine(sword.Level);
            Console.WriteLine(sword.ClassIndex);
            Console.WriteLine();
            Console.Read();
        }
    }
}
