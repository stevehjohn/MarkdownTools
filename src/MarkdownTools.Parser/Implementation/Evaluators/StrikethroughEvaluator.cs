using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [InlineElement]
    [DoNotParseForParagraphs]
    public class StrikethroughEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.Strikethrough;

        public EvaluatorResult Evaluate(string source)
        {
            if (source.StartsWith("~~"))
            {
                var line = source.GetLine();

                if (line.Length == 2)
                {
                    return null;
                }

                var end = line.SafeSubstring(2)?.IndexOf("~~") ?? -1;

                if (end < 0)
                {
                    return null;
                }

                return new EvaluatorResult(
                    new Node
                    {
                        Type = NodeType.Strikethrough,
                        Content = source.SafeSubstring(2, end).Trim(),
                        RawContent = source.Substring(2, end)
                    },
                    source.SafeSubstring(end + 4));
            }

            return null;
        }
    }
}