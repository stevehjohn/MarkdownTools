using MarkdownTools.Models;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    public class UnorderedListItemEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.UnorderedListItem;

        public EvaluatorResult Evaluate(string source)
        {
            if (source.StartsWith("* ") || source.StartsWith("- ") || source.StartsWith("+ "))
            {
                var line = source.GetLine();

                return new EvaluatorResult(
                    new Node
                    {
                        Type = NodeType.UnorderedListItem,
                        Content = line.Substring(1).Trim()
                    },
                    source.NextLine());
            }

            return null;
        }
    }
}