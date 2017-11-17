using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLL_Proyecto_Final;
namespace Tests
{
    class Program
    {
        public static void Main(string[] args)
        {
            RLE r = new RLE();
            
            string output = r.Cipher("Estaa ees uuuna prueba");
            Console.WriteLine(output);
            Console.ReadKey();
        }
    }
}