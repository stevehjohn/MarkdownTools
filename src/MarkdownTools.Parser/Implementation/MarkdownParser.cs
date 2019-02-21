using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using MarkdownTools.Parser.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MarkdownTools.Parser.Implementation
{
    public class MarkdownParser : IMarkdownParser
    {
        private readonly IList<IEvaluator> _evaluators;

        public IReadOnlyList<IEvaluator> Evaluators => new ReadOnlyCollection<IEvaluator>(_evaluators);

        public MarkdownParser(IEnumerable<IEvaluator> evaluators)
        {
            _evaluators = evaluators
                          .OrderBy(e =>
                          {
                              var attribute = Attribute.GetCustomAttribute(e.GetType(), typeof(PrecedenceAttribute)) as PrecedenceAttribute;

                              return attribute?.Precedence ?? int.MaxValue;
                          }).ToList();
        }

        public Node Parse(string markdown)
        {
            var root = new Node(NodeType.Root);

            Parse(root, markdown);

            return root;
        }

        private void Parse(Node parent, string content)
        {
            while (! string.IsNullOrWhiteSpace(content))
            {
                foreach (var evaluator in _evaluators)
                {
                    var result = evaluator.Evaluate(content);

                    if (result != null)
                    {
                        var node = result.Node;

                        parent.AddChild(node);
                        content = result.EvaluateNext;
                        if (result.ParseContent)
                        {
                            Parse(node, node.Content);
                        }

                        break;
                    }
                }

                // TODO: Throw?
            }
        }
    }
}