using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Utils
    {        
        /// <summary>
        /// Проверка строки 
        /// </summary>
        /// <param name="line">Строка, которая должна соответствовать шаблону "Город А; Город Б; Расстояние от А до Б; Расстояние от Б до А".</param>
        /// <param name="numberOfLine">Номер строки в файле.</param>
        public static bool IsCorrectLine(string[] line, int numberOfLine)
        {            
            if (line.Length != 4)
                throw new Exception($"Invalid file filling format! On {numberOfLine + 1} line");

            else if (double.TryParse(line[2], out double num) != true && line[2] != "N/A")
                throw new Exception($"Invalid file filling format! On {numberOfLine + 1} line; 3 argument (not number)");
            
            else if (double.TryParse(line[3], out num) != true && line[3] != "N/A")
                throw new Exception($"Invalid file filling format! On {numberOfLine + 1} line; 4 argument (not number)"); 
            
            else if (line.Any(x => x == ""))
                throw new Exception($"Invalid file filling format! On {numberOfLine + 1} line; one of the arguments is an empty string!");

            return true;
        }
    }
}