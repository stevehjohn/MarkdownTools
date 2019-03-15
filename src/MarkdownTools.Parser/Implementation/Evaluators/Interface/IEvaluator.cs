using MarkdownTools.Models;

namespace MarkdownTools.Parser.Implementation.Evaluators.Base
{
    public interface IEvaluator
    {
        NodeType IsEvaluatorFor { get; }

        EvaluatorResult Evaluate(string source);
    }
}