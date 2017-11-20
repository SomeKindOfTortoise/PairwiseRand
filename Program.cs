using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pairwise_Tool
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand =new Random();
            List<List<String>> table = new List<List<string>>();
            string []lines = default(string[]);

            try
            {
                lines = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\testcase.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine("File " + Directory.GetCurrentDirectory() + "\\testcase.txt" + " does not exists.");
                return;
            }
            
            for (var i = 0; i < lines.Length; i++)
            {
                List<string> listed = new List<string>();
                string[] splitted = lines[i].Split(',');
                foreach (var split in splitted)
                {
                    listed.Add(split);
                }
                table.Add(listed);
            }


            Dictionary<String, bool> Pairs = new Dictionary<String, bool>();
            for (int i = 0; i < table.Count; i++)
            {
                for (int j = 0; j < table[i].Count; j++)
                {
                    for (int x = i + 1; x < table.Count; x++)
                    {
                        for (int z = 0; z < table[x].Count; z++)
                        {
                           // Console.WriteLine(table[i][j] + table[x][z]);
                            Pairs.Add(table[i][j] + table[x][z], false);
                        }
                    }
                }
            }

            List<List<string>> TestCases = new List<List<string>>();
            List<string> Actual = new List<string>();
            for (var i = 0; i < table.Count; i++)
            {
                Actual.Add(table[i][0]);
            }

            var end = false;
            while (!end)
            {
                var isValid = false; // 
                for (int i = 0; i < Actual.Count - 1; i++)
                {
                    for (int j = i + 1; j < Actual.Count; j++)
                    {
                        if (!Pairs[Actual[i] + Actual[j]])
                        {
                            isValid = true;
                            Pairs[Actual[i] + Actual[j]] = true;
                        }
                    }
                }
                if (isValid)
                {
                    List<string> copy = new List<string>(Actual);
                    TestCases.Add(copy);
                    var falses = 0;
                    foreach (var VARIABLE in Pairs)
                    {
                        if (!VARIABLE.Value)
                        {
                            falses++;
                            break;
                        }
                    }
                    if (falses > 0) end = false;
                    else end = true;
                }
                for (int i = 0; i < Actual.Count; i++)
                {
                    Actual[i] = table[i][rand.Next(0, table[i].Count)];
                }
            }

            foreach (var lists in TestCases)
            {
                string result = "";
                foreach (var res in lists)
                {
                    result += res + "\t";
                }
                Console.WriteLine(result);
            }

            Console.ReadKey();
        }
    }
}
