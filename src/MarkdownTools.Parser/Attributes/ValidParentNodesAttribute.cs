using MarkdownTools.Models;
using System;
using System.Collections.Generic;

namespace MarkdownTools.Parser.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ValidParentNodesAttribute : Attribute
    {
        public IList<NodeType> NodeTypes { get; }

        public ValidParentNodesAttribute(params NodeType[] nodeTypes)
        {
            NodeTypes = nodeTypes;
        }
    }
}