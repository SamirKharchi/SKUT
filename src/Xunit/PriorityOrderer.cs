using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace SKUT.Xunit
{
    /// <summary>
    /// <para>
    /// This test case orderer will read out a TestPriorityAttribute in order to sort
    /// the test case methods according to their priority value.
    /// </para>
    /// <para>
    /// Based on original source: https://github.com/xunit/samples.xunit/blob/main/TestOrderExamples/TestCaseOrdering/PriorityOrderer.cs
    /// </para>
    ///
    /// Mark the test class with [TestCaseOrderer(PriorityOrderer.Location, PriorityOrderer.Assembly)]
    /// to make it work. Then use [Fact, TestPriority(priority)] on test methods accordingly.
    /// </summary>
    public class PriorityOrderer : ITestCaseOrderer
    {
        public const string Assembly = "SKDDD.Testing";
        public const string Location = Assembly + ".Xunit.PriorityOrderer";

        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
            where TTestCase : ITestCase
        {
            var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

            foreach (var testCase in testCases)
            {
                var priority = 0;

                foreach (var attr in testCase.TestMethod.Method.GetCustomAttributes((typeof(TestPriorityAttribute).AssemblyQualifiedName)))
                {
                    priority = attr.GetNamedArgument<int>("Priority");
                }

                PriorityOrderer.GetOrCreate(sortedMethods, priority).Add(testCase);
            }

            foreach (var list in sortedMethods.Keys.Select(priority => sortedMethods[priority]))
            {
                list.Sort((x, y) =>
                              StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name,
                                                                       y.TestMethod.Method.Name));
                foreach (var testCase in list)
                {
                    yield return testCase;
                }
            }
        }

        static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key) where TValue : new()
        {
            if (dictionary.TryGetValue(key, out var result))
            {
                return result;
            }

            result          = new TValue();
            dictionary[key] = result;

            return result;
        }
    }
}