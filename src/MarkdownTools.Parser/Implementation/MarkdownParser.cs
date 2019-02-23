using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
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

            PostProcessForParagraphs(root);
            
            return root;
        }

        private void Parse(Node parent, string content)
        {
            Node previousNode = null;

            while (! string.IsNullOrEmpty(content))
            {
                var validEvaluators = GetValidEvaluators(parent);

                foreach (var evaluator in validEvaluators)
                {
                    if (! CheckEvaluatorAttributes(parent, evaluator))
                    {
                        continue;
                    }

                    var result = evaluator.Evaluate(content);

                    if (result != null)
                    {
                        content = result.EvaluateNext;

                        var node = result.Node;
                        if (node.Type == NodeType.Text && previousNode != null && previousNode.Type == NodeType.Text)
                        {
                            previousNode.Content = $"{previousNode.Content}{node.Content}";
                            break;
                        }

                        parent.Children.Add(node);
                        if (Attribute.GetCustomAttribute(evaluator.GetType(), typeof(ParseContentAttribute)) != null)
                        {
                            Parse(node, node.RawContent);
                        }

                        previousNode = node;
                        break;
                    }
                }
            }
        }

        private void PostProcessForParagraphs(Node node)
        {
            if (node.Type != NodeType.Root)
            {
                var evaluator = _evaluators.FirstOrDefault(e => e.IsEvaluatorFor == node.Type);

                // ReSharper disable once UsePatternMatching
                // ReSharper disable once PossibleNullReferenceException
                var attribute = Attribute.GetCustomAttribute(evaluator.GetType(), typeof(DoNotParseForParagraphsAttribute)) as DoNotParseForParagraphsAttribute;

                if (attribute != null)
                {
                    return;
                }
            }

            var nodes = node.Children;

            if (nodes.Count == 0)
            {
                return;
            }

            nodes.ToList().ForEach(PostProcessForParagraphs);

            while (true)
            {
                var first = nodes.IndexOf(n => n.Type == NodeType.Text);

                if (first == -1)
                {
                    break;
                }

                var length = 1;

                while (first + length < nodes.Count 
                       && (nodes[first + length].Type == NodeType.Text || nodes[first + length].Type == NodeType.Whitespace))
                {
                    length++;
                }

                var paragraph = new Node
                                {
                                    Type = NodeType.Paragraph,
                                    Children = nodes.GetRange(first, length).ToList()
                                };

                nodes = nodes.RemoveRange(first, length).ToList();

                nodes.Insert(first, paragraph);

                node.Children = nodes;
            }
        }

        private IEnumerable<IEvaluator> GetValidEvaluators(Node parent)
        {
            if (parent.Type == NodeType.Root)
            {
                return _evaluators;
            }

            var parentEvaluator = _evaluators.Single(e => e.IsEvaluatorFor == parent.Type);

            var attribute = Attribute.GetCustomAttribute(parentEvaluator.GetType(), typeof(ValidChildNodesAttribute)) as ValidChildNodesAttribute;

            if (attribute == null)
            {
                return _evaluators;
            }

            var evaluators = new List<IEvaluator>();

            foreach (var evaluator in _evaluators)
            {
                if (attribute.ValidChildNodes.Contains(evaluator.IsEvaluatorFor))
                {
                    evaluators.Add(evaluator);
                }
            }

            return evaluators;
        }

        private static bool CheckEvaluatorAttributes(Node parent, IEvaluator evaluator)
        {
            // ReSharper disable once UsePatternMatching
            var validPreviousNodeSequenceAttributes = Attribute.GetCustomAttributes(evaluator.GetType(), typeof(ValidPreviousNodeSequenceAttribute)) as ValidPreviousNodeSequenceAttribute[];

            if (validPreviousNodeSequenceAttributes != null && validPreviousNodeSequenceAttributes.Length > 0)
            {
                if (parent.Children.Count == 0 && parent.Type == NodeType.Root)
                {
                    return true;
                }

                foreach (var validPreviousNodeSequenceAttribute in validPreviousNodeSequenceAttributes)
                {
                    if (validPreviousNodeSequenceAttribute.NodeTypeSequence.Count > parent.Children.Count)
                    {
                        continue;
                    }

                    var index = 1;

                    var match = true;

                    foreach (var nodeType in validPreviousNodeSequenceAttribute.NodeTypeSequence)
                    {
                        if (parent.Children[parent.Children.Count - index].Type != nodeType)
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                    {
                        return true;
                    }
                }

                return false;
            }

            return true;
        }
    }
}