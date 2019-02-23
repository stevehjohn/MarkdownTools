using MarkdownTools.Models;
using MarkdownTools.Parser.Implementation.Evaluators;
using MarkdownTools.Parser.Implementation.Evaluators.Base;
using NUnit.Framework;
using System;

namespace MarkdownTools.Parser.Tests.Implementation.Evaluators
{
    [TestFixture]
    public class TableEvaluatorTests
    {
        private IEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            _evaluator = new TableEvaluator();
        }

        [TestCase("Not a table", false)]
        //[TestCase("|Heading 1|Heading 2|\n|---|---|\n|Column 1|Column 2|", true)]
        public void Identifies_tables(string input, bool isTable)
        {
            input = input.Replace("\n", Environment.NewLine);

            if (isTable)
            {
                Assert.That(_evaluator.Evaluate(input).Node.Type, Is.EqualTo(NodeType.Table));
            }
            else
            {
                Assert.Null(_evaluator.Evaluate(input));
            }
        }
    }
}