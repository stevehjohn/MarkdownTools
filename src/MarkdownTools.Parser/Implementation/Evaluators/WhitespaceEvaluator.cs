using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Models;
using System.Collections.Generic;
using MarkdownTools.Parser.Implementation.Evaluators.Interface;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    public class WhitespaceEvaluator : IEvaluator
    {
        public EvaluatorResult Evaluate(string source)
        {
            var length = 0;

            while (length < source.Length && (source[length] == ' ' || source[length] == '\t'))
            {
                length++;
            }

            if (length > 0)
            {
                return new EvaluatorResult(
                    new Node(NodeType.Whitespace, new Dictionary<string, string>
                                                  {
                                                      { "Length", $"{length}" }
                                                  }),
                    source.SafeSubstring(length));
            }

            return null;
        }
    }
}