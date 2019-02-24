using JetBrains.Annotations;
using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Implementation.Evaluators.Base;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [ParseChildren]
    [UsedImplicitly]
    [DoNotParseForParagraphs]
    public class TableRowEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.TableRow;

        public EvaluatorResult Evaluate(string source)
        {
            return null;
        }
    }
}