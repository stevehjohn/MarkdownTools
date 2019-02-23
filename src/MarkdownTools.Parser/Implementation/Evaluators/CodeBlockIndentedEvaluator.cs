using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [Precedence(1)]
    public class CodeBlockIndentedEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.CodeBlock;

        public EvaluatorResult Evaluate(string source)
        {
            var code = new StringBuilder();

            while (! string.IsNullOrEmpty(source) && (source.StartsWith("    ") || source.StartsWith("\t")))
            {
                var start = source.StartsWith("\t") ? 1 : 4;

                if (source.Length == start)
                {
                    return null;
                }

                source = source.SafeSubstring(start);

                var eol = source.IndexOf(Environment.NewLine, StringComparison.Ordinal);

                if (eol > -1)
                {
                    code.AppendLine(source.SafeSubstring(0, eol));
                    source = source.SafeSubstring(eol + Environment.NewLine.Length);
                }
                else
                {
                    code.AppendLine(source);
                    source = null;
                }
            }

            if (code.Length == 0)
            {
                return null;
            }

            return new EvaluatorResult(
                new Node
                {
                    Type = NodeType.CodeBlock,
                    MetaData = new Dictionary<string, string>
                               {
                                   { "SubType", "Indented" }
                               },
                    Content = code.ToString()
                }, source);
        }
    }
}