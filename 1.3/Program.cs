using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract3
{
    class EvenNumbersCounter
    {
        static void Main()
        {
            Console.WriteLine("Подсчет четных чисел в диапазоне [A, B]");

            int a = 0, b = 0; // Инициализируем переменные
            bool isValidInput;

            // Ввод и проверка корректности данных
            do
            {
                Console.Write("Введите начало диапазона (A): ");
                isValidInput = int.TryParse(Console.ReadLine(), out a);

                if (!isValidInput)
                {
                    Console.WriteLine("Ошибка! Введите целое число.");
                    continue;
                }

                Console.Write("Введите конец диапазона (B): ");
                isValidInput = int.TryParse(Console.ReadLine(), out b) && b >= a;

                if (!isValidInput)
                {
                    Console.WriteLine($"Ошибка! B должно быть целым числом >= A ({a}).");
                }
            } while (!isValidInput);

            // Подсчет четных чисел
            int count = 0;

            // Если начало диапазона нечетное, начинаем с следующего четного числа
            if (a % 2 != 0)
            {
                a++;
            }

            // Подсчитываем четные числа
            for (int i = a; i <= b; i += 2)
            {
                count++;
            }

            // Вывод результата
            Console.WriteLine($"Количество четных чисел в диапазоне [{a}, {b}]: {count}");

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
