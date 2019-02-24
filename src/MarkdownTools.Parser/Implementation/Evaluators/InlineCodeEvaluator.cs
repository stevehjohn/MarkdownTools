using MarkdownTools.Models;
using MarkdownTools.Parser.Implementation.Evaluators.Base;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    public class InlineCodeEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.InlineCode;

        public EvaluatorResult Evaluate(string source)
        {
            return null;
        }
    }
}