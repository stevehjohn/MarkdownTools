using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [InlineElement]
    [ValidChildNodes]
    public class ItalicEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.Italic;

        public EvaluatorResult Evaluate(string source)
        {
            if (source.StartsWith("*") || source.StartsWith("_"))
            {
                var line = source.GetLine();

                if (line.Length == 1)
                {
                    return null;
                }

                var end = line.IndexOf(source[0], 1);

                if (end == -1 || end == 1)
                {
                    return null;
                }

                return new EvaluatorResult(
                    new Node
                    {
                        Type = NodeType.Italic,
                        Content = line.Substring(1, end - 1)
                    }, 
                    source.SafeSubstring(end + 1));
            }

            return null;
        }
    }
}