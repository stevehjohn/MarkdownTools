using MarkdownTools.Models;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    public class TableEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.Table;

        public EvaluatorResult Evaluate(string source)
        {
            if (source.StartsWith("|"))
            {
                var next = source.NextLine();
            }

            return null;
        }
    }
}