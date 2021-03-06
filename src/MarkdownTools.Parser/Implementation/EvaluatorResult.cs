﻿using MarkdownTools.Models;

namespace MarkdownTools.Parser.Implementation
{
    public class EvaluatorResult
    {
        public Node Node { get; }
        public string EvaluateNext { get; }

        public EvaluatorResult(Node node, string evaluateNext)
        {
            Node = node;
            EvaluateNext = evaluateNext;
        }
    }
}