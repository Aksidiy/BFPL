using System;
using System.Collections.Generic;
using System.Text;

namespace BFPL.STEPIK.Basic_Plus_Sintax
{
    public class OnlineStore
    {
        public Dictionary<int, string> ProductsInStore;
        public List<int> ShoppingCart;

        public OnlineStore(Dictionary<int, string> productsInStore)
        {
            ProductsInStore = productsInStore;
            ShoppingCart = new List<int>();
        }

        public void PrintProductsInStore()
        {
            if (ProductsInStore.Count == 0)
            {
                Console.WriteLine("В магазине нет товаров на продажу.");
                return;
            }

            Console.WriteLine("Товары на продажу:");
            foreach (KeyValuePair<int, string> Product in ProductsInStore)
            {
                Console.WriteLine($"{Product.Key}\t{Product.Value}");
            }
        }

        public void PrintShoppingCart()
        {
            if (ShoppingCart.Count == 0)
            {
                Console.WriteLine("Корзина пуста.");
                return;
            }

            Console.WriteLine("Товары в корзине:");
            for (int i = 0; i < ShoppingCart.Count; i++)
            {
                if (!ProductsInStore.ContainsKey(ShoppingCart[i]))
                {
                    Console.WriteLine($"{i}\tТовар с кодом [{ShoppingCart[i]}] снят с продажи.");
                }
                else
                {
                    Console.WriteLine($"{i}\t{ShoppingCart[i]}\t{ProductsInStore[ShoppingCart[i]]}");
                }
            }
        }

        public void AddProductInStore(int id, string product)
        {
            if (ProductsInStore.ContainsKey(id))
            {
                Console.WriteLine("Код товара занят, выберите другой код для товара.");
            }
            ProductsInStore.Add(id, product);
        }
    }
}
