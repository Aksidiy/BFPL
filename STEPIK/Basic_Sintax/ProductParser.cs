using System;
using System.Collections.Generic;
using System.Text;

namespace BFPL.STEPIK.Basic_Sintax
{
    internal class ProductParser
    {
        static public void ParseProducts() 
        {
            // Кроссовки: 1500 руб
            int productCount = Convert.ToInt32(Console.ReadLine());
            Dictionary<string, int> productDict = new Dictionary<string, int>();
            for (int i = 0; i < productCount; i++)
            {
                string[] product = Console.ReadLine()?.Split(": ") ?? ["", ""];
                string productName = product[0];
                int productCost = Convert.ToInt32(product[1].Split(" ")[0]);

                productDict.Add(productName, productCost);
            }

            // <li>Футболка: 150 руб</li>
            Dictionary<string, int> concurentProductDict = new Dictionary<string, int>();
            while (true)
            {
                string[] htmlLine = Console.ReadLine()?.Split(": ") ?? ["", ""];

                if (htmlLine[0] == "</html>")
                    break;
                else if (htmlLine[0].Substring(0, 4) == "<li>")
                {
                    string productName = htmlLine[0].Substring(4, htmlLine[0].Length - 4);
                    int productCost = Convert.ToInt32(htmlLine[1].Split(" ")[0]);

                    if (productDict.ContainsKey(productName))
                    {
                        concurentProductDict.Add(productName, productCost);
                    }
                }
            }

            // Футболка поло: 0 руб
            foreach (KeyValuePair<string, int> product in productDict)
            {
                if (!concurentProductDict.ContainsKey(product.Key))
                {
                    productDict[product.Key] = 0;
                }
                else
                {
                    productDict[product.Key] = concurentProductDict[product.Key] - productDict[product.Key];
                }
                Console.WriteLine($"{product.Key}: {productDict[product.Key]} руб");
            }

            //Итого: 350 руб
            int summ = 0;

            foreach (int value in productDict.Values)
            {
                summ += value;
            }

            Console.WriteLine($"Итого: {summ} руб");
        }
    }
}
