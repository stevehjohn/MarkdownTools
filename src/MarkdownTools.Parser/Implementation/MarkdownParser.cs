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
        private readonly IList<BaseEvaluator> _evaluators;

        private Node _root;

        public IReadOnlyList<BaseEvaluator> Evaluators => new ReadOnlyCollection<BaseEvaluator>(_evaluators);

        public MarkdownParser(IEnumerable<BaseEvaluator> evaluators)
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
            _root = new Node(NodeType.Root);

            var previousNode = _root;

            return _root;
        }
    }
}