namespace MarkdownTools.Parser.Implementation.Evaluators
{
    public interface IEvaluator
    {
        EvaluatorResult Evaluate(string source);
    }
}