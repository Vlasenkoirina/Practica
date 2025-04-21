using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Определение времени года по номеру месяца");
            Console.Write("Введите номер месяца (1-12): ");

            int monthNumber;

            // Проверка корректности ввода
            if (!int.TryParse(Console.ReadLine(), out monthNumber) || monthNumber < 1 || monthNumber > 12)
            {
                Console.WriteLine("Неверный номер месяца. Введите число от 1 до 12.");
                return;  // Прекращаем выполнение программы, если введен некорректный месяц
            }

            string season;

            if (monthNumber == 12 || monthNumber == 1 || monthNumber == 2)
            {
                season = "Зима";
            }
            else if (monthNumber >= 3 && monthNumber <= 5)
            {
                season = "Весна";
            }
            else if (monthNumber >= 6 && monthNumber <= 8)
            {
                season = "Лето";
            }
            else
            {
                season = "Осень";
            }

            Console.WriteLine($"Время года: {season}");
            Console.ReadKey();
        }
    }
}