using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Interface;
using MarkdownTools.Parser.Models;
using System.Collections.Generic;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [ValidParentNodes(NodeType.Root)]
    public class WhitespaceEvaluator : IEvaluator
    {
        public int Precedence => 3;

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