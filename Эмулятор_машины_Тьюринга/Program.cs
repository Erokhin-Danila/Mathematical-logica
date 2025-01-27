using System;
using System.Collections.Generic;
namespace MatLogLR4
{
    class Programm
    {
        static void Print_Carriage(int number)      // Головка чтения записи. (с умен\увел номера меняется сдвиг головки)
        {
            Console.WriteLine();
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
            for (int i = 0; i < mas.Length; i++)
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
            for (int i = 0; i < Lenta.Length; i++) if (Lenta[i] == null) Lenta[i] = "#"; //  # - ПУСТОЙ СИМВОЛ
            foreach (string el in Lenta) { Console.Write(el); }

            string[] states = new string[10];  // создание массива состояний из 10 элементов
            Console.WriteLine('\n' + "Ввод состояний (q0 - конечное состояние):");
            states[0] = "q0";
            int chet_stait = 0;
            for (int i = 1; i < states.Length; i++)
            {
                Console.Write($"Состояние {i}: ");
                states[i] = Console.ReadLine();
                chet_stait++;
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
            
            string[,] new_simbol = new string[chet_stait, mas.Length];           // массив новых символов
            string[,] elements_direction = new string[chet_stait, mas.Length];   // массив перемещений
            string[,] mas_states = new string[chet_stait, mas.Length];

            Console.WriteLine("Введите начальное состояние:");
            string begin = Console.ReadLine();
            if (!states.Contains(begin))
            {
                Console.WriteLine("Ошибка ввода состояний!");
                return;
            }
            string vspom = "";
            vspom = begin;
            for (int j = 0; j < chet_stait; j++)
            {
                for (int i = 0; i < mas.Length; i++)
                {                    
                    Console.WriteLine("Работаем с состоянием {0} и символом алфавита {1}", vspom, mas[i]);
                    Console.WriteLine("Символ {0} заменим:", mas[i]);
                    new_simbol[j, i] = Console.ReadLine();
                    if (!mas.Contains(new_simbol[j, i]))
                    {
                        Console.WriteLine("Данный символ не содержится в алфавите!");
                        return;
                    }
                    Console.WriteLine("Выберете направление движения:");
                    Console.WriteLine("1.влево");
                    Console.WriteLine("2.вправо");
                    Console.WriteLine("3. На месте");
                    string direction = Console.ReadLine();
                    if (direction == "1")
                    {
                        elements_direction[j, i] = "Left";
                    }
                    else if (direction == "2")
                    {
                        elements_direction[j, i] = "Right";
                    }
                    else
                    {
                        elements_direction[j, i] = "Nothing";
                    }
                    Console.WriteLine("Следующее состояние:");
                    mas_states[j, i] = Console.ReadLine();
                    if (!states.Contains(mas_states[j, i]))
                    {
                        Console.WriteLine("Ошибка ввода состояний!");
                        return;
                    }
                    vspom = mas_states[j, i];
                }
            }
            
            foreach (string el in new_simbol) Console.Write(el + " ");
              
            Console.WriteLine();
            Console.WriteLine("Введите номер символа, с которого нужно начать:");
            int number = Convert.ToInt32(Console.ReadLine());
            if (number < 6 || number > 6 + str.Length)
            {
                Console.WriteLine("Неверный номер каретки!");
                return;
            }
            
            Console.WriteLine("Работа машины Тьюринга!");
            Print_Carriage(number);
            Print_Elements(Lenta, number);

            for (int i = number, a = 1; a < chet_stait + 1;a++)
            {
                for (int j = 0; j < mas.Length; j++)
                {
                    if (Lenta[i - 1] == mas[j] && begin == states[a])              // Замена символа a = 0 (q0)
                    {
                        Lenta[i - 1] = new_simbol[a - 1, j];
                        switch (elements_direction[a - 1, j])
                        {
                            case "Left":
                                i--;
                                break;
                            case "Right":
                                i++;
                                break;
                            case "Nothing":
                                break;
                            default: return;
                        }
                        begin = mas_states[a - 1, j];
                        a = 0;
                        Print_Carriage(i);
                        Print_Elements(Lenta, i);
                        break;
                    }
                }
                if (begin == "q0")
                {
                    break;
                }
            }
            //Console.WriteLine(Lenta[number] - 1);
            //number--;
            Console.WriteLine();
            Console.WriteLine("Конец!");
        }
    }
}