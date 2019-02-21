using System.Collections.Generic;

namespace MarkdownTools.Parser.Models
{
    public class Node
    {
        public NodeType Type { get; set; }
        public IDictionary<string, string> MetaData { get; set; }
        public string Content { get; set; }
        public IList<Node> Children { get; }

        public Node()
        {
            Children = new List<Node>();
        }
    }
}