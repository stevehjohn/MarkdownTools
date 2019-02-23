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
    [ValidChildNodes(NodeType.Text, NodeType.Whitespace)]
    public class CodeBlockEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.CodeBlock;

        public EvaluatorResult Evaluate(string source)
        {
            return EvaluateForIndentedCodeBlock(source)
                   ?? EvaluateForTickMarkCodeBlock(source);
        }

        private EvaluatorResult EvaluateForTickMarkCodeBlock(string source)
        {
            if (!source.StartsWith("```"))
            {
                return null;
            }

            var length = 3;

            while (length < source.Length && source[length] == '`')
            {
                length++;
            }

            string language;
            string content = null;

            var eol = source.IndexOf(Environment.NewLine, StringComparison.Ordinal);

            if (eol > 0)
            {
                language = source.SafeSubstring(length, eol - length).Trim();
                source = source.SafeSubstring(eol + Environment.NewLine.Length);

                var span = source.IndexOf(new string('`', length), StringComparison.Ordinal);

                if (span > 0)
                {
                    content = source.SafeSubstring(0, span);
                    source = source.SafeSubstring(span + length);
                }
                else
                {
                    content = source;
                    source = null;
                }
            }
            else
            {
                language = source.SafeSubstring(length).Trim();
                source = null;
            }

            return new EvaluatorResult(
                new Node
                {
                    Type = NodeType.CodeBlock,
                    MetaData = new Dictionary<string, string>
                               {
                                   { "TickMarkLength", $"{length}" },
                                   { "Language", $"{language}" },
                                   { "SubType", "TickMarks" }
                               },
                    Content = content
                },
                source);
        }

        private EvaluatorResult EvaluateForIndentedCodeBlock(string source)
        {
            var code = new StringBuilder();

            while (!string.IsNullOrEmpty(source) && (source.StartsWith("    ") || source.StartsWith("\t")))
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