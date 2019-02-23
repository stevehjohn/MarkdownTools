using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using NUnit.Framework;
using System;

namespace MarkdownTools.Parser.Tests.Attributes
{
    [TestFixture]
    public class ValidChildNodesAttributeTests
    {
        [Test]
        public void Can_gather_metadata_from_class_decorated_with_attribute()
        {
            var instance = new DecoratedWithValidChildNodesAttribute();

            var attribute = Attribute.GetCustomAttribute(instance.GetType(), typeof(ValidChildNodesAttribute)) as ValidChildNodesAttribute;

            Assert.NotNull(attribute);
            Assert.That(attribute.ValidChildNodes.Count, Is.EqualTo(2));
            Assert.True(attribute.ValidChildNodes.Contains(NodeType.Heading));
            Assert.True(attribute.ValidChildNodes.Contains(NodeType.HorizontalRule));
        }
    }

    [ValidChildNodes(NodeType.Heading, NodeType.HorizontalRule)]
    internal class DecoratedWithValidChildNodesAttribute
    {
    }
}