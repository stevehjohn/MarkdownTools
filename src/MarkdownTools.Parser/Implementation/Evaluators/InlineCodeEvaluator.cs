using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [InlineElement]
    [DoNotParseForParagraphs]
    public class InlineCodeEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.InlineCode;

        public EvaluatorResult Evaluate(string source)
        {
            if (source.StartsWith("`"))
            {
                var line = source.GetLine();

                if (line.Length == 1)
                {
                    return null;
                }

                if (line[1] != '`')
                {
                    var end = line.SafeSubstring(1)?.IndexOf('`') ?? -1;

                    if (end < 0)
                    {
                        return null;
                    }

                    return new EvaluatorResult(
                        new Node
                        {
                            Type = NodeType.InlineCode,
                            Content = source.SafeSubstring(1, end).Trim(),
                            RawContent = source.Substring(1, end)
                        },
                        source.SafeSubstring(end + 2));
                }
            }

            return null;
        }
    }
}