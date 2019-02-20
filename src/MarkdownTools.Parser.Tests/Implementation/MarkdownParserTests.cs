using MarkdownTools.Parser.Implementation;
using MarkdownTools.Parser.Implementation.Evaluators;
using NUnit.Framework;

namespace MarkdownTools.Parser.Tests.Implementation
{
    [TestFixture]
    public class MarkdownParserTests
    {
        private IMarkdownParser _parser;

        [SetUp]
        public void SetUp()
        {
            _parser = MarkdownParserBuilder.GetParserWithAllEvaluators();
        }

        // TODO: Proper tests
        //[Test]
        //public void Parse()
        //{
        //    _parser.Parse(null);
        //}

        [Test]
        public void Parser_sorts_evaluators_in_precedence_order()
        {
            var attributes = ((MarkdownParser) _parser).Evaluators;

            Assert.That(attributes[0], Is.AssignableTo(typeof(CodeBlockEvaluator)));
            Assert.That(attributes[1], Is.AssignableTo(typeof(HeadingEvaluator)));
        }
    }
}