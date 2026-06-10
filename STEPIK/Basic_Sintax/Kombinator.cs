using System;
using System.Collections.Generic;
using System.Text;

namespace BFPL.STEPIK.Basic_Sintax
{
    internal class Kombinator
    {

        static List<List<string>> GetKobitations(List<string> input) 
        {
            //s t e p i k r o c k s
            List<List<string>> combinations = new List<List<string>>();
            combinations.Add([]);

            for (int i = 0; i < input.Count; i++)
            {
                string s = input[i];
                int index = i;
                for (int j = 1; index + j <= input.Count; j++)
                {
                    combinations.Add(input.GetRange(index, j));
                }
            }

            combinations = combinations.OrderBy(combination => combination.Count).ToList();

            Console.Write("[");
            for (int i = 0; i < combinations.Count; i++)
            {
                Console.Write("[");
                if (combinations[i].Count == 0) Console.Write("");
                else Console.Write("'" + string.Join("', '", combinations[i]) + "'");
                Console.Write("]");
                if (i != combinations.Count - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine("]");

            return combinations;
        }
    }
}
