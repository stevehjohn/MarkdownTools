using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Implementation.Evaluators.Base;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [ParseChildren]
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