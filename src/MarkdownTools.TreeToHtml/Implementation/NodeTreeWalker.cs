using MarkdownTools.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkdownTools.TreeToHtml.Implementation
{
    public class NodeTreeWalker : INodeTreeWalker
    {
        private const int Indentation = 4;

        private Node _root;

        public void LoadTree(Node root)
        {
            _root = root;
        }

        public string ToHtml(Theme theme, string customThemePath = null)
        {
            var builder = new StringBuilder();

            ProcessNodes(_root.Children, builder, 1);

            return builder.ToString();
        }

        private static void ProcessNodes(IEnumerable<Node> nodes, StringBuilder builder, int level)
        {
            foreach (var node in nodes)
            {
                switch (node.Type)
                {
                    case NodeType.Blockquote:
                        ProcessBlockquoteNode(node, builder, level);
                        break;
                    case NodeType.CodeBlock:
                        ProcessCodeBlockNode(node, builder, level);
                        break;
                    case NodeType.Heading:
                        ProcessHeadingNode(node, builder, level);
                        break;
                    case NodeType.HorizontalRule:
                        ProcessHorizontalRuleNode(builder, level);
                        break;
                    case NodeType.Newline:
                        ProcessNewlineNode(builder);
                        break;
                    case NodeType.Whitespace:
                        ProcessWhitespaceNode(builder);
                        break;
                    default:
                        ProcessTextNode(node, builder);
                        break;
                }
            }
        }

        private static void ProcessBlockquoteNode(Node node, StringBuilder builder, int level)
        {
            if (node.Children.Any())
            {
                builder.Append($"{new string(' ', level * Indentation)}<blockquote>");
                ProcessNodes(node.Children, builder, level + 1);
                builder.Append($"{new string(' ', level * Indentation)}</blockquote>");
            }
        }

        private static void ProcessCodeBlockNode(Node node, StringBuilder builder, int level)
        {
            builder.AppendLine($"{new string(' ', level * Indentation)}<code>");
            builder.Append(node.Content);
            builder.AppendLine($"{new string(' ', level * Indentation)}</code>");
        }

        private static void ProcessHeadingNode(Node node, StringBuilder builder, int level)
        {
            builder.AppendLine($"{new string(' ', level * Indentation)}<h{node.MetaData["Level"]}>{node.Content}</h{node.MetaData["Level"]}>");
        }

        private static void ProcessHorizontalRuleNode(StringBuilder builder, int level)
        {
            builder.AppendLine($"{new string(' ', level * Indentation)}<hr>");
        }

        private static void ProcessNewlineNode(StringBuilder builder)
        {
            builder.AppendLine();
        }

        private static void ProcessWhitespaceNode(StringBuilder builder)
        {
            builder.Append(" ");
        }

        private static void ProcessTextNode(Node node, StringBuilder builder)
        {
            // TODO: Indent when first in sequence
            builder.Append(node.Content);
        }
    }
}