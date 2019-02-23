using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Implementation.Evaluators.Base;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [DoNotParseForParagraphs]
    [ValidChildNodes(NodeType.Text, NodeType.Whitespace)]
    public class TableCellHeadEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.TableCellHead;

        public EvaluatorResult Evaluate(string source)
        {
            return null;
        }
    }
}