using MarkdownTools.Parser.Models;

namespace MarkdownTools.Parser.Implementation.Evaluators.Interface
{
    public interface IEvaluator
    {
        EvaluatorResult Evaluate(string source);

        int Precedence { get; }
    }
}