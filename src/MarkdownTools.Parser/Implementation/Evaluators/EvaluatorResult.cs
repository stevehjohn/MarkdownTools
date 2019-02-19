using MarkdownTools.Parser.Models;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    public class EvaluatorResult
    {
        public Node Node { get; }
        public int SkipAhead { get; }

        public EvaluatorResult(Node node, int skipAhead)
        {
            Node = node;
            SkipAhead = skipAhead;
        }
    }
}