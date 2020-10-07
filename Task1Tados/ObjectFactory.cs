using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Task1Tados.Exceptions;

namespace Task1Tados
{
    public class ObjectFactory
    {
        /*
         * Необходимо реализовать фабрику объектов из определенный сборки
         * В конструктор фабрике должно передаваться полное имя файла сборки (путь/smth.dll)
         * В единственный метод Create должны передаваться следующие параметры:
         * - string className  — короткое имя класса для создания
         * - params object[] constructorArgs — набор значений для передачи в качестве параметров в конструктор
         * Данный метод должен возвращать экземпляр класса className, если такой существует в переданной в конструктор сборке и может быть создан
         + Метод не должен падать при передаче имени несуществующего класса
         + Метод не должен падать при невозможности создать класс
         + Метод не должен падать при передаче некорректного набора аргументов для конструктора
         + Метод должен нормально работать в случае, если у класса несколько разных конструкторов (должна быть возможность создать экземпляр через любой конструктор, в зависимости от переданных параметров)
         + По возможности, необходимо как можно меньше использовать рефлексию (подумайте над внутренним кэшем вашей фабрики для уже "исследованных" данных о типах)
         * 
         * не должен падать следуют воспринимать как вы должны это обрабатывать
         + Лучше обрабатывать эти ситуации и кидать осмысленные исключения
         */

        private List<Type> _assemblyExportedTypes;

        public ObjectFactory(string path)
        {
            //проверить, существует ли сборка
            Assembly assembly;
            try
            {
                assembly = Assembly.LoadFrom(path);
            }
            catch (FileNotFoundException ex)
            {
                throw new AssemblyNotFoundException("Файл сборки не найден");
            }
            catch (BadImageFormatException ex)
            {
                //System.BadImageFormatException: "Could not load file or assembly [...]
                //Format of the executable (.exe) or library (.dll) is invalid."
                throw new AssemblyHasInvalidFormatException("Файл сборки имеет неверный формат");
            }

            //загрузить данные о типах
            _assemblyExportedTypes = assembly.ExportedTypes.ToList();
        }

        public object Create(string className, params object[] constructorArgs)
        {
            //проверка существования такого типа
            var type = _assemblyExportedTypes.Where(x => x.Name == className).FirstOrDefault();
            if (type == null)
            {
                throw new TypeNotExistException($"Тип {className} не найден");
            }

            if (!type.IsClass)
            {
                throw new TypeIsNotClassException($"Тип {className} не является классом");
            }

            //создание экземпляра
            //проверка, что экземпляр создан (существует конструктор, подходят аргументы)
            object result;
            try
            {
                result = Activator.CreateInstance(type, constructorArgs);
            }
            catch (MissingMethodException ex)
            {
                //System.MissingMethodException: "Constructor on type 'JuniorTest.Models.User' not found."
                throw new ConstructorNotFoundException($"Конструктор класса {className} с указанными параметрами не найден");
            }

            return result;
        }
    }
}
