using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MarkdownTools.Parser.Models
{
    public class Node
    {
        public NodeType Type { get; }
        public IReadOnlyDictionary<string, string> MetaData { get; }
        public string Content { get; }
        public IReadOnlyList<Node> Children => new ReadOnlyCollection<Node>(_children);

        private readonly IList<Node> _children = new List<Node>();

        public Node(NodeType type, IDictionary<string, string> metadata = null, string content = null)
        {
            Type = type;
            MetaData = new ReadOnlyDictionary<string, string>(metadata ?? new Dictionary<string, string>());
            Content = content;
        }

        internal void AddChild(Node node)
        {
            _children.Add(node);
        }
    }
}