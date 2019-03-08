using MarkdownTools.Parser.Implementation;
using NUnit.Framework;

namespace MarkdownTools.Parser.Tests.Implementation
{
    [TestFixture]
    public class MarkdownParserBuilderTests
    {
        [Test]
        public void Builder_passes_all_evaluators_to_parser()
        {
            var parser = (MarkdownParser) MarkdownParserBuilder.GetParserWithAllEvaluators();

            Assert.That(parser.Evaluators.Count, Is.EqualTo(17));
        }
    }
}