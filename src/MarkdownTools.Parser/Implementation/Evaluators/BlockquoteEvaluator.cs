using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using System;
using System.Text;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [ParseContent]
    [DoNotParseForParagraphs]
    [ValidPreviousNodeSequence(NodeType.Newline)]
    [ValidPreviousNodeSequence(NodeType.Newline, NodeType.Whitespace)]
    public class BlockquoteEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.Blockquote;

        public EvaluatorResult Evaluate(string source)
        {
            var quote = new StringBuilder();
            var rawQuote = new StringBuilder();

            while (!string.IsNullOrEmpty(source) && (source.StartsWith(">") || source.StartsWith(" >") || source.StartsWith("  >") || source.StartsWith("   >")))
            {
                source = source.SafeSubstring(source.IndexOf('>') + 1);

                var eol = source.IndexOf(Environment.NewLine, StringComparison.Ordinal);

                if (eol > -1)
                {
                    var text = source.SafeSubstring(0, eol).Trim();

                    if (string.IsNullOrWhiteSpace(text))
                    {
                        var nextLine = source.SafeSubstring(eol + Environment.NewLine.Length);

                        if (nextLine != null && (nextLine.StartsWith(">") || nextLine.StartsWith(" >") || nextLine.StartsWith("  >") || nextLine.StartsWith("   >")))
                        {
                            rawQuote.Append("<br><br>");
                        }
                    }
                    else
                    {
                        quote.Append($"{text} ");
                        rawQuote.AppendLine($"{source.SafeSubstring(0, eol)}");
                    }

                    source = source.SafeSubstring(eol + Environment.NewLine.Length);
                }
                else
                {
                    quote.Append(source.Trim());
                    rawQuote.AppendLine(source);
                    source = null;
                }
            }

            var blockquote = quote.ToString().Trim();

            if (blockquote.Length > 0)
            {
                return new EvaluatorResult(
                    new Node
                    {
                        Type = NodeType.Blockquote,
                        Content = blockquote,
                        RawContent = rawQuote.ToString()
                    },
                    source);
            }

            return null;
        }
    }
}