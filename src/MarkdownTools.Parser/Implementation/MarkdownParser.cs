using MarkdownTools.Parser.Implementation.Evaluators.Interface;
using MarkdownTools.Parser.Models;
using System.Collections.Generic;
using System.Linq;

namespace MarkdownTools.Parser.Implementation
{
    public class MarkdownParser : IMarkdownParser
    {
        private readonly IList<IEvaluator> _evaluators;

        private Node _root;

        public MarkdownParser(IEnumerable<IEvaluator> evaluators)
        {
            _evaluators = evaluators.ToList();
        }

        public Node Parse(string markdown)
        {
            _root = new Node(NodeType.Root);

            var previousNode = _root;

            while (! string.IsNullOrEmpty(markdown))
            {
                foreach (var evaluator in _evaluators)
                {
                    var result = evaluator.Evaluate(markdown);

                    if (result != null)
                    {
                        var node = result.Node;

                        node.SetPreviousNode(previousNode);
                        previousNode.SetNextNode(node);

                        markdown = result.EvaluateNext;
                        break;
                    }
                }
            }

            return _root;
        }
    }
}