using System;
using System.Collections.Generic;

namespace ArithmeticExpressionChecker
{
    class Program
    {
        enum stait
        {
           zerostate, stait_1, stait_2, stait_3, stait_4   
           // нач. состояние, оператор, операнд, открывающая скобка, закрывающая скобка
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Введите арифметическое выражение:");
            string expression = Console.ReadLine();

            if (CheckExpression(expression))
            {
                Console.WriteLine("Выражение корректно.");
            }
            else
            {
                Console.WriteLine("Выражение некорректно.");
            }
        }

        static bool CheckExpression(string expression)
        {
            stait st = stait.zerostate;
            int c = 0;
            for (int i = 0, j = 1; i < expression.Length; i++, j++)
            {
                if (j == expression.Length) j = i;
                char ch = expression[i];
                char ch1 = expression[j];
                if (ch == '+' || ch == '-' || ch == '*' || ch == '/')
                {
                    if (st == stait.stait_1)
                        return false;
                    st = stait.stait_1;
                }
                else if(ch >= '0' && ch <= '9')
                {
                    if (st == stait.stait_4) { return false; }
                    st = stait.stait_2;
                }
                else if (ch == '(')
                {
                    c++;
                    if (ch1 == '+' || ch1 == '*' || ch1 == '/')
                        return false;
                    if(st == stait.stait_2) return false;
                    st = stait.stait_3;
                }
                else if (ch == ')')
                {
                    c--;
                    if (st == stait.stait_1 || st == stait.stait_3) return false;
                    st = stait.stait_4;
                    if(c == -1) return false;
                }
            }
            if (c != 0)  return false; 
            if (st == stait.stait_2 || st == stait.stait_4) return true;
            else return false;
        }
    }
}