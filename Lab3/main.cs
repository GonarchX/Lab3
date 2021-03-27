using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Lab3
{
    class main
    {
        static void Main()
        {            
            try
            {
                string fileName;
                Console.WriteLine("Please enter file name: ");
                fileName = Console.ReadLine();

                Floyd_Warshall algTest = new Floyd_Warshall(fileName);

                algTest.Path("Москва", "Владивосток");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadLine();
        }        
    }
}
