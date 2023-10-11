using System;
using Microsoft.Win32;

namespace RegistryExplorer
{
    class Program
    {
        static RegistryKey rootKey = Registry.CurrentUser.OpenSubKey("Software");

        static RegistryKey currentKey = rootKey;

        static string[] menuOptions = new string[]
        {
"Перейти в подраздел",
"Вернуться в главный подраздел",
"Просмотреть элементы",
"Просмотреть имена и значения Value",
"Очистить консоль",
"Выйти из программы"
        };

        static void Main(string[] args)
        {

            Console.WriteLine("Добро пожаловать в city 17");
            Console.WriteLine("Вы начинаете с раздела {0}", rootKey.Name);

            int option = OpenMenu();

            while (option != menuOptions.Length)
            {
                switch (option)
                {
                    case 1:
                        GoToSubKey();
                        break;
                    case 2:
                        GoToRootKey();
                        break;
                    case 3:
                        ViewSubKeys();
                        break;
                    case 4:
                        ViewValues();
                        break;
                    case 5:
                        ClearConsole();
                        break;
                    default:
                        Console.WriteLine("Неверный ввод.");
                        break;
                }

                option = OpenMenu();
            }

            Console.WriteLine("До свидания!");
        }

        static int OpenMenu()
        {
            Console.WriteLine("\nВыберите один из следующих вариантов:");
            for (int i = 0; i < menuOptions.Length; i++)
            {
                Console.WriteLine("{0}. {1}", i + 1, menuOptions[i]);
            }
            Console.Write("Ваш выбор: ");
            int option = 0;
            int.TryParse(Console.ReadLine(), out option);
            return option;
        }

        static void GoToSubKey()
        {
            Console.Write("Введите имя подраздела: ");
            string subKeyName = Console.ReadLine();
            RegistryKey subKey = currentKey.OpenSubKey(subKeyName);
            if (subKey != null)
            {
                currentKey = subKey;
                Console.WriteLine("Вы перешли в подраздел {0}", currentKey.Name);
            }
            else
            {
                Console.WriteLine("Подраздел {0} не существует.", subKeyName);
            }
        }

        static void GoToRootKey()
        {
            currentKey = rootKey;
            Console.WriteLine("Вы вернулись в главный подраздел {0}", currentKey.Name);
        }

        static void ViewSubKeys()
        {
            Console.WriteLine("Элементы в {0}:", currentKey.Name);
            foreach (string subKeyName in currentKey.GetSubKeyNames())
            {
                Console.WriteLine(subKeyName);
            }
        }

        static void ViewValues()
        {
            Console.WriteLine("Имена и значения Value в {0}:", currentKey.Name);
            foreach (string valueName in currentKey.GetValueNames())
            {
                object value = currentKey.GetValue(valueName);
                Console.WriteLine("{0} - {1}", valueName, value);
            }
        }

        static void ClearConsole()
        {
            Console.Clear();
            Console.WriteLine("Консоль очищена.");
        }
    }
}