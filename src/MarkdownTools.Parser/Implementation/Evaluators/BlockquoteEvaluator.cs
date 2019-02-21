﻿using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using System;
using System.Text;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [ParseContent]
    [ValidPreviousNodeSequence(NodeType.Newline)]
    [ValidPreviousNodeSequence(NodeType.Newline, NodeType.Whitespace)]
    public class BlockquoteEvaluator : IEvaluator
    {
        public EvaluatorResult Evaluate(string source)
        {
            var quote = new StringBuilder();

            while (!string.IsNullOrEmpty(source) && (source.StartsWith(">") || source.StartsWith(" >") || source.StartsWith("  >") || source.StartsWith("   >")))
            {
                source = source.SafeSubstring(source.IndexOf('>') + 1);

                var eol = source.IndexOf(Environment.NewLine, StringComparison.Ordinal);

                if (eol > -1)
                {
                    quote.Append($"{source.SafeSubstring(0, eol).Trim()} ");
                    source = source.SafeSubstring(eol + Environment.NewLine.Length);
                }
                else
                {
                    quote.Append(source.Trim());
                    source = null;
                }
            }

            var blockquote = quote.ToString().Trim();

            if (blockquote.Length > 0)
            {
                return new EvaluatorResult(
                    new Node
                    {
                        Type = NodeType.BlockQuote,
                        Content = blockquote
                    }
                    , source);
            }

            return null;
        }
    }
}