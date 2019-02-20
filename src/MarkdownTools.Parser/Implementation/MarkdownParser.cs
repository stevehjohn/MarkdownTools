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

            //_evaluators = new List<IEvaluator>();

            //var evaluators = Assembly.GetAssembly(typeof(MarkdownParser))
            //                         .GetTypes()
            //                         .Where(t => t.IsAssignableFrom(typeof(IEvaluator)) && ! t.IsInterface)
            //                         .ToList();

            //evaluators.ForEach(e => _evaluators.Add((IEvaluator) Activator.CreateInstance(e)));
        }

        public Node Parse(string markdown)
        {
            _root = new Node(NodeType.Root);

            while (! string.IsNullOrEmpty(markdown))
            {
                foreach (var evaluator in _evaluators)
                {
                    var result = evaluator.Evaluate(markdown);

                    if (result != null)
                    {
                        markdown = result.EvaluateNext;
                        break;
                    }
                }
            }

            return _root;
        }
    }
}