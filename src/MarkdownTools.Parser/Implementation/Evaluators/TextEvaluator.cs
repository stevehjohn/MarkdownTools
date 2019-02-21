using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using MarkdownTools.Parser.Models;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [Precedence(int.MaxValue)]
    public class TextEvaluator : IEvaluator
    {
        public EvaluatorResult Evaluate(string source)
        {
            return new EvaluatorResult(
                new Node(NodeType.Text, null, source[0].ToString()), 
                source.SafeSubstring(1));
        }
    }
}