using MarkdownTools.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MarkdownTools.TreeToHtml.Implementation
{
    public class NodeTreeWalker : INodeTreeWalker
    {
        private const int Indentation = 4;

        private Node _root;

        private string _title;

        public void LoadTree(Node root)
        {
            _root = root;
        }

        public string ToHtml(Theme theme, string customThemePath = null)
        {
            var builder = new StringBuilder();

            ProcessNodes(_root.Children, builder, 1);

            var html = builder.ToString();

            string template;

            switch (theme)
            {
                case Theme.Dark:
                    template = LoadThemeFromResource("MarkdownTools.TreeToHtml.Templates.Dark.html");
                    break;
                case Theme.Light:
                    template = LoadThemeFromResource("MarkdownTools.TreeToHtml.Templates.Light.html");
                    break;
                case Theme.Raw:
                    return html;
                default:
                    if (string.IsNullOrWhiteSpace(customThemePath))
                    {
                        throw new ArgumentNullException(nameof(customThemePath));
                    }
                    template = File.ReadAllText(customThemePath);
                    break;
            }

            return template.Replace("{title}", _title).Replace("{body}", html);
        }

        private string LoadThemeFromResource(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(name))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private void ProcessNodes(IEnumerable<Node> nodes, StringBuilder builder, int level)
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
                        if (string.IsNullOrWhiteSpace(_title))
                        {
                            _title = node.Content;
                        }
                        ProcessHeadingNode(node, builder, level);
                        break;
                    case NodeType.HorizontalRule:
                        ProcessHorizontalRuleNode(builder, level);
                        break;
                    case NodeType.Newline:
                        ProcessNewlineNode(builder);
                        break;
                    case NodeType.Paragraph:
                        ProcessParagraphNode(node, builder, level);
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

        private void ProcessBlockquoteNode(Node node, StringBuilder builder, int level)
        {
            if (node.Children.Any())
            {
                builder.AppendLine($"{new string(' ', level * Indentation)}<blockquote>");
                ProcessNodes(node.Children, builder, level + 1);
                builder.AppendLine($"{new string(' ', level * Indentation)}</blockquote>");
            }
            else
            {
                builder.AppendLine($"{new string(' ', level * Indentation)}<blockquote>");
                builder.AppendLine($"{new string(' ', (level + 1) * Indentation)}{node.Content}");
                builder.AppendLine($"{new string(' ', level * Indentation)}</blockquote>");
            }
        }

        private static void ProcessCodeBlockNode(Node node, StringBuilder builder, int level)
        {
            builder.Append($"{new string(' ', level * Indentation)}<code>");
            builder.Append(node.Content.Trim());
            builder.AppendLine("</code>");
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

        private void ProcessParagraphNode(Node node, StringBuilder builder, int level)
        {
            builder.AppendLine($"{new string(' ', level * Indentation)}<p>");
            ProcessNodes(node.Children, builder, level + 1);
            builder.AppendLine($"{new string(' ', level * Indentation)}</p>");
        }

        private static void ProcessTextNode(Node node, StringBuilder builder)
        {
            // TODO: Indent when first in sequence?
            builder.Append(node.Content);
        }

        private static void ProcessWhitespaceNode(StringBuilder builder)
        {
            builder.Append(" ");
        }
    }
}