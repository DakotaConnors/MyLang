using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Parser
{
    class Program
    {
        static bool isNumber(char a)
        {
            int num = Convert.ToInt32(a);
            bool isNum = false;
            if (num >= 48 && num <= 57) isNum = true;
            return isNum;
        }

        static bool isLetter(char a)
        {
            int let = Convert.ToInt32(a);
            bool isLet = false;
            if ((let >= 65 && let <= 90) || (let >= 97 && let <= 122)) isLet = true;
            return isLet;
        }

        static bool isSymbol(char a)
        {
            int sym = Convert.ToInt32(a);
            bool isSym = false;
            if (sym == 95 || sym == 36) isSym = true;
            return isSym;
        }

        static bool isOperator(char a)
        {
            int op = Convert.ToInt32(a);
            bool isOp = false;
            if (op == 42 || op == 43 || op == 45 || op == 47 || op == 61) isOp = true;
            return isOp;
        }

        static string CheckToken(string token)
        {
            string tokenType = "INVALID TOKEN";
            if (token == "START" || token == "STOP") return "COMMAND";
            else if (token == "EOL") return "END OF LINE";
            else if (token == "int" || token == "string") return "DATA TYPE";
            else
            {
                //checking id it's an operator
                if (token.Length == 1 && isOperator(token[0])) return "OPERATOR";

                //checking if it's a number
                bool num = true;
                foreach (char c in token)
                {
                    if (!isNumber(c)) num = false;
                }
                if (num) return "NUMBER";

                //checking if it's a valid identifier
                bool firstLet = true;
                bool restOfString = true;
                if (!isLetter(token[0])) firstLet = false;
                for (int i = 1; i < token.Length; i++)
                {
                    if (!isLetter(token[i]) && !isNumber(token[i]) && !isSymbol(token[i])) restOfString = false;
                }
                if (firstLet && restOfString) return "INDENTIFIER";
                return "INVALID TOKEN";
            }
            return tokenType;
        }

        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader("input.txt");
            StreamWriter tokenWriter = new StreamWriter("tokens.txt");
            List<string> tokens = new List<string>();

            string line = "";
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                string temp = "";
                int i = 0;
                while (i < line.Length)
                {
                    while (i < line.Length && line[i] != ' ')
                    {
                        temp += line[i];
                        i++;
                    }
                    i++;
                    tokens.Add(temp);
                    temp = "";
                }
                tokens.Add("EOL");
            }

            foreach (string s in tokens)
            {
                tokenWriter.WriteLine(s + " : " + CheckToken(s));
                tokenWriter.Flush();
            }
            Console.ReadKey();
        }
    }
}
