using System;
using System.Collections.Generic;
using System.Linq;
namespace MatLogLR3
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Введите логическое выражение (A + B, !A, B+!C):");
                string str;
                str = Console.ReadLine();
                string[] singles = str.Split(','); // разбиваем по запятым
                List<string> list = new List<string>();
                for (int i = 0; i < singles.Length; i++) { list.Add(singles[i]); }
                //list.Sort();
                foreach (string el in list) { Console.WriteLine(el); }
                Console.WriteLine();
                string camp = "!";
                string camp_1 = "";
               
                for (int i = 0; i < list.Count;i++) 
                { 
                    for(int j = 0; j < list.Count; j++)
                    {
                        if (i != j)
                        {
                            // разбиваем элемент списка на массив дизъюнктов
                            string[] str_list = list[i].Split('+');
                            // разбиваем элемент списка на массив дизъюнктов
                            string[] str_list_1 = list[j].Split('+');
                            
                            

                            for (int a = 0; a < str_list.Length; a++)
                            {
                                for(int b = 0; b < str_list_1.Length; b++)
                                {
                                    if ("!" + str_list[a] == str_list_1[b])
                                    {
                                        camp_1 = str_list[a];
                                        camp += str_list[a]; // дизъюнкт вида !A 
                                        if(str_list.Length == 1 && str_list_1.Length == 1)
                                        {
                                            Console.WriteLine("Резолютивный вывод нуля возможен");
                                            return;
                                        }
                                        else if (str_list.Length == 1 && str_list_1.Length > 1)
                                        {
                                            for(int k = 0; k < str_list_1.Length; k++)
                                            {
                                                if (str_list_1[k] != '!' + str_list[a])
                                                {
                                                    list.Add(str_list_1[k]); // резольвента 
                                                }
                                            }
                                        }
                                        else if (str_list.Length > 1 && str_list_1.Length == 1)
                                        {
                                            for (int k = 0; k < str_list.Length; k++)
                                            {
                                                if ('!' + str_list[k] != str_list_1[b])
                                                {
                                                    list.Add(str_list[k]); // резольвента                 
                                                }
                                            }
                                        }
                                        else if (str_list.Length > 1 && str_list_1.Length > 1)
                                        {
                                            for (int k = 0; k < str_list.Length; k++)
                                            {
                                                if ('!' + str_list[k] != str_list_1[b])
                                                {
                                                    string s = str_list[k];
                                                    for (int c = 0; c < str_list_1.Length; c++)
                                                    {
                                                        if (str_list_1[c] != str_list[k] && str_list_1[c] != str_list_1[b])
                                                        {
                                                            s += "+";
                                                            s += str_list_1[c]; 
                                                        }
                                                    }                                              
                                                    list.Add(s);// резольвента 
                                                }
                                            }
                                        }
                                        //!D+E,!E+A,!A,D
                                        //!B,A+B,!A+C,!C
                                        //A+B,!A+C,!B+C,E+!C,!E+!C
                                        //!A+B,!C+D,!B+E,!D+F,!E+!F,!A+C,A
                                    }

                                }
                                camp = "!";
                            }
                        }
                        
                    }
                }
                Console.WriteLine();
                foreach (string el in list) { Console.WriteLine(el); }
                Console.WriteLine();
                Console.WriteLine("Резолютивный вывод нуля невозможен");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}