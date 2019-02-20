using System;

namespace MarkdownTools.Parser.Attributes
{
    public class PrecedenceAttribute : Attribute
    {
        public int Precedence { get; }

        public PrecedenceAttribute(int precedence)
        {
            Precedence = precedence;
        }
    }
}