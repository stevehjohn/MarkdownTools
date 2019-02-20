using System;

namespace MarkdownTools.Parser.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class PrecedenceAttribute : Attribute
    {
        public int Precedence { get; }

        public PrecedenceAttribute(int precedence)
        {
            Precedence = precedence;
        }
    }
}