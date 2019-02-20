using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Interface;
using MarkdownTools.Parser.Models;
using System;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [ValidParentNodes(NodeType.Root)]
    public class NewlineEvaluator : IEvaluator
    {
        public int Precedence => 2;

        public EvaluatorResult Evaluate(string source)
        {
            if (source.StartsWith(Environment.NewLine))
            {
                return new EvaluatorResult(
                    new Node(NodeType.Newline),
                    source.SafeSubstring(Environment.NewLine.Length));
            }

            return null;
        }
    }
}