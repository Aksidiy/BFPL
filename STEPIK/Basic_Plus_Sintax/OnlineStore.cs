using System;
using System.Collections.Generic;
using System.Text;

namespace BFPL.STEPIK.Basic_Plus_Sintax
{
    /// <summary>
    /// Класс симулирует онлайн магазин.
    /// </summary>
    public class OnlineStore
    {
        public Dictionary<int, string> ProductsInStore;
        public List<int> ShoppingCart;

        public OnlineStore(Dictionary<int, string> productsInStore)
        {
            ProductsInStore = productsInStore;
            ShoppingCart = new List<int>();
        }

        /// <summary>
        /// Выводит в консоль список товаров онлайн магазина.
        /// </summary>
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

        /// <summary>
        /// Выводит в консоль список товаров в корзине.
        /// </summary>
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

        /// <summary>
        /// Добавляет товар в онлайн магазин.
        /// </summary>
        /// <param name="id"> Индекс товара </param>
        /// <param name="product"> Название товара. </param>
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
