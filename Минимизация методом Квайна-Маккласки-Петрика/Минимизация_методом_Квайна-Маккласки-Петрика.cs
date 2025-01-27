using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Text;

internal class Program
{
    public static void Sortirovka(int[] arr, string[] arr2)
    {
        int N = arr.Length;
        for (int i = 0; i < N; i++) // сортировка по группам(по возрастанию)
        {
            for (int j = i + 1; j < N; j++)
            {
                if (arr[i] > arr[j])
                {
                    int t1 = arr[i]; // происходят две сортировки массивов(кол. единиц и мас. терм)
                    arr[i] = arr[j];
                    arr[j] = t1;

                    string t = arr2[i];
                    arr2[i] = arr2[j];
                    arr2[j] = t;
                }
            }
        }
    }
    static void RemoveAt(ref string[] array, int index) // удаляет элемент массива
    {
        string []Newarr = new string[array.Length - 1];
        for (int i = 0; i < index; i++)
        {
            Newarr[i] = array[i];
        }
        for (int i = index+1; i < array.Length; i++)
        {
            Newarr[i - 1] = array[i];
        }
        array = Newarr;
    }
    private static void Main(string[] args)
    {
        Console.WriteLine("Введите колличество чисел(для минимизации): ");
        int N = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите термы: ");
        string[] arr2 = new string[N]; // Содержит массив импликант
        string[] arr3 = new string[N]; // Содержит массив конституент

        // ПОЛУЧАЕМ МАССИВ ЧИСЕЛ
        int[] arr = new int[arr2.Length];
        for (int i = 0; i < N; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }

        int k = Convert.ToInt32(Math.Round(Math.Log2(arr[N - 1]), MidpointRounding.ToNegativeInfinity) + 1);
        // oкругляем в меньшую сторону(степень двойки + 1)
        Console.WriteLine("Число разрядов:");
        Console.WriteLine(k);

        for (int i = 0; i < N; i++)
        {
            string gvoich = Convert.ToString(arr[i], 2); // перевод в двоичную систему счисления
            arr2[i] = gvoich.PadLeft(k, '0'); // добавляет 0 в начало строки, кол = k
            //получаем строковый массив терм(неотсортированный по группам)
        }
        for (int i = 0; i < N; i++) // выводим массив конституент
        {
            arr3[i] = arr2[i];
        }

        // Сортировка по колличеству единиц
        int kol_ed = 0;
        for (int i = 0; i < arr2.Length; i++)
        {
            for (int a = 0; a < k; a++)
            {
                // считаем кол единиц в строке
                if (arr2[i][a] == '1') kol_ed += 1;
            }
            arr[i] = kol_ed; // массив сод. кол. 1
            kol_ed = 0;
        }
        Console.WriteLine("До склейки:");
        Sortirovka(arr, arr2);
        foreach (string el in arr2)
        {
            Console.WriteLine(el);
        }
        /////

        List<string> list = new List<string>();//содержит список импликант(на каждом этапе склейки)
        List<int> list_2 = new List<int>();   // содержит номера использованных конъюнктов
        int chet = 0, num_sym = 0;
        char symbol = '-';

        //k - кол.нулей, поэтому макс. число склеиваний = k
        for (int j = 0; j < k; j++)             // Полная склейка!  
        {
            if (j > 0)
            {
                for (int i = 0; i < arr2.Length; i++)
                {
                    for (int a = 0; a < k; a++)
                    {
                        // считаем кол единиц в строке
                        if (arr2[i][a] == '1') kol_ed += 1;
                    }
                    arr[i] = kol_ed; // массив сод. кол. 1
                    kol_ed = 0;
                }
                Sortirovka(arr, arr2);
            }
            for (int i = 0; i < arr2.Length; i++)  // Склейка
            {
                for (int c = i + 1; c < arr2.Length; c++) // сравнение i группы с i + 1 группой
                {
                    if (arr[c] - arr[i] == 1)
                    {
                        for (int a = 0; a < k; a++)
                        {
                            if (arr2[i][a] != arr2[c][a])
                            {
                                chet++; // разница символов в конституэнтах
                                num_sym = a;
                            }
                        }
                        if (chet == 1)
                        {
                            if (!list_2.Contains(c)) // номера, которые мы должны удалить (используем)
                            {
                                list_2.Add(i);
                                list_2.Add(c);
                            }
                            StringBuilder sb = new StringBuilder(arr2[c]);
                            sb[num_sym] = symbol;
                            list.Add(Convert.ToString(sb)); // меняем символ 
                        }
                        chet = 0;
                    }
                }
            }
            for (int i = 0; i < arr2.Length; i++) // добавляем неиспользованные числа
            {
                if (!list_2.Contains(i))
                    list.Add(arr2[i]);
            }
            list.Sort();
            Array.Resize(ref arr2, list.Count); // изменение длины массива
            for (int i = 0; i < list.Count; i++)
            {
                arr2[i] = list.ToArray()[i];
            }
            list.Clear();                  // Очистка списков
            list_2.Clear();
            for (int i = 0; i < arr2.Length - 1;)
            {
                if (arr2[i] == arr2[i + 1])
                {
                    RemoveAt(ref arr2, i); // удаляем повторяющиеся элементы массива
                }
                else
                {
                    i++;
                }
            }
            Array.Resize(ref arr, arr2.Length); // изменение длины массива
        }

        Console.WriteLine("Склейка:");
        foreach (string el in arr2)
        {
            Console.WriteLine(el);
        }
        string[] arr4 = new string[arr2.Length];
        for (int i = 0;i < arr2.Length; i++) { arr4[i] = arr2[i]; }


        string[,] table = new string[arr2.Length + 1, arr3.Length + 1]; // построение импликантной матрицы
        for (int i = 0; i < arr2.Length + 1; i++)
        {
            for (int j = 0; j < arr3.Length + 1; j++)
            {
                table[i, j] = " 0  ";
                table[0, 0] = "    ";
                if (i > 0)
                    table[i, 0] = arr2[i - 1];
                if (j > 0)
                    table[0, j] = arr3[j - 1];
                for (int a = 0; a < k; a++)
                {
                    if (table[i, 0][a] != table[0, j][a])
                    {
                        if (table[i, 0][a] != '-')
                            chet++; // разница символов 
                    }
                }
                if (chet == 0)
                {
                    table[i, j] = " +  "; // меняем символ 
                }
                chet = 0;
            }
        }

        Console.WriteLine();
        Console.WriteLine("Импликантная матрица: ");
        for (int i = 0; i < arr2.Length + 1; i++)
        {
            for (int j = 0; j < arr3.Length + 1; j++)
            {
                Console.Write(table[i, j] + "    ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        string strBykv = "K =  ";
        for (int j = 0; j < arr3.Length + 1; j++)
        {
            for (int i = 0; i < arr2.Length + 1; i++)
            {
                if (i > 0)
                {
                    if (table[i, j] == " +  ")
                    {
                        strBykv += (char)(64 + i); // A = 65 символ -> английский алфавит
                        strBykv += '+';
                    }

                }
            }
            strBykv = strBykv.Substring(0, strBykv.Length - 1); // удаляем последний символ
            if (j > 0 && j != arr3.Length) strBykv += '*';
        }
        Console.WriteLine(strBykv);
        strBykv = strBykv.Remove(0, 4);
        string[] arr_bykv = new string[strBykv.Length];
        arr_bykv = strBykv.Split('*'); // Массив букв(разбиваем по символу *) 
        for (int i = 0; i < arr_bykv.Length; i++)
        {
            list.Add(arr_bykv[i]);
        }
        foreach (string el in list)
        {
            Console.WriteLine(el);
        }
        Console.WriteLine();

        list.Sort();
        for (int i = 0; i < list.Count; i++) // блок сокращения
        {
            for (int j = i + 1; j < list.Count;)
            {
                if (list[i] == list[j])
                {
                    list.RemoveAt(j);// Сокращение вида АА
                }
                else j++;
            }
        }
        for (int i = 0; i < list.Count; i++) // блок сокращения
        {
            for (int j = 0; j < list.Count; j++)
            {
                string Sokr = list[i];
                string Sokr_1 = list[j];
                if (Sokr.Length == 1 && i != j) // сокращение вида A(A+(*))
                {
                    for (int a = 0, b = 0; a < Sokr_1.Length; a++)
                    {
                        if (Sokr[b] == Sokr_1[a])
                        {
                            list.RemoveAt(j);
                        }
                    }
                }
            }
        }
        foreach (string el in list)
        {
            Console.WriteLine(el);
        }
        Console.WriteLine();

        string str = "";
        List<string> pod = new List<string>();
        int stroka = 1;
        char sym = '\0';
        while (stroka != 0) // блок раскрытия скобок
        {
            stroka = 0;
            pod.Clear();
            for (int i = 0; i < list.Count; i++)  
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    string Sokr = list[i];
                    string Sokr_1 = list[j];
                    if (Sokr.Length != 1 && Sokr_1.Length != 1)
                    {
                        string[] podmas = Sokr.Split('+');
                        string[] podmas_1 = Sokr_1.Split('+');
                        stroka++;
                        for (int a = 0; a < podmas.Length; a++)
                        {
                            for (int b = 0; b < podmas_1.Length; b++)
                            {
                                if (podmas[a] == podmas_1[b])
                                {
                                    sym = Convert.ToChar(podmas[a]);
                                }
                            }
                        }
                        for (int a = 0; a < podmas.Length; a++)
                        {
                            for (int b = 0; b < podmas_1.Length; b++)
                            {
                                if (podmas[a] == podmas_1[b])
                                {
                                    str += podmas[a];
                                    str += '+';
                                }
                                else
                                {
                                    if(sym != '\0')
                                    {
                                        if (!podmas_1[b].Contains(sym) && !podmas_1[a].Contains(sym))
                                        {
                                            str += podmas[a];
                                            str += podmas_1[b];
                                            str += "+";
                                        }
                                    }
                                    else
                                    {
                                        str += podmas[a];
                                        str += podmas_1[b];
                                        str += "+";
                                    }
                                }
                            }
                        }
                        sym = '\0';
                        str = str.Substring(0, str.Length - 1); // удаляем последний символ
                        pod.Add(str);
                        list.RemoveAt(j);
                        j--;
                        list.RemoveAt(i);
                        str = "";
                    }
                }
            }
            for (int i = 0; i < pod.Count; i++)
            {
                list.Add(pod[i]);
            }
        }

        if (list[list.Count - 1].Length != 1)
        {
            pod.Add(list[list.Count - 1]);
            list.RemoveAt(list.Count - 1);
            string[] minstr = pod[0].Split('+');
            pod.Clear();
            for (int i = 0; i < minstr.Length; i++)
            {
                pod.Add(minstr[i]);
                for (int j = 0; j < list.Count; j++)
                {
                    pod[i] += list[j];
                }
                for (int j = 0; j < pod[i].Length; j++)
                {
                    for (int a = 0; a < pod[i].Length; a++)
                    {
                        if (pod[i][j] == pod[i][a])
                        {
                            // удаляем повторяющиеся символы в строке
                            string newStr = new string(pod[i].Distinct().ToArray());
                            pod[i] = newStr;    
                        }
                    }
                }
            }
            foreach (string el in pod)
            {
                Console.WriteLine(el);
            }
            for (int i = 0; i < pod.Count; i++)
            {
                for (int j = 0; j < pod.Count; )
                {
                    if (pod[i].Length < pod[j].Length)
                    {
                        pod.RemoveAt(j);
                    }
                    else j++;
                }
            }
            Console.WriteLine();
            foreach (string el in pod)
            {
                Console.WriteLine(el);
            }
            Console.WriteLine();
            for (int i = 0; i < pod.Count; i++)
            {
                list.Clear();
                char[] fin = pod[i].ToCharArray();
                for(int j = 0; j < fin.Length; j++) { list.Add(Convert.ToString(fin[j])); }
                list.Sort();
                foreach (string el in list)
                {
                    Console.Write(el);
                }
                Console.WriteLine();
                for (int j = 0; j < list.Count; j++)
                {
                    int s = (char)(Convert.ToChar(list[j]) - 65);
                    arr2[j] = arr4[s];
                }
                Array.Resize(ref arr2, list.Count); // уменьшение длины массива

                string MyString = "fmin = ";
                int Znach = 1;
                for (int a = 0; a < arr2.Length; a++)
                {
                    for (int j = 0; j < k; j++)
                    {
                        if (arr2[a][j] == '0')
                        {
                            MyString += "!X" + Znach + "*";
                        }
                        else if (arr2[a][j] == '1')
                        {
                            MyString += "X" + Znach + "*";
                        }
                        Znach++;
                        // удаление последнего символа строки
                        if (j == k - 1) MyString = MyString.Substring(0, MyString.Length - 1);
                    }
                    Znach = 1;
                    if (a != arr2.Length - 1) MyString += "+";
                }
                Console.WriteLine(MyString);
            }
        }
        else
        {
            list.Sort();
            foreach (string el in list)
            {
                Console.WriteLine(el);
            }
            Console.WriteLine();

            for (int i = 0; i < list.Count; i++)
            {
                int s = (char)(Convert.ToChar(list[i]) - 65);
                arr2[i] = arr2[s];
            }
            Array.Resize(ref arr2, list.Count); // уменьшение длины массива

            foreach (string el in arr2)
            {
                Console.WriteLine(el);
            }

            Console.WriteLine();
            string MyString = "fmin = ";
            int Znach = 1;
            for (int i = 0; i < arr2.Length; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    if (arr2[i][j] == '0')
                    {
                        MyString += "!X" + Znach + "*";
                    }
                    else if (arr2[i][j] == '1')
                    {
                        MyString += "X" + Znach + "*";
                    }
                    Znach++;
                    // удаление последнего символа строки
                    if (j == k - 1) MyString = MyString.Substring(0, MyString.Length - 1);
                }
                Znach = 1;
                if (i != arr2.Length - 1) MyString += "+";
            }
            Console.WriteLine(MyString);
        }
        Console.WriteLine("Конец");
    }
}//3,4,5,7,8,9,11,13
//0,1,5,7,8,10,14,15
//0,1,4,5,7,10,14,15