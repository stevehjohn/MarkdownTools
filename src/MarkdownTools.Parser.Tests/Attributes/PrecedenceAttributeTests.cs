using MarkdownTools.Parser.Attributes;
using NUnit.Framework;
using System;

namespace MarkdownTools.Parser.Tests.Attributes
{
    [TestFixture]
    public class PrecedenceAttributeTests
    {
        [Test]
        public void Can_gather_metadata_from_class_decorated_with_attribute()
        {
            var instance = new DecoratedWithPrecedenceAttribute();

            var attribute = Attribute.GetCustomAttribute(instance.GetType(), typeof(PrecedenceAttribute)) as PrecedenceAttribute;

            Assert.NotNull(attribute);
            Assert.That(attribute.Precedence, Is.EqualTo(666));
        }
    }

    [Precedence(666)]
    internal class DecoratedWithPrecedenceAttribute
    {
    }
}