using MarkdownTools.Parser.Implementation.Evaluators.Interface;
using MarkdownTools.Parser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MarkdownTools.Parser.Implementation
{
    public class MarkdownParser
    {
        private readonly IList<IEvaluator> _evaluators;

        private Node _root;

        public MarkdownParser()
        {
            _evaluators = new List<IEvaluator>();

            var evaluators = Assembly.GetAssembly(typeof(MarkdownParser))
                                     .GetTypes()
                                     .Where(t => t.IsAssignableFrom(typeof(IEvaluator)))
                                     .ToList();

            evaluators.ForEach(e => _evaluators.Add((IEvaluator) Activator.CreateInstance(e)));
        }

        public Node Parse(string markdown)
        {
            _root = new Node(NodeType.Root);

            return _root;
        }
    }
}