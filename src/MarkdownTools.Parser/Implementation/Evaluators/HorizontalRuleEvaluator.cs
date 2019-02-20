﻿using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using MarkdownTools.Parser.Models;
using System;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [ValidPreviousNodeSequence(NodeType.Newline)]
    [ValidPreviousNodeSequence(NodeType.Newline, NodeType.Whitespace)]
    public class HorizontalRuleEvaluator : BaseEvaluator
    {
        public override EvaluatorResult Evaluate(string source)
        {
            if (source.StartsWith("---") || source.StartsWith("***") || source.StartsWith("___"))
            {
                var character = source[0];

                var length = 3;

                while (length < source.Length && source[length] == character)
                {
                    length++;
                }

                var eol = source.IndexOf(Environment.NewLine, StringComparison.Ordinal);

                if (eol < 0)
                {
                    if (string.IsNullOrWhiteSpace(source.SafeSubstring(length)))
                    {
                        return new EvaluatorResult(
                            new Node(NodeType.HorizontalRule), 
                            null);
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(source.SafeSubstring(length, eol - length)))
                    {
                        return new EvaluatorResult(
                            new Node(NodeType.HorizontalRule),
                            source.Substring(eol));
                    }
                }
            }

            return null;
        }
    }
}