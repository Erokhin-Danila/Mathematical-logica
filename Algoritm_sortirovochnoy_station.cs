internal class Program  
{
    public static int priority(char c)
    {
        if (c == '*' || c == '/') return 2;
        if (c == '+' || c == '-') return 1;
        else return 0;
    }
    public static int calculate(char oper, int a, int b)
    {
        switch (oper)
        {
            case '+':
                return a + b;
                break;
            case '-':
                return a - b;
                break;
            case '*':
                return a * b;
                break;
            case '/':
                return a / b;
                break;
        }
        return 0;
    }
    private static void Main(string[] args)
    {
        Console.WriteLine("Введите Выражение(Инфиксная форма): ");
        string instr = Console.ReadLine();
        string outstr = null;

        /////////////////////// 
        // ПОЛУЧАЕМ МАССИВ СОСТОЯЩИЙ ИЗ ДЛИН ЧИСЕЛ СТРОКИ
        int kol = 0;
        int j = 0;
        int[] array = new int[100];
        for (int i = 0; i < instr.Length; i++)
        {
            if (instr[i] >= '0' && instr[i] <= '9')
            {
                kol++;
            }
            else
            {
                array[j] = kol;
                j++;
                kol = 0;
            }
            
        }
        array[j] = kol; // для последнего элемента
        //////////////////////
        /*
        for(j = 0; j < 5; j++)
        {
            Console.WriteLine(array[j]);
        }
        */
        
        var oper = new Stack<char>(100);
        for (int i = 0; i < instr.Length; i++)
        {
            if (instr[i] >= '0' && instr[i] <= '9')
            {
                // в выходную строку помещаем числа (очередь)
                outstr += instr[i];
            }
            else
            {
                if (oper.Count == 0)
                {
                    // помещаем в стек
                    oper.Push(instr[i]);
                }
                else if (priority(instr[i]) > priority(oper.First()))
                {
                    // помещаем в стек
                    oper.Push(instr[i]);
                }
                else if (priority(instr[i]) == priority(oper.First()))
                {
                    outstr += oper.Pop();
                    // помещаем в стек
                    oper.Push(instr[i]);

                }
                else if (priority(instr[i]) < priority(oper.First()))
                {
                    while(oper.Count != 0)
                    {
                        outstr += oper.Pop();
                    }
                    // помещаем в стек
                    oper.Push(instr[i]);
                }
            }
        }
        while (oper.Count != 0)
        {
            outstr += oper.Pop();   // cтек пуст
        }
        Console.WriteLine("Постфиксная форма: ");
        foreach (char el in outstr)
        {
            Console.Write(el);
        }

        System.Console.WriteLine();
        int a = 0, b = 0;
        j = 0;
        string pazm_chicl = null;
        Stack<int> chisl = new Stack<int>(100);
        for (int i = 0; i < outstr.Length; i++)
        {
            if (outstr[i] >= '0' && outstr[i] <= '9')
            {
                if (array[j] > 1)
                {
                    while (array[j] != 0)
                    {
                        pazm_chicl += outstr[i];
                        array[j]--;
                        i++;
                    }
                    j++;
                    i--;
                }
                else
                {
                    pazm_chicl = Convert.ToString(outstr[i]);
                }
                // конвертируем в число
                int c = Convert.ToInt32(pazm_chicl);
                // помещаем в стек
                chisl.Push(c);
                pazm_chicl = null;
            }
            else
            {
                a = chisl.Pop();
                b = calculate(outstr[i], chisl.Pop(), a);
                chisl.Push(b);
            }
        }
        Console.WriteLine("Результат: " + chisl.Pop());
    }
}