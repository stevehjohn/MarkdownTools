using MarkdownTools.Models;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    public class LineBreakEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.LineBreak;

        public EvaluatorResult Evaluate(string source)
        {
            if (source.StartsWith("<br>") || source.StartsWith("<br >") || source.StartsWith("<br/>") || source.StartsWith("<br />"))
            {
                return new EvaluatorResult(
                    new Node
                    {
                        Type = NodeType.LineBreak
                    }, 
                    source.SafeSubstring(source.IndexOf('>') + 1));
            }

            return null;
        }
    }
}