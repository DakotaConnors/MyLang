using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AST
{
    class Program
    {
        static void readTokens(StreamReader reader, List<string> Tokens, List<string> tokenType)
        {
            string line = "";
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                int i = 0;
                string tempToken = "";
                while(line[i] != ' ')
                {
                    tempToken += line[i];
                    i++;
                }
                i+=3;

                string tempTokenType = "";

                while(i < line.Length)
                {
                    tempTokenType += line[i];
                    i++;
                }
                Tokens.Add(tempToken);
                tokenType.Add(tempTokenType);
            }
        }

        static Node AddNode(string token, string tokenType, Node root, int lineNumber)
        {
            Node tempNode = new Node();

            if (root.name == "START" && lineNumber - 1 == root.children.Count)
            {
                //Console.WriteLine("Adding Token " + token);
                tempNode.name = token;
                tempNode.type = tokenType;
                root.children.Add(tempNode);
            }

            else if (root.children.Count == 0)
            {
                if (token == "EOL")
                {
                    //Console.WriteLine("End of line");
                }
                //Console.WriteLine("Adding Token " + token);
                tempNode.name = token;
                tempNode.type = tokenType;
                root.children.Add(tempNode);
            }

            else
            {
                {
                    if (root.children[root.children.Count - 1].children == null) root.children[root.children.Count - 1].children = new List<Node>();
                    root.children[root.children.Count - 1] = AddNode(token, tokenType, root.children[root.children.Count - 1], lineNumber);
                }
            }
            return root;
        }

        static void PrintTree(Node node, string format)
        {
            Console.WriteLine(format + node.name);
            if (node.children != null)
                if (node.children.Count != 0) PrintTree(node.children[0], format + "--");
        }

        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader("tokens.txt");
            List<string> tokens = new List<string>();
            List<string> tokenType = new List<string>();
            Node root = new Node();
            

            readTokens(reader, tokens, tokenType);

            int lineTracker = 0;

            //Console.WriteLine(tokens.Count);
            for (int i = 0; i < tokens.Count; i++)
            {
                //Console.WriteLine(tokens[i] + " " + tokenType[i]);
                if (tokens[i] == "START" && lineTracker == 0)
                {
                    //Console.WriteLine("Found Start Node");
                    root.name = tokens[i];
                    root.type = tokenType[i];
                    root.children = new List<Node>();
                    lineTracker++;
                }

                else
                {
                    root = AddNode(tokens[i], tokenType[i], root, lineTracker);
                }
                if (tokens[i] == "EOL") lineTracker++;
            }
            foreach (Node n in root.children)
            {
                PrintTree(n, "--");
            }

            Console.ReadKey();
        }
    }
}
 