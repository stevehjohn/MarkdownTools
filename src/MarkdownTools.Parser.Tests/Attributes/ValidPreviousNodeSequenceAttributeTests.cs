using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using NUnit.Framework;
using System;

namespace MarkdownTools.Parser.Tests.Attributes
{
    [TestFixture]
    public class ValidPreviousNodeSequenceAttributeTests
    {
        [Test]
        public void Can_gather_metadata_from_class_decorated_with_attribute()
        {
            var instance = new DecoratedWithValidPreviousNodeSequenceAttribute();

            var attribute = Attribute.GetCustomAttribute(instance.GetType(), typeof(ValidPreviousNodeSequenceAttribute)) as ValidPreviousNodeSequenceAttribute;

            Assert.NotNull(attribute);
            Assert.That(attribute.NodeTypeSequence[0], Is.EqualTo(NodeType.BlockQuote));
            Assert.That(attribute.NodeTypeSequence[1], Is.EqualTo(NodeType.CodeBlock));
        }
    }

    [ValidPreviousNodeSequence(NodeType.BlockQuote, NodeType.CodeBlock)]
    internal class DecoratedWithValidPreviousNodeSequenceAttribute
    {
    }
}