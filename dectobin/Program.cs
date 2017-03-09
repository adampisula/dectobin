using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace dectobin
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();

            Console.WriteLine("------");
            Console.WriteLine(Big.DecToBin(s));
        }
    }

    class Big
    {
        public static string Add(string a, string b)
        {
            bool toneg = false;

            if (Compare(a, "0") == 2)
                return b;
            else if (Compare(b, "0") == 2)
                return a;

            if (IsNegative(a) == true && IsNegative(b) == false)
            {
                a = Negate(a);

                return (Compare(a, b) == 0) ? Negate(Subtract(a, b)) : Subtract(b, a);
            }
            else if (IsNegative(a) == false && IsNegative(b) == true)
            {
                b = Negate(b);

                return (Compare(b, a) == 0) ? Negate(Subtract(b, a)) : Subtract(a, b);

            }
            else if (IsNegative(b) == true && IsNegative(b) == true)
            {
                a = Negate(a);
                b = Negate(b);

                toneg = true;
            }

            int al = a.Length - 1;
            int bl = b.Length - 1;
            int s = 0;
            int i = 0;
            int ac = 0;
            int bc = 0;
            int cc = 0;
            string c = "";

            while ((i < a.Length) || (i < b.Length))
            {
                if (al - i < 0) ac = 0;
                else ac = (int)Char.GetNumericValue(a[al - i]);
                if (bl - i < 0) bc = 0;
                else bc = (int)Char.GetNumericValue(b[bl - i]);

                cc = (ac + bc + s) % 10;
                s = (ac + bc + s) / 10;

                c += cc.ToString();

                i++;
            }

            c += s.ToString();

            string cd = "";
            for (int j = c.Length - 1; j >= 0; j--)
                cd += c[j];

            return (toneg) ? TrimZero(Negate(cd)) : TrimZero(cd);
        }
        public static string Subtract(string n1, string n2)
        {
            if (Compare(n1, "0") == 2)
                return Negate(n2);
            else if (Compare(n2, "0") == 2)
                return n1;

            byte[] a = null;
            byte[] b = null;
            bool toneg = false;

            if (IsNegative(n1) == true && IsNegative(n2) == false)
            {
                n1 = Negate(n1);

                return Negate(Add(n2, n1));
            }
            else if (IsNegative(n1) == false && IsNegative(n2) == true)
            {
                n2 = Negate(n2);

                return Add(n1, n2);
            }
            else if (IsNegative(n1) == true && IsNegative(n2) == true)
            {
                n1 = Negate(n1);
                n2 = Negate(n2);

                return (Compare(n2, n1) == 0) ? Subtract(n2, n1) : Negate(Subtract(n1, n2));
            }
            else
            {
                if (Compare(n2, n1) == 0)
                    toneg = true;
            }

            //JESLI n1 WIEKSZE
            if (Compare(n1, n2) == 0)
            {
                a = new byte[n1.Length];
                b = new byte[n1.Length];
                n2 = new String('0', n1.Length - n2.Length) + n2;

                for (int j = 0; j < n1.Length; j++)
                {
                    a[j] = Convert.ToByte(Char.GetNumericValue(n1[j]));
                    b[j] = Convert.ToByte(Char.GetNumericValue(n2[j]));
                }
            }

            //JESLI n2 WIEKSZE
            else if (Compare(n1, n2) == 1)
            {
                a = new byte[n2.Length];
                b = new byte[n2.Length];
                n1 = new String('0', n2.Length - n1.Length) + n1;

                for (int j = 0; j < n1.Length; j++)
                {
                    a[j] = Convert.ToByte(Char.GetNumericValue(n2[j]));
                    b[j] = Convert.ToByte(Char.GetNumericValue(n1[j]));
                }
            }

            //JESLI IDENTYCZNE
            else if (Compare(n1, n2) == 2)
                return "0";

            int al = a.Length - 1;
            int bl = b.Length - 1;
            int i = 0;
            byte ac = 0;
            byte bc = 0;
            string c = "";
            string cd = "";

            while (i < a.Length)
            {
                ac = a[al - i];
                bc = b[bl - i];

                if (ac - bc < 0)
                {
                    for (int j = al - i - 1; j >= 0; j--)
                    {
                        if (a[j] == 0)
                            a[j] = 9;
                        else
                        {
                            a[j] -= 1;
                            break;
                        }
                    }

                    c += (ac - bc + 10).ToString();
                }
                else
                    c += ac - bc;

                i++;
            }

            for (int j = c.Length - 1; j >= 0; j--)
                cd += c[j];

            return (toneg) ? Negate(TrimZero(cd)) : TrimZero(cd);
        }
        public static string Multiply(string a, string b)
        {
            if ((IsNegative(a) == true && IsNegative(b) == false) || (IsNegative(a) == false && IsNegative(b) == true))
                return (IsNegative(a) == true) ? Negate(Multiply(Negate(a), b)) : Negate(Multiply(a, Negate(b)));
            else if (IsNegative(a) == true && IsNegative(b) == true)
            {
                a = Negate(a);
                b = Negate(b);
            }

            int ac = 0;
            int bc = 0;
            int w = 0;
            int s = 0;
            int al = a.Length - 1;
            int bl = b.Length - 1;
            string res = "0";
            string resin = "";
            string buff = "";

            for (int i = bl; i >= 0; i--)
            {
                buff = "";
                s = 0;

                for (int j = al; j >= 0; j--)
                {
                    ac = Convert.ToByte(Char.GetNumericValue(a[j]));
                    bc = Convert.ToByte(Char.GetNumericValue(b[i]));

                    w = (ac * bc + s) % 10;
                    s = (ac * bc + s) / 10;
                    buff += w.ToString();
                }

                buff += s.ToString();

                resin = Reverse(buff);

                for (int k = 0; k < bl - i; k++)
                    resin += '0';

                res = Add(res, resin);
            }

            return TrimZero(res);
        }
        public static string Factorial(string a)
        {
            if (Compare(a, "1") != 0)
                return "1";

            string result = "1";
            string i;

            for (i = "1"; Compare(i, Add(a, "1")) == 1; i = Add(i, "1"))
                result = Multiply(result, i);

            return TrimZero(result);
        }
        public static string Power(string a, string b)
        {
            string result = "1";

            for (string i = "0"; Compare(i, b) == 1; i = Add(i, "1"))
                result = Multiply(result, a);

            return TrimZero(result);
        }
        public static string SquareRoot(string s, int a)
        {
            string buff = "";
            string output = "";
            int n = 0;
            int c = 0;

            if (s.Length % 2 == 1)
                s = s.Insert(0, "0");

            for (int i = 0; i < a; i++)
                s += "00";

            string[] pairs = new string[s.Length / 2];

            for (int i = 0; i < pairs.Length * 2; i += 2)
            {
                pairs[i / 2] = s[i].ToString();
                pairs[i / 2] += s[i + 1].ToString();
            }

            int j = 9;

            for (j = 9; j >= 0; j--)
            {
                if (Compare(Power(j.ToString(), 2.ToString()), pairs[0]) == 1 || Compare(Power(j.ToString(), 2.ToString()), pairs[0]) == 2)
                    break;
            }

            n = j;
            output = n.ToString();

            if (pairs.Length == 1)
                return output;

            buff = Subtract(pairs[0], Power(n.ToString(), "2"));

            int k = 0;

            //
            for (int i = 1; i < pairs.Length; i++)
            {
                if (i == pairs.Length - a)
                    c = i;

                buff += pairs[i];
                string q = "";
                string m = "";

                for (k = 9; k >= 0; k--)
                {
                    m = Multiply(output, "2") + k.ToString();
                    q = Multiply(m, k.ToString());

                    if (Compare(q, buff) != 0)
                        break;
                }

                buff = Subtract(buff, q);
                output += k.ToString();
            }

            output = output.Insert(c, ".");

            return TrimZero(output);
        }
        public static string DecToBin(string a)
        {
            if (a == "0" || a == "")
                return "0";

            int i = 0;

            for (; ; i++)
            {
                if (Compare(a, Power("2", i.ToString())) == 1)
                    break;
            }

            i -= 1;

            string[] pows = new string[i + 1];

            for (int j = 0; j <= i; j++)
                pows[j] = Power("2", (i - j).ToString());

            string sum = "0";
            string output = "";

            foreach(string pow in pows)
            {
                if (Compare(Add(sum, pow), a) == 1 || Compare(Add(sum, pow), a) == 2)
                {
                    sum = Add(sum, pow);
                    output += "1";
                }
                else
                    output += "0";
            }

            return output;
        }
        public static string ParseRPN(string s)
        {
            string[] words = s.Split(' ');
            Stack stack = new Stack();
            string el = "";
            string output = "";
            int i = 0;

            for (int j = 0; j < words.Length; j++)
            {
                if (words[j][words[j].Length - 1] == '!')
                {
                    words[j] = words[j].Remove(words[j].Length - 1);
                    words[j] = Factorial(words[j]);
                }
                else if (words[j][0] == '√')
                {
                    words[j] = words[j].Substring(1);
                    words[j] = SquareRoot(words[j], 2);
                }
            }

            while (i < words.Length)
            {
                el = words[i];

                if (el == "")
                    break;

                else if (IsNum(el) == true)
                {
                    output += el + " ";
                    i++;
                    continue;
                }
                else if (el == "(")
                {
                    stack.Push("(");
                    i++;
                    continue;
                }
                else if (el == ")")
                {
                    while (stack.Peek().ToString() != "(")
                        output += stack.Pop() + " ";

                    stack.Pop();
                    i++;
                    continue;
                }
                else
                {
                    if (stack.Count != 0)
                    {
                        while (Priority(el) <= Priority(stack.Peek().ToString()))
                        {
                            output += stack.Pop() + " ";

                            if (stack.Count == 0)
                                break;
                        }
                    }

                    stack.Push(el);

                    i++;
                    continue;
                }
            }

            while (stack.Count != 0)
            {
                output += stack.Pop() + " ";
            }

            return (output[output.Length - 1] == ' ') ? output.Remove(output.Length - 1) : output;
        }
        public static string ParseRPN(string s, int a)
        {
            string[] words = s.Split(' ');
            Stack stack = new Stack();
            string el = "";
            string output = "";
            int i = 0;

            for (int j = 0; j < words.Length; j++)
            {
                if (words[j][words[j].Length - 1] == '!')
                {
                    words[j] = words[j].Remove(words[j].Length - 1);
                    words[j] = Factorial(words[j]);
                }
                else if (words[j][0] == '√')
                {
                    words[j] = words[j].Substring(1);
                    words[j] = SquareRoot(words[j], a);
                }
            }

            while (i < words.Length)
            {
                el = words[i];

                if (el == "")
                    break;

                else if (IsNum(el) == true)
                {
                    output += el + " ";
                    i++;
                    continue;
                }
                else if (el == "(")
                {
                    stack.Push("(");
                    i++;
                    continue;
                }
                else if (el == ")")
                {
                    while (stack.Peek().ToString() != "(")
                        output += stack.Pop() + " ";

                    stack.Pop();
                    i++;
                    continue;
                }
                else
                {
                    if (stack.Count != 0)
                    {
                        while (Priority(el) <= Priority(stack.Peek().ToString()))
                        {
                            output += stack.Pop() + " ";

                            if (stack.Count == 0)
                                break;
                        }
                    }

                    stack.Push(el);

                    i++;
                    continue;
                }
            }

            while (stack.Count != 0)
            {
                output += stack.Pop() + " ";
            }

            return (output[output.Length - 1] == ' ') ? output.Remove(output.Length - 1) : output;
        }
        public static string CalcRPN(string s)
        {
            Stack stack = new Stack();
            string[] words = s.Split(' ');
            string el = "";
            string a = "";
            string b = "";
            string w = "";
            int i = 0;

            while (i < words.Length)
            {
                el = words[i];

                if (el == "")
                    break;

                else if (IsNum(el) == true)
                    stack.Push(el);

                else if (stack.Count >= 2)
                {
                    b = stack.Pop().ToString();
                    a = stack.Pop().ToString();

                    if (el == "^") w = Power(a, b);
                    else if (el == "*") w = Multiply(a, b);
                    else if (el == "/") w = (Double.Parse(a) / Double.Parse(a)).ToString();
                    else if (el == "%") w = (Int32.Parse(a) % Int32.Parse(a)).ToString();
                    else if (el == "+") w = Add(a, b);
                    else if (el == "-") w = Subtract(a, b);

                    stack.Push(w);
                }

                i++;
            }

            return stack.Pop().ToString();
        }
        public static int Compare(string n1, string n2)
        {
            if (IsNegative(n1) == true && IsNegative(n2) == false)
                return 1;
            else if (IsNegative(n1) == false && IsNegative(n2) == true)
                return 0;

            if (n1.Length > n2.Length)
            {
                n2 = (IsNegative(n2) == false) ? new String('0', n1.Length - n2.Length) + n2 : Negate(new String('0', n1.Length - n2.Length) + Negate(n2));
            }
            else if (n1.Length < n2.Length)
            {
                n1 = (IsNegative(n1) == false) ? new String('0', n2.Length - n1.Length) + n1 : Negate(new String('0', n2.Length - n1.Length) + Negate(n1));
            }

            int i = (IsNegative(n1) && IsNegative(n2)) ? 1 : 0;

            while (i < n1.Length)
            {
                if (Char.GetNumericValue(n1[i]) > Char.GetNumericValue(n2[i]))
                    return (IsNegative(n1) && IsNegative(n2)) ? 1 : 0;
                else if (Char.GetNumericValue(n1[i]) < Char.GetNumericValue(n2[i]))
                    return (IsNegative(n1) && IsNegative(n2)) ? 0 : 1;
                else
                    i++;
            }

            if (i == n1.Length && IsNegative(n1) == false)
                return 2;
            else if (i == n1.Length && IsNegative(n2) == true)
                return 2;

            return 3;
        }
        public static bool IsNum(string s)
        {
            if (s == "" || s == "-")
                return false;

            int i = (IsNegative(s)) ? 1 : 0;

            while (i < s.Length)
            {
                if (Char.IsNumber(s[i]) == true || s[i] == '.')
                {
                    i++;
                    continue;
                }
                else
                    break;
            }

            return ((i == s.Length - 1 && IsNegative(s)) || (i == s.Length)) ? true : false;
        }
        public static bool IsNegative(string a)
        {
            if (a[0] == '-')
                return true;
            else
                return false;

        }
        private static string Negate(string a)
        {
            if (a == "0" || a == "-0")
                return "0";
            else if (a[0] == '-')
                return a.Trim('-');
            else
                return a.Insert(0, "-");
        }
        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        public static string TrimZero(string a)
        {
            bool aisneg = false;
            bool f = false;

            for (int k = 0; k < a.Length; k++)
            {
                if (a[k] == '.')
                    f = true;
            }

            if (a[0] == '-')
            {
                aisneg = true;
                a = Negate(a);
            }
            else
                aisneg = false;

            StringBuilder asb = new StringBuilder(a);
            int i = 0;

            for (i = 0; i < asb.Length; i++)
            {
                if (asb[i] == '0')
                    continue;
                else
                    break;
            }

            if (i == asb.Length)
                return "0";

            asb = asb.Remove(0, i);

            if (f == true)
            {
                int j = 0;

                for (j = asb.Length - 1; j >= 0; j--)
                {
                    if (asb[j] != '.' && asb[j] != '0')
                        break;
                    else
                        asb = asb.Remove(asb.Length - 1, 1);
                }
            }
            //asb = asb.Remove(asb.Length - j, j);

            //if (asb[asb.Length - 1] == '.')
            //    asb = asb.Remove(asb.Length - 1, 1);

            return (aisneg) ? Negate(asb.ToString()) : asb.ToString();
        }
        private static byte Priority(string s)
        {
            if (s == "^")
                return 3;
            else if (s == "*" || s == "/" || s == "%")
                return 2;
            else if (s == "+" || s == "-")
                return 1;
            else
                return 0;
        }
        public static bool IsFloat(string a)
        {
            bool f = false;

            for (int k = 0; k < a.Length; k++)
            {
                if (a[k] == '.')
                    f = true;
            }

            return f;
        }
    }
}
