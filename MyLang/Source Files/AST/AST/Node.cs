using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AST
{
    class Node
    {
        public string type;
        public string name;
        public List<Node> children;

        public Node()
        {
            type = "";
            name = "";
            children = null;
        }
    }
}
