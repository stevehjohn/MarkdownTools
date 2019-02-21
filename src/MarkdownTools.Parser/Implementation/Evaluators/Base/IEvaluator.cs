namespace MarkdownTools.Parser.Implementation.Evaluators.Base
{
    public interface IEvaluator
    {
        EvaluatorResult Evaluate(string source);
    }
}