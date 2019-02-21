﻿using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Extensions;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using MarkdownTools.Parser.Models;
using System.Collections.Generic;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [Precedence(int.MaxValue - 1)]
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
                    new Node
                    {
                        Type = NodeType.Whitespace,
                        MetaData = new Dictionary<string, string>
                                   {
                                       { "Length", $"{length}" }
                                   }
                    },
                    source.SafeSubstring(length));
            }

            return null;
        }
    }
}