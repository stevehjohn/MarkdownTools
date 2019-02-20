using MarkdownTools.Parser.Models;
using System;
using System.Collections.Generic;

namespace MarkdownTools.Parser.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ValidPreviousNodeSequenceAttribute : Attribute
    {
        public IList<NodeType> NodeTypeSequence { get; }

        public ValidPreviousNodeSequenceAttribute(params NodeType[] nodeTypeSequence)
        {
            NodeTypeSequence = nodeTypeSequence;
        }
    }
}