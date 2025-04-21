using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pract4
{
    class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }

        public Employee(string name, string position, decimal salary)
        {
            Name = name;
            Position = position;
            Salary = salary;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Имя: {Name}, Должность: {Position}, Зарплата: {Salary} руб.");
        }
    }

    class Manager : Employee
    {
        public string Department { get; set; }

        public Manager(string name, string position, decimal salary, string department)
            : base(name, position, salary)
        {
            Department = department;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Отдел: {Department}");
        }

        public void ManageTeam()
        {
            Console.WriteLine("Управляет командой...");
        }
    }

    class Intern : Employee
    {
        public string University { get; set; }

        public Intern(string name, string position, decimal salary, string university)
            : base(name, position, salary)
        {
            University = university;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Университет: {University}");
        }

        public void Learn()
        {
            Console.WriteLine("Изучает и набирается опыта...");
        }
    }

    class Program
    {
        static void Main()
        {
            Employee employee = new Employee("Иван Иванов", "Разработчик", 50000);
            Manager manager = new Manager("Анна Смирнова", "Менеджер проекта", 80000, "ИТ-отдел");
            Intern intern = new Intern("Максим Кузнецов", "Стажёр", 20000, "МГУ");

            Console.WriteLine("Информация о сотруднике:");
            employee.DisplayInfo();

            Console.WriteLine("\nИнформация о менеджере:");
            manager.DisplayInfo();
            manager.ManageTeam();

            Console.WriteLine("\nИнформация о стажёре:");
            intern.DisplayInfo();
            intern.Learn();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
