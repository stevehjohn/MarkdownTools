using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using System;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [InlineElement]
    [ValidChildNodes]
    public class StrongEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.Strong;

        public EvaluatorResult Evaluate(string source)
        {
            if (source.StartsWith("**") || source.StartsWith("__"))
            {
                var line = source.GetLine();

                if (line.Length == 2)
                {
                    return null;
                }

                var end = line.IndexOf($"{source[0]}{source[0]}", 2, StringComparison.Ordinal);

                if (end == -1)
                {
                    return null;
                }

                return new EvaluatorResult(
                    new Node
                    {
                        Type = NodeType.Strong,
                        Content = line.Substring(2, end - 2)
                    }, 
                    source.SafeSubstring(end + 1));
            }

            return null;
        }
    }
}