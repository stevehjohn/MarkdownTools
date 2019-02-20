using MarkdownTools.Parser.Implementation;
using MarkdownTools.Parser.Implementation.Evaluators.Interface;
using NUnit.Framework;
using System.Linq;

namespace MarkdownTools.Parser.Tests.Implementation
{
    [TestFixture]
    public class MarkdownParserTests
    {
        private IMarkdownParser _parser;

        [SetUp]
        public void SetUp()
        {
            _parser = new MarkdownParser(Enumerable.Empty<IEvaluator>());
        }

        // TODO: Proper tests
        [Test]
        public void Parse()
        {
            _parser.Parse(null);
        }

        [Test]
        public void Parser_invokes_evaluators_in_precedence_order()
        {
        }
    }
}