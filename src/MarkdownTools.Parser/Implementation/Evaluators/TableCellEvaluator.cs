using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Implementation.Evaluators.Base;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [DoNotParseForParagraphs]
    [ParseContent]
    [ValidChildNodes(NodeType.Image, NodeType.InlineCode, NodeType.Italic, NodeType.LineBreak, NodeType.Link, NodeType.Strikethrough, NodeType.Strong, NodeType.Text, NodeType.Whitespace)]
    public class TableCellEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.TableCell;

        public EvaluatorResult Evaluate(string source)
        {
            return null;
        }
    }
}