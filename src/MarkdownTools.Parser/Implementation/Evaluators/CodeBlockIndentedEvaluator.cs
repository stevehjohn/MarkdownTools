﻿using MarkdownTools.Parser.Attributes;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using MarkdownTools.Parser.Models;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    [ValidPreviousNodeSequence(NodeType.Newline)]
    public class CodeBlockIndentedEvaluator : BaseEvaluator
    {
        public override EvaluatorResult Evaluate(string source)
        {
            throw new System.NotImplementedException();
        }
    }
}