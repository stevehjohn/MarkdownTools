using MarkdownTools.Models;
using System;
using System.Collections.Generic;

namespace MarkdownTools.Parser.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidChildNodesAttribute : Attribute
    {
        public IList<NodeType> ValidChildNodes;

        public ValidChildNodesAttribute(params NodeType[] validChildNodes)
        {
            ValidChildNodes = validChildNodes;
        }
    }
}