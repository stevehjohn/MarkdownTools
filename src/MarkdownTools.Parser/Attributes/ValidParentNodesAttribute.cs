using MarkdownTools.Models;
using System;
using System.Collections.Generic;

namespace MarkdownTools.Parser.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidParentNodesAttribute : Attribute
    {
        public IList<NodeType> ValidParentNodes;

        public ValidParentNodesAttribute(params NodeType[] validParentNodes)
        {
            ValidParentNodes = validParentNodes;
        }
    }
}