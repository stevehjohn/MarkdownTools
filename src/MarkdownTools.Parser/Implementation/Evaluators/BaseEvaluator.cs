﻿using MarkdownTools.Parser.Implementation.Evaluators.Interface;
using MarkdownTools.Parser.Models;
using System.Collections.Generic;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    public abstract class BaseEvaluator
    {
        public void Parse(Node parent, string markdown, IList<IEvaluator> evaluators)
        {
        }
    }
}