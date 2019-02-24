using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [InlineElement]
    [Precedence(int.MaxValue)]
    public class TextEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.Text;

        public EvaluatorResult Evaluate(string source)
        {
            return new EvaluatorResult(
                new Node
                {
                    Type = NodeType.Text,
                    Content = source[0].ToString()
                },
                source.SafeSubstring(1));
        }
    }
}