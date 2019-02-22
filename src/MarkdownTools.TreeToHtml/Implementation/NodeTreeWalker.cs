using MarkdownTools.Models;
using System.Collections.Generic;

namespace MarkdownTools.TreeToHtml.Implementation
{
    public class NodeTreeWalker : INodeTreeWalker
    {
        private Node _root;

        public void LoadTree(Node root)
        {
            _root = root;
        }

        public string ToHtml(Theme theme, string customThemePath = null)
        {
            return ProcessNodes(_root.Children);
        }

        private static string ProcessNodes(IEnumerable<Node> nodes)
        {
            return null;
        }
    }
}