using System;

namespace MarkdownTools.Parser.Implementation
{
    public class ParserException : Exception
    {
        public string Content { get; }

        public ParserException(string message, string content) : base(message)
        {
            Content = content;
        }
    }
}