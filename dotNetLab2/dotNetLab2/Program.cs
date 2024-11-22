using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Reflection;

namespace dotNetLab2
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string sentence = "Learning C# is great! Let's keep coding!";
            string charToFind = "e";

            Console.WriteLine("Розширені методи для рядків");
            Console.WriteLine($"Початковий рядок: {sentence}");
            string reversedSentence = sentence.ReverseString();
            Console.WriteLine($"Перевернутий рядок: {reversedSentence}");

            int occurrences = sentence.CountOccurrences(charToFind);
            Console.WriteLine($"Кількість символів '{charToFind}' в рядку: {occurrences}");

            Console.WriteLine("\nРозширені методи для масивів");
            double[] numbers = { 5.5, 10.3, 15.8, 25.2, 30.1 };
            Console.WriteLine($"Масив чисел: {string.Join(", ", numbers)}");

            double sum = numbers.CalculateSum();
            double average = numbers.CalculateAverage();
            Console.WriteLine($"Сума елементів масиву: {sum}");
            Console.WriteLine($"Середнє значення елементів масиву: {average}");

            var userDictionary = new ExtendedDictionary<int, string, string>();
            userDictionary.Add(1, "Олена", "Шевченко");
            userDictionary.Add(2, "Дмитро", "Ковальчук");
            userDictionary.Add(3, "Ірина", "Петренко");

            Console.WriteLine("\nВиведення словника:");
            foreach (var user in userDictionary)
            {
                Console.WriteLine($"ID: {user.Key}, Ім'я: {user.Value.Item1}, Прізвище: {user.Value.Item2}");
            }

            Console.WriteLine($"Чи містить ID 3: {userDictionary.ContainsKey(3)}");
            Console.WriteLine($"Видалення користувача з ID 3: {userDictionary.Remove(3)}");

            Console.WriteLine("Кількість елементів у словнику після видалення: " + userDictionary.Count());

            Console.WriteLine("\nСпроба доступу до видаленого ключа:");
            var deletedUser = userDictionary[3];
            Console.WriteLine(deletedUser == null ? "Користувача з ID 3 не існує" : $"Ім'я: {deletedUser.Item1}, Прізвище: {deletedUser.Item2}");
        }
    }


    public class ExtendedDictionaryElement<T, U, V>
    {
        public T Key { get; }
        public U Value1 { get; }
        public V Value2 { get; }

        public ExtendedDictionaryElement(T key, U value1, V value2)
        {
            Key = key;
            Value1 = value1;
            Value2 = value2;
        }

        public override string ToString()
        {
            return $"Key: {Key}, Value1: {Value1}, Value2: {Value2}";
        }
    }

    public static class StringExtensions
    {
        public static string ReverseString(this string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Рядок не може бути порожнім або null.");

            return new string(str.Reverse().ToArray());
        }

        public static int CountOccurrences(this string str, string element)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("Рядок не може бути порожнім або null.");
            if (string.IsNullOrEmpty(element))
                throw new ArgumentException("Елемент для пошуку не може бути порожнім або null.");

            int count = 0;
            int index = 0;
            while ((index = str.IndexOf(element, index)) != -1)
            {
                count++;
                index += element.Length;
            }
            return count;
        }
    }

    public static class ArrayExtensions
    {
        public static double CalculateSum(this double[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("Масив не може бути порожнім або null.");
            return array.Sum();
        }

        public static double CalculateAverage(this double[] array)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("Масив не може бути порожнім або null.");
            return array.Average();
        }

        public static int CountGreaterThan(this double[] array, double value)
        {
            if (array == null || array.Length == 0)
                throw new ArgumentException("Масив не може бути порожнім або null.");
            return array.Count(num => num > value);
        }
    }

    public class ExtendedDictionary<K, V1, V2> : IEnumerable<KeyValuePair<K, Tuple<V1, V2>>>
    {
        private Dictionary<K, Tuple<V1, V2>> dictionary = new Dictionary<K, Tuple<V1, V2>>();

        public bool Add(K key, V1 value1, V2 value2)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary[key] = new Tuple<V1, V2>(value1, value2);
                return true;
            }
            return false;
        }

        public bool Remove(K key)
        {
            return dictionary.Remove(key);
        }

        public bool ContainsKey(K key)
        {
            return dictionary.ContainsKey(key);
        }

        public Tuple<V1, V2> this[K key]
        {
            get
            {
                dictionary.TryGetValue(key, out var value);
                return value;
            }
        }

        public bool Update(K key, V1 value1, V2 value2)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = new Tuple<V1, V2>(value1, value2);
                return true;
            }
            return false;
        }

        public int Count()
        {
            return dictionary.Count;
        }

        public IEnumerator<KeyValuePair<K, Tuple<V1, V2>>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
