using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            Blowfish b = new Blowfish();
            Console.WriteLine(b.Decrypt(b.Encrypt("aaaaaaaa")).ToString());
            Console.ReadKey();
        }
    }
}
