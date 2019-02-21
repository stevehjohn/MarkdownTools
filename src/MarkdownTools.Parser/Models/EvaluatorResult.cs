using MarkdownTools.Models;

namespace MarkdownTools.Parser.Models
{
    public class EvaluatorResult
    {
        public Node Node { get; }
        public string EvaluateNext { get; }
        public bool ParseContent { get; }

        public EvaluatorResult(Node node, string evaluateNext, bool parseContent = false)
        {
            Node = node;
            EvaluateNext = evaluateNext;
            ParseContent = parseContent;
        }
    }
}