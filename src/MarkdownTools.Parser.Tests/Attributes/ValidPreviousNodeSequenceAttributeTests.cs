using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using NUnit.Framework;
using System;
using System.Linq;

namespace MarkdownTools.Parser.Tests.Attributes
{
    [TestFixture]
    public class ValidPreviousNodeSequenceAttributeTests
    {
        [Test]
        public void Can_gather_metadata_from_class_decorated_with_attribute()
        {
            var instance = new DecoratedWithValidPreviousNodeSequenceAttribute();

            var attributes = (Attribute.GetCustomAttributes(instance.GetType(), typeof(ValidPreviousNodeSequenceAttribute)) as ValidPreviousNodeSequenceAttribute[])?
                .ToList()
                .OrderBy(a => a.NodeTypeSequence.Count)
                .ToList();

            Assert.NotNull(attributes);
            Assert.That(attributes[0].NodeTypeSequence[0], Is.EqualTo(NodeType.Blockquote));
            Assert.That(attributes[1].NodeTypeSequence[0], Is.EqualTo(NodeType.Blockquote));
            Assert.That(attributes[1].NodeTypeSequence[1], Is.EqualTo(NodeType.CodeBlock));
        }
    }

    [ValidPreviousNodeSequence(NodeType.Blockquote)]
    [ValidPreviousNodeSequence(NodeType.Blockquote, NodeType.CodeBlock)]
    internal class DecoratedWithValidPreviousNodeSequenceAttribute
    {
    }
}