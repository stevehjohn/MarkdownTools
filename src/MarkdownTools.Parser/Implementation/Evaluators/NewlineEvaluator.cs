using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using MarkdownTools.Parser.Models;
using System;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    public class NewlineEvaluator : IEvaluator
    {
        public EvaluatorResult Evaluate(string source)
        {
            if (source.StartsWith(Environment.NewLine))
            {
                return new EvaluatorResult(
                    new Node
                    {
                        Type = NodeType.Newline
                    },
                    source.SafeSubstring(Environment.NewLine.Length));
            }

            return null;
        }
    }
}