using System;

namespace MarkdownTools.Parser.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DoNotParseForParagraphsAttribute : Attribute
    {
    }
}