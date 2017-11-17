using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL_Proyecto_Final;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            RLE r = new RLE();
            string output = r.Decipher(r.Cipher("Eestaaa eesss uuuna pruueba"));
            Console.WriteLine(output);
            Console.ReadKey();
        }
    }
}
