﻿using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using System;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [InlineElement]
    public class NewlineEvaluator : IEvaluator
    {
        public NodeType IsEvaluatorFor => NodeType.Newline;

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