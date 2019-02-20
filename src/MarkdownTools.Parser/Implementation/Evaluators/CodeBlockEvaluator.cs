﻿using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Interface;
using MarkdownTools.Parser.Models;
using System;
using System.Collections.Generic;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [ValidParentNodes(NodeType.Root)]
    public class CodeBlockEvaluator : IEvaluator
    {
        public int Precedence => 1;

        public EvaluatorResult Evaluate(string source)
        {
            if (! source.StartsWith("```"))
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
                new Node(
                    NodeType.CodeBlock,
                    new Dictionary<string, string>
                    {
                        { "Length", $"{length}" },
                        { "Language", $"{language}" }
                    },
                    content),
                source);
        }
    }
}