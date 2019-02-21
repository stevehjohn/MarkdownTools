using MarkdownTools.Models;
using MarkdownTools.Parser.Attributes;
using NUnit.Framework;
using System;

namespace MarkdownTools.Parser.Tests.Attributes
{
    [TestFixture]
    public class ValidParentNodeAttributeTests
    {
        [Test]
        public void Can_gather_metadata_from_class_decorated_with_attribute()
        {
            var instance = new DecoratedWithValidParentNodeAttribute();

            var attribute = Attribute.GetCustomAttribute(instance.GetType(), typeof(ValidParentNodesAttribute)) as ValidParentNodesAttribute;

            Assert.NotNull(attribute);
            Assert.That(attribute.NodeTypes, Contains.Item(NodeType.BlockQuote));
            Assert.That(attribute.NodeTypes, Contains.Item(NodeType.CodeBlock));
            Assert.That(attribute.NodeTypes, ! Contains.Item(NodeType.Heading));
        }
    }
    
    [ValidParentNodes(NodeType.BlockQuote, NodeType.CodeBlock)]
    internal class DecoratedWithValidParentNodeAttribute
    {
    }
}