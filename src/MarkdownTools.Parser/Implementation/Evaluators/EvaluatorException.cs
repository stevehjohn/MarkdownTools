using System;

namespace MarkdownTools.Parser.Implementation.Evaluators
{
    public class EvaluatorException : Exception
    {
        public EvaluatorException(string message) : base (message)
        {
        }
    }
}