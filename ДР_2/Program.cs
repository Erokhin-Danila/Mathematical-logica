using System;
using System.Collections.Generic;
namespace MatLogLR4
{
    class Programm
    {
        static void Print_Carriage(int number)      // Головка чтения записи. (с умен\увел номера меняется сдвиг головки)
        {
            Console.WriteLine();                    //!пробел!
            int count = 1;
            while (count < number)
            {
                Console.Write(" ");
                count++;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("↓");
            Console.ResetColor();
        }
        static void Print_Elements(string[] Lenta, int number)
        {
            for (int i = 0; i < Lenta.Length; i++)
            {
                if (i == number - 1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                Console.Write(Lenta[i]);
                Console.ResetColor();
            }
        }
        static void Main()
        {
            Console.WriteLine("Введите Алфавит Машины Тьюринга:");
            string alfavit = Console.ReadLine();
            string[] mas = alfavit.Split(',');
            Console.WriteLine("Алфавит состоит из {0},#", alfavit);
             
            Console.WriteLine("Введите элементы на ленту");
            string str = Console.ReadLine();
            for(int i = 0; i < mas.Length; i++)
            {
                if (!str.Contains(mas[i]))
                {
                    Console.WriteLine("Вы ввели символ не содержащийся в алфавите!");
                    return;
                }
            }
            Array.Resize(ref mas, mas.Length + 1); // увеличение длины массива
            mas[mas.Length - 1] = "#";

            int sym = 0;
            string[] Lenta = new string[20];            // ЛЕНТА СОСТОИТ ИЗ СТРОКИ И ПУСТЫХ СИМВОЛОВ 
            for (int i = 6; i < 6 + str.Length; i++)
            {
                Lenta[i] = Convert.ToString(str[sym]);
                sym++;
            }
            for (int i = 0; i < Lenta.Length; i++) if(Lenta[i] == null) Lenta[i] = "#"; //  # - ПУСТОЙ СИМВОЛ
            foreach (string c in Lenta) { Console.Write(c); }
            
            string[] states = new string[10];  // создание массива состояний из 10 элементов
            Console.WriteLine('\n' + "Ввод состояний (q0 - конечное состояние):");
            states[0] = "q0";
            for (int i = 1; i < states.Length; i++)
            {
                Console.Write($"Состояние {i}: ");
                states[i] = Console.ReadLine();
                if (states[i].Contains('.'))
                {
                    states[i] = states[i].Substring(0, states[i].Length - 1);
                    break;
                }
            }

            // вывод массива состояний на экран
            Console.WriteLine("Массив состояний:");
            foreach (string state in states) Console.Write(state + " ");       

            Console.WriteLine();
            Console.WriteLine("Создадим таблицу перехода:");
            Console.WriteLine("Введите начальное состояние:");
            string[] new_simbol = new string[mas.Length];
            string[] mas_states = new string[mas.Length];
            string begin = Console.ReadLine();
            for (int i = 0; i < mas.Length; i++)
            {
                mas_states[i] = begin;
                if (!states.Contains(mas_states[i]))
                {
                    Console.WriteLine("Ошибка ввода состояний!");
                    return;
                }
                Console.WriteLine("Работаем с состоянием {0} и символом алфавита {1}", mas_states[i], mas[i]);
                Console.WriteLine("Символ {0} заменим:", mas[i]);
                new_simbol[i] = Console.ReadLine();
                if (!mas.Contains(new_simbol[i]))
                {
                    Console.WriteLine("Данный символ не содержится в алфавите!");
                    return;
                }
                Console.WriteLine("Из состояния {0} в состояние", mas_states[i]);
                mas_states[i] = Console.ReadLine(); 
            }
            foreach (string el in new_simbol) Console.Write(el + " ");
            


            string elements_direction;
            Console.WriteLine('\n' + "Выберете направление движения:");
            Console.WriteLine("1.влево");
            Console.WriteLine("2.вправо");
            string direction = Console.ReadLine();
            if (direction == "1")
            {
                elements_direction = "Left";
            }
            else
            {
                elements_direction = "Right";
            }

            Console.Write("Введите номер символа, с которого нужно начать:");
            int number = Convert.ToInt32(Console.ReadLine());
            if (number < 6 || number > 6 + str.Length)
            {
                Console.WriteLine("Неверный номер каретки!");
                return;
            }
            Console.WriteLine("Работа машины Тьюринга!");
            Print_Carriage(number);
            Print_Elements(Lenta, number);
            for (int i = number; ;)
            {
                for(int j = 0;  j < new_simbol.Length; j++)
                {
                    if(Lenta[i - 1] == mas[j])              // Замена символа
                    {
                        Lenta[i - 1] = new_simbol[j];
                        begin = mas_states[j];
                        break;
                    }
                }
                if (begin == "q0")
                {
                    break;
                }                
                Print_Carriage(i);
                Print_Elements(Lenta, i);
                switch (elements_direction)
                {
                    case "Left":
                        i--;
                        break;
                    case "Right":
                        i++;
                        break;
                    default: return;
                }
            }

            //Console.WriteLine(Lenta[number] - 1);
            //number--;
            Console.WriteLine();
            Console.WriteLine("Конец!");
        }
    }
}
