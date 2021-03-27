using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab3
{    
    public class Floyd_Warshall
    {
        public List<string> cities { get; private set; }
        public List<string[]> allPathInfo { get; private set; }
        public double[,] adjMatr { get; private set; }
        public double[,] bestPathMatr { get; private set; }
        int[,] pathMatr;

        public Floyd_Warshall(string fileName)
        {
            cities = new List<string>();
            allPathInfo = GetFileInfo(fileName);            
            BuildAdjMatr();
            bestPathMatr = new double[cities.Count, cities.Count];
            BuildBestPathMatr();
        }

        /// <summary>
        /// Build most optimal fileName by Floyd-Warshall algorithm
        /// </summary>        
        /// <returns></returns>
        public void BuildBestPathMatr()
        {
            int n = cities.Count;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    bestPathMatr[i,j] = adjMatr[i,j];
                }
            }            
            
            pathMatr = new int[n, n];

            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (bestPathMatr[i, k] + bestPathMatr[k, j] < bestPathMatr[i, j])
                        {
                            bestPathMatr[i, j] = bestPathMatr[i, k] + bestPathMatr[k, j];
                            pathMatr[i, j] = k + 1;
                        }                            
                    }
                }
            }
        }

        public List<string> Path(string from, string to)
        {
            int i;
            int j;
            
            if ((i = cities.IndexOf(from)) == -1)
                throw new ArgumentException("You entered incorrect name of first city!");
            if ((j = cities.IndexOf(to)) == -1)
                throw new ArgumentException("You entered incorrect name of second city!");

            // Если не существует пути
            if (bestPathMatr[i, j] == double.MaxValue)
            {
                throw new Exception("There is no way!");                
            }

            List<string> Path = new List<string>();
            Path.Add(cities[i]); // Добавляем первую вершину
            getPath(Path, i, j); // Добавляем посещенные вершины
            Path.Add(cities[j]); // Добавляем последнюю вершину

            Console.Write("Start ----- ");
            foreach (string city in Path)
            {
                Console.Write($"{city} ----- ");
            }
            Console.Write("end");

            Console.WriteLine($"\nPath length = {bestPathMatr[i,j]}");

            return Path;
        }

        void getPath(List<string> Path, int i, int j)
        {
            int k = pathMatr[i, j] - 1;

            if (k == -1) return;

            getPath(Path, i, k);
            Path.Add(cities[k]);
            getPath(Path, k, j);
        }

        /// <summary>
        /// Read information from file and return array of strings with path info
        /// </summary>
        /// <returns>Return null if line is incorrect. Array with path info.</returns>
        List<string[]> GetFileInfo(string fileName)
        {
            allPathInfo = new List<string[]>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                string[] curLine; // Текущая строка файла
                int lineNum = 0;
                while (sr.Peek() != -1)
                {
                    curLine = sr.ReadLine().Split(';'); // Делим каждую строку файла по символу разделителю ';'

                    for (int i = 0; i < curLine.Length; i++) // Убираем лишние пробелы в названиях городов
                        curLine[i] = curLine[i].Trim();
                        
                    if (!Utils.IsCorrectLine(curLine, lineNum)) break; // Проверка строки на корректность
                    allPathInfo.Add(curLine);

                    // Добавляем названия городов
                    cities.Add(curLine[0]);
                    cities.Add(curLine[1]);

                    lineNum++;
                }
                if (allPathInfo.Count == 0) throw new Exception("Empty file!");
            }
            cities = cities.Distinct().ToList<string>(); // Удаляем все дублирующиеся города

            return allPathInfo;
        }

        /// <summary>
        /// Build adjacency matrix
        /// </summary>
        /// <returns>Adjacency matrix</returns>   
        public void BuildAdjMatr()
        {
            int n = cities.Count;
            adjMatr = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j) adjMatr[i, j] = 0;
                    else adjMatr[i,j] = double.MaxValue;
                }
            }
            foreach (string[] path in allPathInfo)
            {
                //Находим индексы городов, у которых указаны расстояния путей между ними
                int from = cities.FindIndex(x => x == path[0]);
                int to   = cities.FindIndex(x => x == path[1]);

                // Если расстояние от города к городу указано в виде числа, то присваиваем соотвествующему элементу это число, иначе по-умолчанию ему присваивается максимальное значение для типа double
                if (path[2] != "N/A") adjMatr[from, to] = double.Parse(path[2]);
                else adjMatr[from, to] = double.MaxValue;
                if (path[3] != "N/A") adjMatr[to, from] = double.Parse(path[3]);
                else adjMatr[to, from] = double.MaxValue;
            }
        }        
    }
}
