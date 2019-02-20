using MarkdownTools.Parser.Implementation.Evaluators.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MarkdownTools.Parser.Implementation
{
    public static class MarkdownParserBuilder
    {
        public static IMarkdownParser GetParserWithAllEvaluators()
        {
            var instances = new List<BaseEvaluator>();

            var evaluators = Assembly.GetAssembly(typeof(BaseEvaluator))
                                     .GetTypes()
                                     .Where(t => t.BaseType == typeof(BaseEvaluator))
                                     .ToList();

            evaluators.ForEach(e => instances.Add((BaseEvaluator) Activator.CreateInstance(e)));

            return new MarkdownParser(instances);
        }
    }
}