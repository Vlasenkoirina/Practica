using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Исходные данные
            double area1 = 8.0;    // площадь в м²
        double paint1 = 1.2;   // количество краски в литрах

        // Расчет коэффициента расхода краски
        double ratio = paint1 / area1;

        // Новая площадь для расчета
        double area2 = 25.0;

        // Расчет необходимого количества краски
        double paint2 = ratio * area2;

        // Вывод результата
        Console.WriteLine($"Для покраски {area1} м² стены нужно {paint1} л краски.");
            Console.WriteLine($"Для покраски {area2} м² стены потребуется {paint2} л краски.");

            // Ожидание нажатия клавиши (для консольного приложения)
            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
}
}

