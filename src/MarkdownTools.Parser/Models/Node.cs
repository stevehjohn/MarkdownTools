using System.Collections.Generic;

namespace MarkdownTools.Parser.Models
{
    public class Node
    {
        private readonly List<Node> _children;

        public NodeType Type { get; }
        public IReadOnlyDictionary<string, string> MetaData { get; }
        public IReadOnlyList<Node> Children => _children.AsReadOnly();

        public Node(NodeType type, IReadOnlyDictionary<string, string> metadata = null)
        {
            Type = type;
            MetaData = metadata;

            _children = new List<Node>();
        }

        internal void AddChild(Node child)
        {
            _children.Add(child);
        }
    }
}