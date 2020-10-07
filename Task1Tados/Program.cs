using System;
using Task1Tados.Exceptions;

namespace Task1Tados
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectFactory factory;

            try
            {
                //неверный путь, ошибка
                factory = new ObjectFactory(@"C:\НесуществующийПуть\JuniorTest.Models.dll");
            }
            catch (AssemblyNotFoundException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка: " + e.Message);
                Console.ResetColor();
            }

            try
            {
                //файл другого типа, ошибка
                factory = new ObjectFactory(@"NotDLL.txt");
            }
            catch (AssemblyHasInvalidFormatException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка: " + e.Message);
                Console.ResetColor();
            }

            //все ок
            factory = new ObjectFactory(@"JuniorTest.Models.dll");
            Console.WriteLine("Сборка загружена");

            try
            {
                //несуществующий класс
                var obj = factory.Create("OtherClass");
            }
            catch (TypeNotExistException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка: " + e.Message);
                Console.ResetColor();
            }

            try
            {
                //неправильный набор аргументов
                var obj = factory.Create("City", 1);
            }
            catch (ConstructorNotFoundException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка: " + e.Message);
                Console.ResetColor();
            }

            //все ок
            var city = factory.Create("City", "Perm");
            Console.WriteLine("Создан объект типа " + city.ToString());

            var user = factory.Create("User", "address@mail.ru", city);
            Console.WriteLine("Создан объект типа " + user.ToString());

            try
            {
                //создание не класса (ContentCategories - это enum)
                var category = factory.Create("ContentCategories");
            }
            catch (TypeIsNotClassException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка: " + e.Message);
                Console.ResetColor();
            }

            //добавила в класс City второй конструктор, задающий также id
            var town = factory.Create("City", "Chaykovsky", 1);
            Console.WriteLine("Создан объект типа " + town.ToString());

            Console.WriteLine();
        }
    }
}
