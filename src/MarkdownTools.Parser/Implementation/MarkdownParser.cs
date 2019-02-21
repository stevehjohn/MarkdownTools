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

                              return attribute?.Precedence ?? int.MaxValue - 2;
                          }).ToList();
        }

        public Node Parse(string markdown)
        {
            var root = new Node
                       {
                           Type = NodeType.Root
                       };

            Parse(root, markdown);
            
            return root;
        }

        private void Parse(Node parent, string content)
        {
            Node previousNode = null;

            while (! string.IsNullOrEmpty(content))
            {
                var parsed = false;

                foreach (var evaluator in _evaluators)
                {
                    var result = evaluator.Evaluate(content);

                    if (result != null)
                    {
                        parsed = true;
                        content = result.EvaluateNext;

                        var node = result.Node;
                        if (node.Type == NodeType.Text && previousNode != null && previousNode.Type == NodeType.Text)
                        {
                            previousNode.Content = $"{previousNode.Content}{node.Content}";
                            break;
                        }

                        parent.Children.Add(node);
                        if (result.ParseContent)
                        {
                            Parse(node, node.Content);
                        }

                        previousNode = node;
                        break;
                    }
                }

                if (! parsed)
                {
                    throw new ParserException("Unable to find evaluator to handle content", content);
                }
            }
        }
    }
}