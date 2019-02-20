using MarkdownTools.Parser.Implementation;
using MarkdownTools.Parser.Implementation.Evaluators;
using NUnit.Framework;
using System.IO;
using Newtonsoft.Json;

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

        [TestCase("TR Delta Sync Process Illustration")]
        public void Parse(string sourceFile)
        {
            var markdown = File.ReadAllText($"TestFiles\\Inputs\\{sourceFile}.md");

            var result = _parser.Parse(markdown);

            var json = File.ReadAllText($"TestFiles\\Outputs\\{sourceFile}.json");

            Assert.That(JsonConvert.SerializeObject(result), Is.EqualTo(json));
        }

        [Test]
        public void Parser_sorts_evaluators_in_precedence_order()
        {
            var attributes = ((MarkdownParser) _parser).Evaluators;

            Assert.That(attributes[0], Is.AssignableTo(typeof(CodeBlockEvaluator)));
            Assert.That(attributes[1], Is.AssignableTo(typeof(HeadingEvaluator)));
        }
    }
}