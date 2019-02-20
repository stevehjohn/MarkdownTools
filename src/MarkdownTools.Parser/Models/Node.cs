using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MarkdownTools.Parser.Models
{
    public class Node
    {
        public NodeType Type { get; }
        public IReadOnlyDictionary<string, string> MetaData { get; }
        public string Content { get; }
        public Node PreviousNode { get; internal set; }
        public Node NextNode { get; internal set; }
        public Node FirstChild { get; internal set; }

        public Node(NodeType type, IDictionary<string, string> metadata = null, string content = null)
        {
            Type = type;
            MetaData = new ReadOnlyDictionary<string, string>(metadata ?? new Dictionary<string, string>());
            Content = content;
        }

        internal void SetPreviousNode(Node node)
        {
            NextNode = node;
        }

        internal void SetNextNode(Node node)
        {
            NextNode = node;
        }

        internal void SetFirstChild(Node node)
        {
            FirstChild = node;
        }
    }
}